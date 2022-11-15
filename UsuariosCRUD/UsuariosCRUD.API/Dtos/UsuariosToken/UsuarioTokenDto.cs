using System.ComponentModel.DataAnnotations;

namespace UsuariosCRUD.API.Dtos.UsuariosToken;

public class UsuarioTokenDto
{
    public UsuarioTokenDto(string token, [Required] DateTime dataExpiracao)
    {
        Token = token;
        DataExpiracao = dataExpiracao;

    }

    [Required]
    public string Token { get; set; }

    [Required]
    public DateTime DataExpiracao { get; set; }
}
