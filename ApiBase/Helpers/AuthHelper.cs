using ApiBase.Data;
using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using ApiBase.Dtos;


namespace ApiBase.Helpers
{
    public class AuthHelper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;
        private readonly DataContextDapper _dapper = new(config);

        public byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(passwordSalt);

            return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000000,
                numBytesRequested: 256 / 8
            );
        }

        public string CreateToken(int userId)
        {
            Claim[] claims = [new Claim("userId", userId.ToString())];

            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new(Encoding.UTF8.GetBytes(tokenKeyString ?? ""));

            SigningCredentials credentials = new(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };

            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool SetPassword(UserForLoginDto userForSetPassword)
        {

            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = GetPasswordHash(userForSetPassword.Password, passwordSalt);

            Console.WriteLine("0x" + BitConverter.ToString(passwordHash).Replace("-", ""));
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(passwordHash, 0, passwordHash.Length));
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(passwordHash));
            Console.WriteLine(Convert.ToBase64String(passwordHash));

            string sqlAddAuth = @"EXEC DotNetDatabase.spRegistration_Upsert
                @Email = @EmailParam, 
                @PasswordHash = @PasswordHashParam, 
                @PasswordSalt = @PasswordSaltParam";

            DynamicParameters sqlParameters = new DynamicParameters();

            sqlParameters.Add("@EmailParam", userForSetPassword.Email, DbType.String);
            sqlParameters.Add("@PasswordHashParam", passwordHash, DbType.Binary);
            sqlParameters.Add("@PasswordSaltParam", passwordSalt, DbType.Binary);

            return _dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters);
        }

        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email))
                return false;

            Regex regex = new(emailPattern);

            return regex.IsMatch(email);
        }
    }
}
