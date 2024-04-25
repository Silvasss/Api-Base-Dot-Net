using ApiBase.Contracts.Admin;
using ApiBase.Data;
using ApiBase.Helpers;
using ApiBase.Models;
using Dapper;
using System.Data;
using System.Security.Cryptography;

namespace ApiBase.Repositories.Admin
{
    public class AdminRepository(IConfiguration config) : IAdminRepository
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);

        public async Task<bool> Delete(int userId)
        {
            string sql = @"EXEC spUser_Delete @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<AuditLogs>> Get()
        {
            return await _dapper.LoadDataAsync<AuditLogs>(@"EXEC spAdmin_LogGet");
        }

        public async Task<bool> Post(InstituicaoInsert instituicaoInsert)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(instituicaoInsert.Password, passwordSalt);

            string sql = @"EXEC spAdmin_InstituicaoInsert
                @Usuario = @UsuarioParameter,                                          
                @PasswordHash = @PasswordHashParameter,                
                @PasswordSalt = @PasswordSaltParameter,
                @Nome = @NomeParameter,
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UsuarioParameter", instituicaoInsert.Usuario, DbType.String);
            sqlParameters.Add("@PasswordHashParameter", passwordHash, DbType.Binary);
            sqlParameters.Add("@PasswordSaltParameter", passwordSalt, DbType.Binary);
            sqlParameters.Add("@NomeParameter", instituicaoInsert.Nome, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", instituicaoInsert.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(int userId, int roleId)
        {
            string sql = @"EXEC spAdmin_UserUpdateRole
                @UserId = @UserIdParameter,
                @Role = @RoleParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);
            sqlParameters.Add("@RoleParameter", roleId, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
