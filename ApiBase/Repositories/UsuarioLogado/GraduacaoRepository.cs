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
            cfg.CreateMap<Graduacao, GraduacaoDto>().ReverseMap();
            cfg.CreateMap<Curso, CursoDto>().ReverseMap();
        }));

        public async Task<IEnumerable<GraduacaoDto>> GetAll(int userId)
        {
            return _mapper.Map<IEnumerable<GraduacaoDto>>(await _entityFramework.Graduacaos.Where(u => u.Usuario_Id == userId).ToListAsync());
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
                Inicio = graduacao.Inicio,
                Fim = graduacao.Fim,
                Usuario_Id = id,
                Curso_Id = graduacao.Curso_Id
            };

            Solicitacao solicitacaoModel = new()
            {
                Instituicao_Id = graduacao.InstituicaoId
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

        public async Task<IEnumerable<ListaInstituicaoDto>> ListaInstituicao()
        {
            IEnumerable<ListaInstituicaoDto> banco = await _entityFramework.Instituicao
                .Select(i => new ListaInstituicaoDto
                {
                    Nome = i.Nome,
                    Instituicao_Id = i.Instituicao_Id,
                    Cursos = _mapper.Map<IEnumerable<CursoDto>>(i.Cursos.Where(c => c.Ativo == true))
                }
                )
                .ToListAsync();

            return banco;
        }
    }
}