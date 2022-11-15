using UsuariosCRUD.DomainModel.Models;
using UsuariosCRUD.DomainService.Authentications;
using UsuariosCRUD.DomainService.Exceptions;
using UsuariosCRUD.DomainService.Exceptions.Extensions;
using UsuariosCRUD.DomainService.Models;
using UsuariosCRUD.DomainService.Repositories;

namespace UsuariosCRUD.DomainService.Services;

internal sealed class UsuarioAutenticacaoService : IUsuarioAutenticacaoService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioTokenRepository _usuarioTokenRepository;
    private readonly IAutenticacaoService _autenticacaoService;

    public UsuarioAutenticacaoService(
        IUsuarioRepository usuarioRepository,
        IAutenticacaoService autenticacaoService,
        IUsuarioTokenRepository usuarioTokenRepository)
    {
        _usuarioRepository = usuarioRepository;
        _autenticacaoService = autenticacaoService;
        _usuarioTokenRepository = usuarioTokenRepository;
    }

    public async Task<TokenUsuario> GerarTokenAsync(long codigoUsuario)
    {
        ArgumentExceptionExtensions.ThrowIfCodigoUsuarioInvalido(codigoUsuario, nameof(codigoUsuario));

        var token = await RecuperarTokenValidoAsync(codigoUsuario);
        if (token is not null) 
            return token!;

        var usuario = await ObterUsuarioAsync(codigoUsuario);

        return await CriarTokenAsync(usuario);
    }

    private async Task<TokenUsuario?> RecuperarTokenValidoAsync(long codigoUsuario)
    {
        if (await _usuarioTokenRepository.ExisteTokenAtivoPorUsuarioAsync(codigoUsuario))
        {
            var token = await _usuarioTokenRepository.ObterValidoPorUsuarioAsync(codigoUsuario);
            if (token is not null)
                return token;
        }
        return null;
    }

    private async Task<Usuario> ObterUsuarioAsync(long codigoUsuario)
    {
        var usuario = await _usuarioRepository.ObterAsync(codigoUsuario);
        DomainServiceException.ThrowIfUsuarioNaoEncontrato(usuario);
        return usuario!;
    }

    private async Task<TokenUsuario> CriarTokenAsync(Usuario usuario)
    {
        (var token, var dataExpiracao) = _autenticacaoService.CriarToken(usuario.Email);
        var novoToken = new TokenUsuario(usuario.Codigo, token, dataExpiracao);
        await _usuarioTokenRepository.CriarAsync(novoToken);
        return novoToken;
    }

    public async Task<TokenUsuario> ObterTokenValidoAsync(long codigoUsuario)
    {
        var token = await RecuperarTokenValidoAsync(codigoUsuario);
        if (token is not null) 
            return token!;
        throw new DomainServiceException($"Não existe um token válidos para o usuário {codigoUsuario}");
    }
}
