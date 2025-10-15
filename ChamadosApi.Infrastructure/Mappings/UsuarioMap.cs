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
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.SenhaHash)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(u => u.Tipo)
                .IsRequired();

            // Relacionamentos
            builder.HasMany(u => u.ChamadosAbertos)
                .WithOne(c => c.UsuarioAbertura)
                .HasForeignKey(c => c.UsuarioAberturaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ChamadosDesignados)
                .WithOne(c => c.TecnicoResponsavel)
                .HasForeignKey(c => c.TecnicoResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}