using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;


namespace ApiBase.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);
        private readonly ReusableSql _reusableSql = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg => { cfg.CreateMap<UserForRegistrationDto, UserComplete>(); }));


        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if (!_authHelper.IsValidEmail(userForRegistration.Email))
            {
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = "Validate the format of the email!",
                    ContentType = "text/plain"
                };
            }

            if (userForRegistration.Password.Length >= 6)
            {
                if (userForRegistration.Password == userForRegistration.PasswordConfirm)
                {
                    string sqlForCheckEmail = @"EXEC DotNetDatabase.spCheckEmail_Get 
                        @Email = @Email";

                    DynamicParameters sqlParameters = new();

                    sqlParameters.Add("@Email", userForRegistration.Email, DbType.String);

                    IEnumerable<UserForCheckEmailDto> existingUsers = _dapper.LoadDataWithParameters<UserForCheckEmailDto>(sqlForCheckEmail, sqlParameters);

                    if (existingUsers.Count() == 0)
                    {
                        UserForLoginDto userForSetPassword = new()
                        {
                            Email = userForRegistration.Email,
                            Password = userForRegistration.Password
                        };

                        if (_authHelper.SetPassword(userForSetPassword))
                        {
                            UserComplete userComplete = _mapper.Map<UserComplete>(userForRegistration);

                            if (_reusableSql.UpsertUser(userComplete))
                            {
                                return Ok();
                            }

                            return new ContentResult
                            {
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                Content = "Failed to add user.",
                                ContentType = "text/plain"
                            };
                        }

                        return new ContentResult
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Content = "Failed to register user.",
                            ContentType = "text/plain"
                        };
                    }

                    return new ContentResult
                    {
                        StatusCode = (int)HttpStatusCode.Conflict,
                        Content = "User with this email already exists!",
                        ContentType = "text/plain"
                    };
                }

                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = "Passwords do not match!",
                    ContentType = "text/plain"
                };
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Minimum 6 characters!",
                ContentType = "text/plain"
            };
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"EXEC DotNetDatabase.spLoginConfirmation_Get 
                @Email = @EmailParam";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@EmailParam", userForLogin.Email, DbType.String);

            UserForLoginConfirmationDto userForConfirmation = _dapper.LoadDataSingleWithParameters<UserForLoginConfirmationDto>(sqlForHashAndSalt, sqlParameters);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt);

            // if (passwordHash == userForConfirmation.PasswordHash) // Won't work

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForConfirmation.PasswordHash[index])
                {
                    return StatusCode(401, "Incorrect password!");
                }
            }

            string userIdSql = @"
                SELECT UserId FROM DotNetDatabase.Usuarios WHERE Email = '" +
                userForLogin.Email + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userId)}
            });
        }


        [HttpGet("RefreshToken")]
        public string RefreshToken()
        {
            string userIdSql = @"
                SELECT UserId FROM DotNetDatabase.Usuarios WHERE UserId = '" +
                User.FindFirst("userId")?.Value + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return _authHelper.CreateToken(userId);
        }


        [AllowAnonymous]
        [HttpGet("TestConnection")]
        public DateTime TestConnection()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }
    }
}
