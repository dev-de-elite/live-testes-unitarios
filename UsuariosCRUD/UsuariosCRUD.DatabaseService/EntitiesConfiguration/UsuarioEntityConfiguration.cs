using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsuariosCRUD.DatabaseService.Entities;

namespace UsuariosCRUD.DatabaseService.EntitiesConfiguration;

internal class UsuarioEntityConfiguration : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.PrimeiroNome).IsRequired();
        builder.Property(u => u.UltimoNome).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.NomeUsuario).IsRequired();
        builder.Property(u => u.Senha).IsRequired();

        builder.HasMany(u => u.Tokens).WithOne(t => t.Usuario).HasForeignKey(t => t.UsuarioId);
    }
}
