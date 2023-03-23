using Microsoft.EntityFrameworkCore;
using UsuariosCRUD.DatabaseService.Entities;

namespace UsuariosCRUD.DatabaseService.DataContext;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions options) : base(options) { }

    public DbSet<UsuarioEntity> Usuarios { get; set; } = default!;
    public DbSet<UsuarioTokenEntity> UsuariosToken { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDataContext).Assembly);
    }
}
