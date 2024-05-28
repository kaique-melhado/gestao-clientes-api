using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoClientes.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder
            .ToTable("Clientes");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.NomeCompleto)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR(100)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("NVARCHAR(100)");

        builder.HasIndex(x => x.Email, "IX_Cliente_Email")
            .IsUnique();

        builder
            .HasMany(x => x.Telefones)
            .WithOne(x => x.Cliente)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
