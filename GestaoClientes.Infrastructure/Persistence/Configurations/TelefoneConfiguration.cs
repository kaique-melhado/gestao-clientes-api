using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoClientes.Infrastructure.Persistence.Configurations;

public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
{
    public void Configure(EntityTypeBuilder<Telefone> builder)
    {
        builder
            .ToTable("Telefones");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DDD)
            .IsRequired()
            .HasMaxLength(2)
            .HasColumnType("CHAR(2)");

        builder.Property(x => x.Numero)
            .IsRequired()
            .HasMaxLength(9)
            .HasColumnType("NVARCHAR(9)");

        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasColumnType("INT");

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Telefones)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}