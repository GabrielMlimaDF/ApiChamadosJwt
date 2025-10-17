using ChamadosApi.Application.Auth.Services;
using ChamadosApi.Application.Usuarios.Commands;
using ChamadosApi.Application.Usuarios.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChamadosApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;

        public UsuariosController(IUsuarioService usuarioService, IAuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUsuarioCommand command)
        {
            var usuario = await _usuarioService.CriarAsync(command);
            return CreatedAtAction(nameof(GetAll), new { id = usuario.Id }, usuario);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var usuario = await _usuarioService.LoginAsync(login.Email, login.Senha);
            if (usuario == null) return Unauthorized("Credenciais inválidas.");

            var token = _authService.GerarToken(usuario); // ou chamar a função GerarToken direto aqui
            return Ok(new { usuario, token });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            Guid usuarioId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var usuario = await _usuarioService.ObterPorIdAsync(usuarioId);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}