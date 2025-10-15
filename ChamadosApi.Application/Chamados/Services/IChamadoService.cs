using ChamadosApi.Application.Chamados.Commands;
using ChamadosApi.Application.Chamados.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Application.Chamados.Services
{
    public interface IChamadoService
    {
        Task<ChamadoDto> CriarAsync(CreateChamadoCommand command);

        Task<IEnumerable<ChamadoDto>> ListarAsync();

        Task<ChamadoDto?> ObterPorIdAsync(Guid id);

        Task<ChamadoDto?> AtribuirTecnicoAsync(Guid chamadoId, Guid tecnicoId);
    }
}