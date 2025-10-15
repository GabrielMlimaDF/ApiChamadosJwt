using ChamadosApi.Application.Chamados.Commands;
using ChamadosApi.Application.Chamados.Dtos;
using ChamadosApi.Application.Chamados.Services;
using ChamadosApi.Domain.Entities;
using ChamadosApi.Infrastructure.Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Infrastructure.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly AppDbContext _context;

        public ChamadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChamadoDto> CriarAsync(CreateChamadoCommand command)
        {
            var chamado = new Chamado(command.Titulo, command.Descricao, command.UsuarioAberturaId);
            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();
            return MapToDto(chamado);
        }

        public async Task<IEnumerable<ChamadoDto>> ListarAsync()
        {
            return await _context.Chamados
                .AsNoTracking()
                .Select(c => MapToDto(c))
                .ToListAsync();
        }

        public async Task<ChamadoDto?> ObterPorIdAsync(Guid id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            return chamado == null ? null : MapToDto(chamado);
        }

        public async Task<ChamadoDto?> AtribuirTecnicoAsync(Guid chamadoId, Guid tecnicoId)
        {
            var chamado = await _context.Chamados.FindAsync(chamadoId);
            if (chamado == null) return null;

            chamado.AtribuirTecnico(tecnicoId);
            await _context.SaveChangesAsync();
            return MapToDto(chamado);
        }

        private static ChamadoDto MapToDto(Chamado c) => new()
        {
            Id = c.Id,
            Titulo = c.Titulo,
            Descricao = c.Descricao,
            DataAbertura = c.DataAbertura,
            UsuarioAberturaId = c.UsuarioAberturaId,
            TecnicoResponsavelId = c.TecnicoResponsavelId
        };
    }
}