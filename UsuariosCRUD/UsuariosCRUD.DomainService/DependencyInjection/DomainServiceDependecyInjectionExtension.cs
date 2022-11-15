using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsuariosCRUD.DomainService.Services;

namespace UsuariosCRUD.DomainService.DependencyInjection;

public static class DomainServiceDependecyInjectionExtension
{
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddTransient<IUsuarioAutenticacaoService, UsuarioAutenticacaoService>();
        return services;
    }
}
