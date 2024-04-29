using ApiBase.Contracts.Admin;
using ApiBase.Data;
using ApiBase.Helpers;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography;

namespace ApiBase.Repositories.Admin
{
    public class AdminRepository(IConfiguration config) : IAdminRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly AuthHelper _authHelper = new(config);

        public async Task<bool> Delete(int userId)
        {
            Auth authDb = await _entityFramework.Auth.Where(a => a.Instituicao.Instituicao_Id == userId).FirstAsync();

            if (authDb != null)
            {
                string logDescricao = JsonConvert.SerializeObject(authDb);

                AuditLogs auditLogs = new()
                {
                    Tipo = "delete",
                    Descricao = logDescricao,
                    Auth_Usuario = authDb.Usuario
                };

                await _entityFramework.AddAsync(auditLogs);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    _entityFramework.Remove(authDb);

                    if (await _entityFramework.SaveChangesAsync() > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<IEnumerable<AuditLogs>> Get()
        {
            return await _entityFramework.AuditLogs.ToListAsync();
        }

        public async Task<bool> Post(InstituicaoInsert instituicaoInsert)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(instituicaoInsert.Password, passwordSalt);

            if ((await _entityFramework.Auth.Where(a => a.Usuario == instituicaoInsert.Usuario).FirstAsync()) == null)
            {
                Auth novoAuth = new()
                {
                    Usuario = instituicaoInsert.Usuario,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                InstituicaoEF novaInstituicaoEF = new()
                {
                    Nome = instituicaoInsert.Usuario,
                    PlusCode = instituicaoInsert.PlusCode,
                    Tipo_Conta_Id = 3 // Tipo Instituição
                };

                novoAuth.Instituicao = novaInstituicaoEF;

                await _entityFramework.AddAsync(novoAuth);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Put(int userId, int roleId)
        {
            Usuario usuarioDb = await _entityFramework.Usuarios.Where(u => u.Auth_Id == userId).FirstAsync();

            if (usuarioDb != null)
            {
                usuarioDb.Tipo_Conta_Id = roleId;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
