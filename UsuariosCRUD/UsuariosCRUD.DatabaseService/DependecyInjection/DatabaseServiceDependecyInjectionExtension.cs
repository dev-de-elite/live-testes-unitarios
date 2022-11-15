using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsuariosCRUD.DatabaseService.DataContext;
using UsuariosCRUD.DatabaseService.Profiles;
using UsuariosCRUD.DatabaseService.Repositories;
using UsuariosCRUD.DomainService.Repositories;

namespace UsuariosCRUD.DatabaseService.DependecyInjection;

public static class DatabaseServiceDependecyInjectionExtension
{
    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDataContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IUsuarioTokenRepository, UsuarioTokenRepository>();
        services.AddAutoMapper(typeof(DatabaseServiceProfile).Assembly);
        return services;
    }
}
