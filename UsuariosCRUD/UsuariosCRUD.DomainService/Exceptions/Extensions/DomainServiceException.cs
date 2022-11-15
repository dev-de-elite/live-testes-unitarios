using UsuariosCRUD.DomainModel.Models;

namespace UsuariosCRUD.DomainService.Exceptions;

public sealed partial class DomainServiceException : Exception
{
    public static void ThrowIfUsuarioNaoEncontrato(Usuario? usuario) =>
        ThrowIfNull(usuario, "Usuário não encontrado");
}
