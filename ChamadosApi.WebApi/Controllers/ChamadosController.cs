using ChamadosApi.Application.Chamados.Commands;
using ChamadosApi.Application.Chamados.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChamadosApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // exige JWT válido
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoService _chamadoService;

        public ChamadosController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarChamado([FromBody] CreateChamadoCommand command)
        {
            // Captura automaticamente o ID do usuário logado do token JWT
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            command.UsuarioAberturaId = userId;

            var chamado = await _chamadoService.CriarAsync(command);
            return CreatedAtAction(nameof(ObterPorId), new { id = chamado.Id }, chamado);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Tecnico")]
        public async Task<IActionResult> ListarChamados()
        {
            var chamados = await _chamadoService.ListarAsync();
            return Ok(chamados);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var chamado = await _chamadoService.ObterPorIdAsync(id);
            if (chamado == null) return NotFound();
            return Ok(chamado);
        }

        [HttpPut("{id:guid}/atribuir-tecnico")]
        [Authorize(Roles = "Administrador,Tecnico")]
        public async Task<IActionResult> AtribuirTecnico(Guid id, [FromBody] AtribuirTecnicoRequest request)
        {
            var chamado = await _chamadoService.AtribuirTecnicoAsync(id, request.TecnicoId);
            if (chamado == null) return NotFound();
            return Ok(chamado);
        }
    }

    public class AtribuirTecnicoRequest
    {
        public Guid TecnicoId { get; set; }
    }
}