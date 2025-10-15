using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Application.Chamados.Dtos
{
    public class ChamadoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
        public Guid UsuarioAberturaId { get; set; }
        public Guid? TecnicoResponsavelId { get; set; }
    }
}