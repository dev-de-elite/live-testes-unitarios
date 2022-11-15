using UsuariosCRUD.DomainService.Models;

namespace UsuariosCRUD.DomainService.Repositories;

public interface IUsuarioTokenRepository
{
    Task<TokenUsuario?> ObterValidoPorUsuarioAsync(long codigoUsuario);
    Task<bool> ExisteTokenAtivoPorUsuarioAsync(long codigoUsuario);
    Task CriarAsync(TokenUsuario tokenUsuario);
}
