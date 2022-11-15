using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsuariosCRUD.DatabaseService.Entities;

namespace UsuariosCRUD.DatabaseService.EntitiesConfiguration;

internal class UsuarioTokenEntityConfiguration : IEntityTypeConfiguration<UsuarioTokenEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioTokenEntity> builder)
    {
        builder.ToTable("Tokens");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Token).IsRequired();
        builder.Property(t => t.DataExpiracao).IsRequired();
        builder.Property(t => t.UsuarioId).IsRequired();

        builder.HasOne(t => t.Usuario).WithMany(u => u.Tokens).HasForeignKey(t => t.UsuarioId);
    }
}
