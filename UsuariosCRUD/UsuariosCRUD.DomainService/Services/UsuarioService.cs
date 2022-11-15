using UsuariosCRUD.DomainModel.Models;
using UsuariosCRUD.DomainService.Repositories;
using UsuariosCRUD.DomainService.Exceptions.Extensions;
using UsuariosCRUD.DomainService.Exceptions;

namespace UsuariosCRUD.DomainService.Services;

internal sealed class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public Task AlterarAsync(Usuario usuario)
    {
        ValidarModelo(usuario);
        try
        {
            return _repository.AlterarAsync(usuario);
        }
        catch (Exception exception)
        {
            throw new DomainServiceException($"Não foi possível alterar o usuário. Usuário não encontrado para o código \"{usuario.Codigo}\"", exception);
        }
    }

    public async Task AlterarSenhaAsync(long codigo, string novaSenha)
    {
        Senha novaSenhaModelo = novaSenha;

        ArgumentExceptionExtensions.ThrowIfCodigoUsuarioInvalido(codigo, nameof(codigo));
        ArgumentNullException.ThrowIfNull(novaSenha, nameof(novaSenha));
        DomainServiceException.ThrowIf(() => !novaSenhaModelo.ValidarModelo(), "A nova senha do usuário é inválida");

        var usuario = await _repository.ObterAsync(codigo);
        DomainServiceException.ThrowIfUsuarioNaoEncontrato(usuario);

        usuario!.Senha = novaSenhaModelo;
        await _repository.AlterarAsync(usuario);
    }

    public Task<long> CriarAsync(Usuario usuario)
    {
        ValidarModelo(usuario);
        return _repository.CriarAsync(usuario);
    }

    public async Task RemoverAsync(long codigoUsuario)
    {
        var usuario = await _repository.ObterAsync(codigoUsuario);
        DomainServiceException.ThrowIfUsuarioNaoEncontrato(usuario);
        await _repository.RemoverAsync(usuario!);
    }

    public async Task<Usuario> ObterAsync(long codigo)
    {
        ArgumentExceptionExtensions.ThrowIfCodigoUsuarioInvalido(codigo, nameof(codigo));
        var usuario = await _repository.ObterAsync(codigo);
        DomainServiceException.ThrowIfUsuarioNaoEncontrato(usuario);
        return usuario!;
    }

    private static void ValidarModelo(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario, nameof(usuario));
        ArgumentExceptionExtensions.ThrowIf(() => !usuario.ValidarModelo(), "Usuário é inválido", nameof(usuario));
    }
}
