namespace UsuariosCRUD.DomainService.Authentications;

public interface IAutenticacaoService
{
    (string token, DateTime dataExpiracao) CriarToken(string chaveUnica);
}
