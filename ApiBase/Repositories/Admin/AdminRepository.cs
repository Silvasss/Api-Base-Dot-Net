using ApiBase.Contracts.Admin;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Helpers;
using ApiBase.Models;
using ApiBase.Pagination;
using Bogus;
using Bogus.Extensions;
using Google.OpenLocationCode;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography;

namespace ApiBase.Repositories.Admin
{
    public class AdminRepository(DataContextEF dataContext, IConfiguration config) : IAdminRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;
        private readonly AuthHelper _authHelper = new(config, dataContext);

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

        public async Task<object> Get()
        {
            // Criação manual do objeto
            var analyticSistema = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "title", "Total Visualizações" },
                    { "count", "1,42,236" },
                    { "percentage", 59.3 },
                    { "color", null }
                },
                new Dictionary<string, object>
                {
                    { "title", "Total Usuários" },
                    { "count", await _entityFramework.Usuarios.AsNoTracking().CountAsync() },
                    { "percentage", 70.5 },
                    { "color", null }
                },
                new Dictionary<string, object>
                {
                    { "title", "Total Instituições" },
                    { "count", await _entityFramework.Instituicao.AsNoTracking().CountAsync() },
                    { "percentage", 27.4 },
                    { "color", "warning" }
                },
                new Dictionary<string, object>
                {
                    { "title", "Total Logs" },
                    { "count", await _entityFramework.Logs.AsNoTracking().CountAsync() },
                    { "percentage", 7.4 },
                    { "color", "warning" }
                }
            };

            var fonteTrafico = new List<int> { 63, 15, 22 };

            return new Dictionary<string, object>
            {
                { "analyticSistema", analyticSistema },
                { "fonteTrafico", fonteTrafico },
                { "logs", await _entityFramework.Logs
                    .AsNoTracking()
                    .OrderByDescending(e => e.TimeStamp)
                    .Take(10)
                    .ToListAsync()
                }
            };
        }

        public async Task<PagedList<object>> GetAllInstituicao(VisitanteParameters visitanteParams)
        {
            var generico = await _entityFramework.Set<InstituicaoEF>()
                .AsNoTracking()
                .Select(x => new
                {
                    x.Instituicao_Id,
                    x.Nome,
                    x.PlusCode,
                    x.Ativo,
                    x.CreatedAt
                })
                .ToListAsync();

            var retornoQuery = generico.AsQueryable();

            return PagedList<object>.ToPagedList(retornoQuery, visitanteParams.PageNumber, visitanteParams.PageSize);
        }

        public async Task<object> GetInfoInstituicao(int id)
        {
            return await _entityFramework.Instituicao
                .AsNoTracking()
                .Where(i => i.Instituicao_Id == id)
                .Include(i => i.Cursos)
                .Select(n => new
                {
                    n.Instituicao_Id,
                    n.Nome,
                    n.PlusCode,
                    n.Ativo,
                    n.CreatedAt,
                    n.UpdatedAt,
                    Cursos = n.Cursos.Select(c => new
                    {
                        c.Nome,
                        c.CreatedAt,
                        c.UpdatedAt
                    })
                })
                .FirstAsync();
        }

        public async Task<PagedList<object>> GetAllLogs(VisitanteParameters visitanteParams)
        {
            var generico = await _entityFramework.Set<SerilogEntry>().AsNoTracking().ToListAsync();

            var retornoQuery = generico.OrderByDescending(e => e.TimeStamp).AsQueryable();

            return PagedList<object>.ToPagedList(retornoQuery, visitanteParams.PageNumber, visitanteParams.PageSize);
        }

        public async Task<PagedList<object>> GetAllUsuarios(VisitanteParameters visitanteParams, int userId)
        {
            var generico = await _entityFramework.Set<Usuario>().AsNoTracking()
                .Where(u => u.Usuario_Id != userId)
                .Select(x => new
                {
                    x.Usuario_Id,
                    x.Nome,
                    x.PlusCode,
                    x.CreatedAt,
                    x.Auth_Id
                })
                .ToListAsync();

            var retornoQuery = generico.AsQueryable();

            return PagedList<object>.ToPagedList(retornoQuery, visitanteParams.PageNumber, visitanteParams.PageSize);
        }

        public async Task<object> GetUsuario(int id)
        {
            return await _entityFramework.Usuarios
                .AsNoTracking()
                .Where(u => u.Usuario_Id == id)
                .Include(u => u.Experiencias)
                .Include(u => u.graduacoes)
                .Select(r => new
                {
                    r.Usuario_Id,
                    r.Nome,
                    r.PlusCode,
                    r.Pais,
                    r.Tipo_Conta_Id,
                    Experiencias = r.Experiencias.Select(e => new
                    {
                        e.Empresa,
                        e.PlusCode,
                        e.Vinculo,
                        e.Funcao,
                        e.Inicio,
                        e.Fim,
                        e.Responsabilidade
                    }),
                    Graduacoes = r.graduacoes.Select(g => new
                    {
                        g.Situacao,
                        g.Inicio,
                        g.Fim,
                        g.InstituicaoNome,
                        g.CursoNome
                    })
                })
                .AsSplitQuery()
                .FirstAsync();
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
                    Tipo_Conta_Id = 3, // Tipo Instituição
                    Ativo = true
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
            Usuario? usuarioDb = await _entityFramework.Usuarios.Where(u => u.Usuario_Id == userId).FirstOrDefaultAsync();

            if (usuarioDb != null)
            {
                usuarioDb.Tipo_Conta_Id = roleId;

                await _entityFramework.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUsuario(int userId)
        {
            Auth? authDb = await _entityFramework.Auth.Where(a => a.UsuarioPerfil.Usuario_Id == userId)
                .Include(a => a.UsuarioPerfil)
                .ThenInclude(x => x.Experiencias)
                .Include(u => u.UsuarioPerfil.graduacoes)
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

        public async Task<bool> GerarPopulacao(int quantidade)
        {
            var fake = new Faker();

            List<string> tiposDeServicosSetorTerciario = [
                "Turismo",
                "Serviços bancários",
                "Restaurantes",
                "Hospitais",
                "Serviços de consultoria",
                "Corretagem de imóveis",
                "Serviços públicos",
                "Comércio de bens",
                "Alojamento e alimentação",
                "Transportes",
                "Correios",
                "Telecomunicações",
                "Financeiras e Seguros",
                "Serviços domésticos",
                "Administração Pública",
                "Defesa",
                "Seguridade Social",
                "Educação",
                "Saúde",
                "Serviços urbanos",
                "Supermercados",
                "Escolas",
                "Serviços de telemarketing",
                "Serviços de limpeza",
                "Centros comerciais",
                "Bancos",
                "Hotéis",
                "Agências de turismo",
                "Serviços logísticos",
                "Serviços de manutenção",
                "Serviços administrativos",
                "Serviços de meio ambiente",
                "Serviços de comunicação",
                "Serviços de construção e engenharia",
                "Serviços de distribuição",
                "Serviços de tecnologia da informação",
                "Serviços de pesquisa e desenvolvimento"
            ];

            for (int i = 0; i < quantidade; i++)
            {
                Auth novoAuth = new()
                {
                    Usuario = fake.Internet.UserName(),
                    PasswordHash = fake.Random.Bytes(10),
                    PasswordSalt = fake.Random.Bytes(10),
                    UsuarioPerfil = new Usuario
                    {
                        Nome = fake.Name.FirstName(),
                        Pais = fake.Address.Country(),
                        PlusCode = OpenLocationCode.Encode(fake.Address.Latitude(), fake.Address.Longitude()),
                        SobreMin = fake.Lorem.Paragraph(3),
                        CargoPrincipal = fake.Lorem.Sentence(50).ClampLength(10, 50),
                        Email = fake.Internet.Email(),
                        PortfolioURL = fake.Internet.Url(),
                        Experiencia = fake.Random.ListItem(["Start Up", "6 months", "1 Year", "2 Year", "3 Year", "4 Year", "5 Year"])
                    }
                };

                await _entityFramework.AddAsync(novoAuth);

                await _entityFramework.SaveChangesAsync();

                for (int k = 0; k < fake.Random.Number(1, 10); k++)
                {
                    var (ativo, startDate, endDate, situacaoGraduacao) = GenerateDateRange();

                    Experiencia experiencia = new()
                    {
                        Usuario_Id = novoAuth.UsuarioPerfil.Usuario_Id,
                        Setor = fake.Random.ListItem(tiposDeServicosSetorTerciario),
                        Empresa = fake.Company.CompanyName(),
                        PlusCode = OpenLocationCode.Encode(fake.Address.Latitude(), fake.Address.Longitude()),
                        Vinculo = fake.Random.ListItem(["CLT", "Estágio", "Empregado Doméstico", "Autônomo", "Pessoa Jurídica (PJ)", "Home Office"]),
                        Funcao = fake.Name.JobDescriptor(),
                        Ativo = ativo,
                        Inicio = startDate,
                        Fim = ativo ? null : endDate,
                        Responsabilidade = fake.Lorem.Sentence(150).ClampLength(20, 150)
                    };

                    await _entityFramework.AddAsync(experiencia);

                    await _entityFramework.SaveChangesAsync();
                }

                for (int k = 0; k < fake.Random.Number(1, 4); k++)
                {
                    var (ativo, startDate, endDate, situacaoGraduacao) = GenerateDateRange();

                    var (Instituicao_Id, InstituicaoNome, Curso_Id, CursoNome) = await InstituicaoSelecao();

                    Graduacao graduacao = new()
                    {
                        Usuario_Id = novoAuth.UsuarioPerfil.Usuario_Id,
                        Situacao = situacaoGraduacao,
                        Curso_Id = Curso_Id,
                        CursoNome = CursoNome,
                        InstituicaoId = Instituicao_Id,
                        InstituicaoNome = InstituicaoNome,
                        Tipo = fake.Random.ListItem(["Bacharelado", "Licenciatura", "Tecnólogo"]),
                        Status = fake.PickRandom<Status>(),
                        Inicio = startDate,
                        Fim = situacaoGraduacao == SituacaoGraduacao.Concluído ? endDate : null,
                    };

                    Solicitacao solicitacaoModel = new()
                    {
                        Instituicao_Id = graduacao.InstituicaoId
                    };

                    graduacao.Solicitacao = solicitacaoModel;

                    await _entityFramework.AddAsync(graduacao);

                    await _entityFramework.SaveChangesAsync();
                }
            }

            return true;
        }

        public (bool, DateTime, DateTime, SituacaoGraduacao) GenerateDateRange()
        {
            var faker = new Faker();

            // Gera a primeira data
            var startDate = faker.Date.Between(new DateTime(2002, 10, 1), DateTime.Now);

            // Gera a segunda data, garantindo que seja maior ou igual à primeira data
            var endDate = faker.Date.Between(startDate, DateTime.Now);

            bool ativo = faker.Random.Bool();

            // Graduação
            SituacaoGraduacao situacaoGraduacao = faker.PickRandom<SituacaoGraduacao>();

            return (ativo, startDate, endDate, situacaoGraduacao);
        }

        public async Task<(int, string, int, string)> InstituicaoSelecao()
        {
            var faker = new Faker();

            var instituicaoDb = await _entityFramework.Instituicao
                .AsNoTracking()
                .Include(x => x.Cursos)
                .Select(x => new
                {
                    x.Instituicao_Id,
                    x.Nome,
                    Cursos = x.Cursos.Select(c => new
                    {
                        c.Curso_Id,
                        c.Nome
                    }).ToArray()
                })
                .ToListAsync();

            var selecionado = faker.Random.CollectionItem(instituicaoDb);

            var cursoSelecionado = faker.Random.ArrayElement(selecionado.Cursos);

            int Instituicao_Id = selecionado.Instituicao_Id;
            string InstituicaoNome = selecionado.Nome;
            int Curso_Id = cursoSelecionado.Curso_Id;
            string CursoNome = cursoSelecionado.Nome;

            return (Instituicao_Id, InstituicaoNome, Curso_Id, CursoNome);
        }
    }
}
