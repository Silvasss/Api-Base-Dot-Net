﻿using ApiBase.Contracts.UsuarioLogado;
using ApiBase.Data;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiBase.Repositories.UsuarioLogado
{
    public class ExperienciaRepository(DataContextEF dataContext) : IExperienciaRepository
    {
        private readonly DataContextEF _entityFramework = dataContext;

        public async Task<IEnumerable<object>> GetAll(int userId)
        {
            return await _entityFramework.Experiencia
                .AsNoTracking()
                .Where(u => u.Usuario_Id == userId)
                .Select(e => new
                {
                    e.Experiencia_Id,
                    e.Setor,
                    e.Empresa,
                    e.PlusCode,
                    e.Vinculo,
                    e.Funcao,
                    e.Inicio,
                    e.Fim,
                    e.Responsabilidade,
                    e.Ativo
                })
                .ToListAsync();
        }

        public async Task<bool> Delete(int id, int Experienciaid)
        {
            Experiencia? experienciaDb = await _entityFramework.Experiencia.Where(e => e.Experiencia_Id == Experienciaid && e.Usuario_Id == id).FirstOrDefaultAsync();

            if (experienciaDb != null)
            {
                _entityFramework.Remove(experienciaDb);

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Post(ExperienciaDto experiencia, int id)
        {
            Experiencia experienciaDb = new()
            {
                Usuario_Id = id,
                Setor = experiencia.Setor,
                Empresa = experiencia.Empresa,
                PlusCode = experiencia.PlusCode,
                Vinculo = experiencia.Vinculo,
                Funcao = experiencia.Funcao,
                Responsabilidade = experiencia.Responsabilidade,
                Ativo = experiencia.Ativo,
                Inicio = experiencia.Inicio,
                Fim = experiencia.Fim
            };

            await _entityFramework.AddAsync(experienciaDb);

            await _entityFramework.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Put(ExperienciaDto experiencia, int id)
        {
            Experiencia? experienciaDb = await _entityFramework.Experiencia.Where(e => e.Experiencia_Id == experiencia.Experiencia_Id && e.Usuario_Id == id).FirstOrDefaultAsync();

            if (experienciaDb != null)
            {
                experienciaDb.Setor = experiencia.Setor;
                experienciaDb.PlusCode = experiencia.PlusCode;
                experienciaDb.Vinculo = experiencia.Vinculo;
                experienciaDb.Funcao = experiencia.Funcao;
                experienciaDb.Responsabilidade = experiencia.Responsabilidade;
                experienciaDb.Ativo = experiencia.Ativo;
                experienciaDb.Inicio = experiencia.Inicio;
                experienciaDb.Fim = experiencia.Fim;

                if (await _entityFramework.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}