using ApiBase.Contracts.Usuario;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiBase.Repositories.Usuario
{
    public class UsuarioRepository(IConfiguration config) : IUsuarioRepository
    {
        private readonly DataContextDapper _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);

        public async Task<bool> Delete(int id)
        {
            string sql = @"EXEC spUser_Delete @UserId=@UserIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<ActionResult<UserCompleto>> Get(int id)
        {
            UserCompleto usuario = await _dapper.LoadDataSingle<UserCompleto>(@"EXEC spUser_Get @UserId='" + id + "'");

            usuario.Experiencia = (List<Experiencia>?)await _dapper.LoadDataAsync<Experiencia>(@"EXEC spExperiencia_Get @UserId='" + id + "'");

            usuario.Graduacao = (List<Graduacao>?)await _dapper.LoadDataAsync<Graduacao>(@"EXEC spGraduacao_Get @UserId='" + id + "'");

            return usuario;
        }

        public async Task<bool> Post(UserForLoginDto userForSetPassword)
        {
            if (await _authHelper.UpdatePasswordAsync(userForSetPassword))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(User user)
        {
            string sql = @"EXEC spUser_PerfilUpdate
                @UserId = @UserIdParameter,
                @Nome = @NomeParameter,  
                @Pais = @PaisParameter,  
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", user.Id, DbType.Int32);
            sqlParameters.Add("@NomeParameter", user.Nome, DbType.String);
            sqlParameters.Add("@PaisParameter", user.Pais, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", user.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
