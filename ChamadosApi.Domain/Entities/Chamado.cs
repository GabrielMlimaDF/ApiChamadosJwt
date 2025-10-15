using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Domain.Entities
{
    public class Chamado
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataAbertura { get; private set; } = DateTime.UtcNow;

        // Quem abriu o chamado (Cliente ou Admin)
        public Guid UsuarioAberturaId { get; private set; }

        public Usuario UsuarioAbertura { get; private set; }

        // Técnico responsável (pode ser nulo inicialmente)
        public Guid? TecnicoResponsavelId { get; private set; }

        public Usuario? TecnicoResponsavel { get; private set; }

        private Chamado()
        { }

        public Chamado(string titulo, string descricao, Guid usuarioAberturaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            UsuarioAberturaId = usuarioAberturaId;
            DataAbertura = DateTime.UtcNow;
        }

        public void AtribuirTecnico(Guid tecnicoId)
        {
            TecnicoResponsavelId = tecnicoId;
        }
    }
}