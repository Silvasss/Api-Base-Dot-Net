﻿using ApiBase.Models;

namespace ApiBase.Contracts
{
    public interface IExperienciaRepository
    {
        Task<bool> Post(Experiencia experiencia, int id);
        Task<bool> Put(Experiencia experiencia, int id);
        Task<bool> Delete(int id, int Experienciaid);
    }
}
