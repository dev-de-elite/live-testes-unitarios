using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsuariosCRUD.AuthenticationService.Configurations;
using UsuariosCRUD.AuthenticationService.Services;
using UsuariosCRUD.DomainService.Authentications;

namespace UsuariosCRUD.AuthenticationService.DependencyInjection;

public static class AuthenticationServiceDependencyInjectionExtension
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("JWT"));
        services.AddSingleton<IAutenticacaoService, AutenticacaoService>();
        return services;
    }
}
