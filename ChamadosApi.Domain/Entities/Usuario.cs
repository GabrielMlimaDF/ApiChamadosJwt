using ChamadosApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public TipoUsuario Tipo { get; private set; }

        // Relacionamentos
        public ICollection<Chamado> ChamadosAbertos { get; private set; } = new List<Chamado>();

        public ICollection<Chamado> ChamadosDesignados { get; private set; } = new List<Chamado>();

        private Usuario()
        { } // EF Core

        public Usuario(string nome, string email, string senhaHash, TipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Tipo = tipo;
        }
    }
}