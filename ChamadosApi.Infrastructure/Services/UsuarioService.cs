using ChamadosApi.Application.Usuarios.Commands;
using ChamadosApi.Application.Usuarios.Dtos;
using ChamadosApi.Application.Usuarios.Services;
using ChamadosApi.Domain.Entities;
using ChamadosApi.Infrastructure.Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Infrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDto> CriarAsync(CreateUsuarioCommand command)
        {
            // Verificar se e-mail já existe
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == command.Email);
            if (existe)
                throw new InvalidOperationException("E-mail já cadastrado.");

            var hash = GerarHash(command.Senha);

            var usuario = new Usuario(command.Nome, command.Email, hash, command.Tipo);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToDto(usuario);
        }

        public async Task<UsuarioDto?> LoginAsync(string email, string senha)
        {
            var hash = GerarHash(senha);
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.SenhaHash == hash);

            return usuario == null ? null : MapToDto(usuario);
        }

        public async Task<UsuarioDto?> ObterPorIdAsync(Guid usuarioId)
        {
            return await _context.Usuarios
                .Where(u => u.Id == usuarioId)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Tipo = u.Tipo
                })
                .FirstOrDefaultAsync();
        }

        private static string GerarHash(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static UsuarioDto MapToDto(Usuario u) => new()
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Tipo = u.Tipo
        };
    }
}