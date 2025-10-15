using ChamadosApi.Application.Usuarios.Commands;
using ChamadosApi.Application.Usuarios.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Application.Usuarios.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> CriarAsync(CreateUsuarioCommand command);

        Task<UsuarioDto?> LoginAsync(string email, string senha);

        Task<IEnumerable<UsuarioDto>> ListarAsync();
    }
}