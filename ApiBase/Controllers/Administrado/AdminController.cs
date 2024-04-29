using ApiBase.Contracts.Admin;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Controllers.Administrado
{
    [Authorize(Policy = "AdminOnly")]
    [Route("admin")]
    [ApiController]
    public class AdminController(IAdminRepository repository) : ControllerBase
    {
        private readonly IAdminRepository _repository = repository;

        // Retorna os logs de alterações de contas da tabelas 'AuditLogs'
        [HttpGet]
        public Task<IEnumerable<AuditLogs>> Index()
        {
            return _repository.Get();
        }

        // Para inserir uma conta do tipo 'instituição'
        [HttpPost]
        public async Task<IActionResult> Post(InstituicaoInsert instituicaoInsert)
        {
            if (await _repository.Post(instituicaoInsert))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha ao atualizar!",
                ContentType = "text/plain"
            };
        }

        // Alterar o nivel de um usuário
        [HttpPut("{userId}&{roleId}")]
        public async Task<IActionResult> Put(int userId, int roleId)
        {
            if (await _repository.Put(userId, roleId))
            {
                return NoContent();
            }

            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = "Falha ao atualizar!",
                ContentType = "text/plain"
            };
        }

        // Excluir uma conta
        [HttpDelete("/usuario/{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            if (await _repository.Delete(userId))
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
