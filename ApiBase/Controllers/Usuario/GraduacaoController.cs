﻿using ApiBase.Contracts.Usuario;
using ApiBase.Data;
using ApiBase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace ApiBase.Controllers.Usuario
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("api/v1/graduacao")]
    [ApiController]
    public class GraduacaoController(IGraduacaoRepository repository) : ControllerBase
    {
        private readonly IGraduacaoRepository _repository = repository;

        [HttpPost]
        public async Task<IActionResult> Post(Graduacao graduacao)
        {
            if (await _repository.Post(graduacao, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na graduacao!",
                ContentType = "text/plain"
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(Graduacao graduacao)
        {
            if (await _repository.Put(graduacao, int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na graduacao!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repository.Delete(id))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }
    }
}
