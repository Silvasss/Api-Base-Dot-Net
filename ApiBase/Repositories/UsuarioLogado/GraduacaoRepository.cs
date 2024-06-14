using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class GraduacaoRepository(IConfiguration config) : IGraduacaoRepository
    {
        private readonly DataContextEF _entityFramework = new(config);
        private readonly Mapper _mapper = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Curso, CursoDto>().ReverseMap();
        }));

        public async Task<ListaInfos> Get(int userId)
        {
            ListaInfos resposta = new()
            {
                Graduacoes = await _entityFramework.Graduacaos
                    .Where(u => u.Usuario_Id == userId)
                    .Select(u => new GraduacaoDto
                    {
                        Graduacao_Id = u.Graduacao_Id,
                        Situacao = u.Situacao,
                        Tipo = u.Tipo,
                        Inicio = u.Inicio,
                        Fim = u.Fim,
                        Curso_Id = u.Curso_Id,
                        CursoNome = u.CursoNome,
                        InstituicaoId = u.InstituicaoId,
                        InstituicaoNome = u.InstituicaoNome
                    }).ToListAsync(),
                ListaInstituicoes = await _entityFramework.Instituicao
                    .Select(i => new ListaInstituicaoDto
                    {
                        Nome = i.Nome,
                        Instituicao_Id = i.Instituicao_Id,
                        Cursos = _mapper.Map<IEnumerable<CursoDto>>(i.Cursos.Where(c => c.Ativo == true))
                    }).ToListAsync()
            };

            return resposta;
        }

        public async Task<bool> Delete(int id, int userId)
        {
            Graduacao? graduacaoDb = await _entityFramework.Graduacaos.Where(g => g.Graduacao_Id == id && g.Usuario_Id == userId).FirstOrDefaultAsync();

            if (graduacaoDb != null)
            {
                _entityFramework.Remove(graduacaoDb);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Post(GraduacaoDto graduacao, int id)
        {
            Graduacao graduacaoDb = new()
            {
                Situacao = graduacao.Situacao,
                Tipo = graduacao.Tipo,
                Inicio = graduacao.Inicio,
                Fim = graduacao.Fim,
                Usuario_Id = id,
                Curso_Id = graduacao.Curso_Id,
                CursoNome = graduacao.CursoNome,
                InstituicaoId = graduacao.InstituicaoId,
                InstituicaoNome = graduacao.InstituicaoNome
            };


            Solicitacao solicitacaoModel = new()
            {
                Instituicao_Id = graduacao.InstituicaoId,
                Ativo = true
            };

            graduacaoDb.Solicitacao = solicitacaoModel;

            await _entityFramework.AddAsync(graduacaoDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Put(GraduacaoDto graduacao, int id)
        {
            Graduacao? graduacaoDb = await _entityFramework.Graduacaos.Where(g => g.Graduacao_Id == graduacao.Graduacao_Id && g.Usuario_Id == id).FirstOrDefaultAsync();

            if (graduacaoDb != null)
            {
                graduacaoDb.Situacao = graduacao.Situacao;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}