using UsuariosCRUD.DomainService.Models;

namespace UsuariosCRUD.DomainService.Services;

public interface IUsuarioAutenticacaoService
{
    Task<TokenUsuario> GerarTokenAsync(long codigoUsuario);
    Task<TokenUsuario> ObterTokenValidoAsync(long codigoUsuario);
}
