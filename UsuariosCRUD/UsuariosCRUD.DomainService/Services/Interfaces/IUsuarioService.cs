using UsuariosCRUD.DomainModel.Models;

namespace UsuariosCRUD.DomainService.Services;

public interface IUsuarioService
{
    Task<Usuario> ObterAsync(long codigo);
    Task<long> CriarAsync(Usuario usuario);
    Task AlterarAsync(Usuario usuario);
    Task RemoverAsync(long codigoUsuario);
    Task AlterarSenhaAsync(long codigo, string novaSenha);
}
