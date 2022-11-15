using UsuariosCRUD.DomainModel.Models;

namespace UsuariosCRUD.DomainService.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterAsync(long codigo);
    Task<long> CriarAsync(Usuario usuario);
    Task AlterarAsync(Usuario usuario);
    Task RemoverAsync(Usuario usuario);
}
