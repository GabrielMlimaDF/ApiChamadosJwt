using ChamadosApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosApi.Infrastructure.Mappings
{
    public class ChamadoMap : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("Chamados");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(c => c.DataAbertura)
                .IsRequired();

            // Chaves estrangeiras já estão definidas no UsuarioMap via HasMany
        }
    }
}