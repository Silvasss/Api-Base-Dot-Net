using ApiBase.Contracts.Usuario;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Controllers.Usuario
{
    [Authorize(Policy = "UsuarioOnly")]
    [Route("api/v1/user")]
    [ApiController]
    public class UsuarioController(IUsuarioRepository repository) : ControllerBase
    {
        private readonly IUsuarioRepository _repository = repository;

        // Retorna todos os dados criado pelo usuário
        [HttpGet]
        public async Task<ActionResult<UserCompleto>> Get()
        {
            return await _repository.Get(int.Parse(User.Claims.First(x => x.Type == "userId").Value));
        }

        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {            
            user.Id = int.Parse(User.Claims.First(x => x.Type == "userId").Value);

            if (await _repository.Put(user))
            {
                return Ok();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar o usuário!",
                ContentType = "text/plain"
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserForUpdatePassword updatePasswort)
        {
            UserForLoginDto userForSetPassword = new()
            {
                Usuario = User.Claims.First(x => x.Type == "nome").Value,
                Password = updatePasswort.PasswordNova
            };

            if (await _repository.Post(userForSetPassword))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha ao atualizar a senha!",
                ContentType = "text/plain"
            };
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            if (await _repository.Delete(int.Parse(User.Claims.First(x => x.Type == "userId").Value)))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "Falha na solicitação!",
                ContentType = "text/plain"
            };
        }
    }
}
