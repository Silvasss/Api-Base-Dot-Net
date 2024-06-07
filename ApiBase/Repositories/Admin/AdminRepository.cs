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
            ""logs"": [
                {
                    ""Id"": 1,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-10T11:06:59.763""
                },
                {
                    ""Id"": 2,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-10T11:08:39.6""
                },
                {
                    ""Id"": 3,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-10T11:16:02.137""
                },
                {
                    ""Id"": 4,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-10T11:27:12.127""
                },
                {
                    ""Id"": 5,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-10T11:32:25.447""
                },
                {
                    ""Id"": 1022,
                    ""MessageTemplate"": ""Failed to determine the https port for redirect."",
                    ""Level"": ""Warning"",
                    ""TimeStamp"": ""2024-05-13T13:33:50.47""
                },
                {
                    ""Id"": 1063,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-14T14:47:22.257""
                },
                {
                    ""Id"": 1064,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-14T14:47:28.17""
                },
                {
                    ""Id"": 1065,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-14T15:28:09.457""
                },
                {
                    ""Id"": 1066,
                    ""MessageTemplate"": ""An unhandled exception has occurred while executing the request."",
                    ""Level"": ""Error"",
                    ""TimeStamp"": ""2024-05-14T15:33:30.113""
                }
            ],
            ""fonteTrafico"": [63, 15, 22]
        }";
                        
            AdminDashboard dados = JsonConvert.DeserializeObject<AdminDashboard>(jsonString);

            // Falta alterar o entity para retornar os logs gerados pelo Serilog*
            await _entityFramework.AuditLogs.ToListAsync();

            return dados;
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
