using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Application.Chamados.Commands
{
    public class CreateChamadoCommand
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        // será preenchido a partir do token
        public Guid UsuarioAberturaId { get; set; }
    }
}