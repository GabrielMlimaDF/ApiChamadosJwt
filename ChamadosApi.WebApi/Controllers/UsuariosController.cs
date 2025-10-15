using ChamadosApi.Application.Usuarios.Commands;
using ChamadosApi.Application.Usuarios.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChamadosApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUsuarioCommand command)
        {
            var usuario = await _usuarioService.CriarAsync(command);
            return CreatedAtAction(nameof(GetAll), new { id = usuario.Id }, usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var usuario = await _usuarioService.LoginAsync(login.Email, login.Senha);
            if (usuario == null) return Unauthorized("Credenciais inválidas.");
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.ListarAsync();
            return Ok(usuarios);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}