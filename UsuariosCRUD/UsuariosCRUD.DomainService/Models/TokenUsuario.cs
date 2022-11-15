namespace UsuariosCRUD.DomainService.Models;

public record TokenUsuario(long CodigoUsuario, string Token, DateTime DataExpiracao);
