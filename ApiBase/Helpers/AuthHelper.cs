using ApiBase.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiBase.Helpers
{
    public class AuthHelper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;
        private readonly DataContextEF _entityFramework = new(config);

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

        public string CreateToken(int userId, string role, string userName)
        {

            Claim[] claims = [new Claim("userId", userId.ToString()), new Claim("nome", userName), new Claim(ClaimTypes.Role, role)];

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

        public async Task<bool> SetPasswordAsync(UserForRegistrationDto userForSetPassword)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = GetPasswordHash(userForSetPassword.Password, passwordSalt);

            if (await _entityFramework.Auth.Where(a => a.Usuario == userForSetPassword.Usuario).FirstOrDefaultAsync() == null)
            {
                Auth novoAuth = new()
                {
                    Usuario = userForSetPassword.Usuario,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                Usuario novoUsuario = new()
                {
                    Nome = userForSetPassword.Nome,
                    Tipo_Conta_Id = 2, // Tipo usuário
                    Pais = "Brasil",
                    PlusCode  = "RM88+4G Plano Diretor Sul, Palmas - State of Tocantins"
                };

                novoAuth.UsuarioPerfil = novoUsuario;

                await _entityFramework.AddAsync(novoAuth);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
