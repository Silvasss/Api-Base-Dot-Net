using ApiBase.Contracts.Admin;
using ApiBase.Dtos;
using ApiBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase.Controllers.Administrado
{
    [Authorize(Policy = "AdminOnly")]
    [Route("admin")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Produces("application/json")]
    public class AdminController(IAdminRepository repository) : ControllerBase
    {
        private readonly IAdminRepository _repository = repository;

        /// <summary>
        /// Retorna lista com os logs de alterações
        /// </summary>
        /// <response code="200">Lista de objetos Log</response>
        /// <response code="404">Nenhum encontrado</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<AuditLogs>>> Index()
        {
            IEnumerable<AuditLogs> logs = await _repository.Get();

            if (!logs.Any())
            {
                return NotFound();
            }

            return Ok(logs);
        }

        /// <summary>
        /// Criar uma conta do tipo 'instituição'
        /// </summary>
        /// <remarks>              
        /// Exemplo de request:        
        /// 
        ///     {
        ///         "Usuario": "NomeDaInstituição",
        ///         "Password": "SenhaDaInstituição"
        ///     } 
        /// Os Plus Codes funcionam como endereços físicos. São baseados em latitude e longitude e usam um 
        /// sistema de grade simples e um conjunto de 20 caracteres alfanuméricos.  
        /// </remarks>
        /// <param name="instituicaoInsert">Objeto Instituição</param>
        /// <response code="201">Criada</response>
        /// <response code="400">Falha na validação da autenticação</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(InstituicaoDtoCreate instituicaoInsert)
        {
            if (await _repository.Post(instituicaoInsert))
            {
                return Created();
            }

            return BadRequest();
        }

        /// <summary>
        /// Alterar o nível de um usuário
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <response code="204">Atualizado</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{userId}&{roleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(int userId, int roleId)
        {
            if (await _repository.Put(userId, roleId))
            {
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// Excluir uma conta
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="204">Apagado</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("/usuario/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int userId)
        {
            if (await _repository.Delete(userId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
