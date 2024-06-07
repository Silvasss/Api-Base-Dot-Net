using ApiBase.Contracts.Admin;
using ApiBase.Data;
using ApiBase.Dtos;
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
            Auth? authDb = await _entityFramework.Auth.Where(a => a.Instituicao.Instituicao_Id == userId)
                .Include(a => a.Instituicao)
                .ThenInclude(c => c.Cursos)
                .FirstOrDefaultAsync();

            if (authDb != null)
            {
                string logDescricao = JsonConvert.SerializeObject(authDb, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

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

        public async Task<AdminDashboard> Get()
        {
            string jsonString = @" {
                ""analyticSistema"": [
                    {
                        ""title"": ""Total Visualizações"",
                        ""count"": ""1,42,236"",
                        ""percentage"": 59.3,
                        ""color"": null
                    },
                    {
                        ""title"": ""Total Usuários"",
                        ""count"": ""78,250"",
                        ""percentage"": 70.5,
                        ""color"": null
                    },
                    {
                        ""title"": ""Total Instituições"",
                        ""count"": ""18,800"",
                        ""percentage"": 27.4,
                        ""color"": ""warning""
                    },
                    {
                        ""title"": ""Total Logs"",
                        ""count"": ""35,078"",
                        ""percentage"": 7.4,
                        ""color"": ""warning""
                    }
                ],
                ""fonteTrafico"": [63, 15, 22]
            }";

            AdminDashboard dados = JsonConvert.DeserializeObject<AdminDashboard>(jsonString);

            // Seleciona os 10 primeiros objetos criados mais recentemente
            dados.Logs = await _entityFramework.Logs
                .OrderByDescending(e => e.TimeStamp)
                .Take(10)
                .ToListAsync();

            return dados;
        }

        public async Task<IEnumerable<InstituicaoEF>> GetAllInstituicao()
        {
            return await _entityFramework.Instituicao
                .Select(x => new InstituicaoEF
                {
                    Instituicao_Id = x.Instituicao_Id,
                    Nome = x.Nome,
                    PlusCode = x.PlusCode,
                    Ativo = x.Ativo,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SerilogEntry>> GetAllLogs()
        {
            return await _entityFramework.Logs.OrderByDescending(e => e.TimeStamp).ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await _entityFramework.Usuarios
                .Select(x => new Usuario
                {
                    Usuario_Id = x.Usuario_Id,
                    Nome = x.Nome,
                    PlusCode = x.PlusCode,
                    CreatedAt = x.CreatedAt,
                    Auth_Id = x.Auth_Id
                })
                .ToListAsync();
        }

        public async Task<bool> Post(InstituicaoDtoCreate instituicaoInsert)
        {
            byte[] passwordSalt = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(instituicaoInsert.Password, passwordSalt);

            if ((await _entityFramework.Auth.Where(a => a.Usuario == instituicaoInsert.Usuario).FirstOrDefaultAsync()) == null)
            {
                Auth novoAuth = new()
                {
                    Usuario = instituicaoInsert.Usuario,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                InstituicaoEF novaInstituicaoEF = new()
                {
                    Nome = instituicaoInsert.Nome,
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
            Usuario? usuarioDb = await _entityFramework.Usuarios.Where(u => u.Auth_Id == userId).FirstOrDefaultAsync();

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
