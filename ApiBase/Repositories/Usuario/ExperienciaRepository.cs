using ApiBase.Contracts;
using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using System.Data;

namespace ApiBase.Repositories.Usuario
{
    public class ExperienciaRepository(IConfiguration config) : IExperienciaRepository
    {
        private readonly DataContextDapper _dapper = new(config);

        public async Task<bool> Delete(int id, int Experienciaid)
        {
            string sql = @"EXEC spExperiencia_Delete 
                @UserId = @UserIdParameter,
                @ExperienciaId = @ExperienciaIdParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", Experienciaid, DbType.Int32);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Post(Experiencia experiencia, int id)
        {
            string sql = @"EXEC spExperiencia_Upsert
                @UserId = @UserIdParameter,                
                @ExperienciaId = @ExperienciaIdParameter,                 
                @Setor = @SetorParameter, 
                @Empresa = @EmpresaParameter, 
                @Situacao = @SituacaoParameter,  
                @Tipo = @TipoParameter,
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", experiencia.Id, DbType.Int32);
            sqlParameters.Add("@SetorParameter", experiencia.Setor, DbType.String);
            sqlParameters.Add("@EmpresaParameter", experiencia.Empresa, DbType.String);
            sqlParameters.Add("@SituacaoParameter", experiencia.Situacao, DbType.String);
            sqlParameters.Add("@TipoParameter", experiencia.Tipo, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", experiencia.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Put(Experiencia experiencia, int id)
        {
            string sql = @"EXEC spExperiencia_Upsert
                @UserId = @UserIdParameter,                
                @ExperienciaId = @ExperienciaIdParameter,                 
                @Setor = @SetorParameter, 
                @Empresa = @EmpresaParameter, 
                @Situacao = @SituacaoParameter,  
                @Tipo = @TipoParameter,
                @PlusCode = @PlusCodeParameter";

            DynamicParameters sqlParameters = new();

            sqlParameters.Add("@UserIdParameter", id, DbType.Int32);
            sqlParameters.Add("@ExperienciaIdParameter", experiencia.Id, DbType.Int32);
            sqlParameters.Add("@SetorParameter", experiencia.Setor, DbType.String);
            sqlParameters.Add("@EmpresaParameter", experiencia.Empresa, DbType.String);
            sqlParameters.Add("@SituacaoParameter", experiencia.Situacao, DbType.String);
            sqlParameters.Add("@TipoParameter", experiencia.Tipo, DbType.String);
            sqlParameters.Add("@PlusCodeParameter", experiencia.PlusCode, DbType.String);

            if (await _dapper.ExecuteSqlWithParametersAsync(sql, sqlParameters))
            {
                return true;
            }

            return false;
        }
    }
}
