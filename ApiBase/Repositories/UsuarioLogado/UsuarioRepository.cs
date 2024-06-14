using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class UsuarioRepository(IConfiguration config) : IUsuarioRepository
    {
        private readonly DataContextEF _entityFramework = new(config);

        public async Task<bool> Delete(int id)
        {
            Usuario? dados = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == id).FirstAsync();

            Auth? authDb = await _entityFramework.Auth.Where(a => a.Auth_id == dados.Auth_Id).FirstAsync();

            _entityFramework.Auth.Remove(authDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<ActionResult<UsuarioIndexDto>> Get(int id)
        {
            UsuarioDto usuario = await _entityFramework.Usuarios.Where(u => u.Usuario_Id == id)
                .Select(u => new UsuarioDto
                {
                    Nome = u.Nome,
                    Pais = u.Pais,
                    PlusCode = u.PlusCode,
                    SobreMin = u.SobreMin,
                    CargoPrincipal = u.CargoPrincipal,
                    Email = u.Email,
                    PortfolioURL = u.PortfolioURL,
                    Experiencia = u.Experiencia,
                })
                .FirstAsync();

            UsuarioIndexDto reposta = new()
            {
                Nome = usuario.Nome,
                Pais = usuario.Pais,
                PlusCode = usuario.PlusCode,
                SobreMin = usuario.SobreMin,
                CargoPrincipal = usuario.CargoPrincipal,
                Email = usuario.Email,
                PortfolioURL = usuario.PortfolioURL,
                Experiencia = usuario.Experiencia,
                Educacao = await _entityFramework.Graduacaos
                    .Where(u => u.Usuario_Id == id)
                    .Select(u => new GraduacaoDto
                    {
                        Tipo = u.Tipo,
                        Inicio = u.Inicio,
                        Fim = u.Fim,
                        InstituicaoNome = u.InstituicaoNome
                    }).ToListAsync(),
                Emprego = await _entityFramework.Experiencia
                    .Where(e => e.Usuario_Id == id)
                    .Select(e => new ExperienciaDto
                    {
                        Funcao = e.Funcao,
                        Inicio = e.Inicio,
                        Fim = e.Fim,
                        Responsabilidade = e.Responsabilidade
                    }).ToListAsync()
            };

            return reposta;
        }

        public async Task<bool> Put(UsuarioDto user)
        {
            Usuario? userDb = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == user.Usuario_Id).FirstAsync();

            userDb.Nome = user.Nome;
            userDb.Pais = user.Pais;
            userDb.PlusCode = user.PlusCode;
            userDb.SobreMin = user.SobreMin;
            userDb.CargoPrincipal = user.CargoPrincipal;
            userDb.Email = user.Email;
            userDb.PortfolioURL = user.PortfolioURL;
            userDb.Experiencia = user.Experiencia;

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PutConfiguracoes(List<string> temp, int userId)
        {
            Usuario? userDb = await _entityFramework.Usuarios.Where(a => a.Usuario_Id == userId).FirstAsync();

            userDb.ConfiguracoesConta = temp;

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<UsuarioDto> GetConfiguracoes(int id)
        {
            return await _entityFramework.Usuarios.Where(u => u.Usuario_Id == id)
                .Select(u => new UsuarioDto
                {
                    ConfiguracoesConta = u.ConfiguracoesConta
                })
                .FirstAsync();
        }
    }
}
