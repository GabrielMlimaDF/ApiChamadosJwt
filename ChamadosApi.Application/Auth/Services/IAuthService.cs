using ChamadosApi.Application.Usuarios.Dtos;

namespace ChamadosApi.Application.Auth.Services
{
    public interface IAuthService
    {
        string GerarToken(UsuarioDto usuario);
    }
}