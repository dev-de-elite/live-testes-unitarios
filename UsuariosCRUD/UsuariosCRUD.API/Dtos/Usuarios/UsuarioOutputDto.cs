namespace UsuariosCRUD.API.Dtos.Usuarios;

public class UsuarioOutputDto
{
    public long Codigo { get; set; }
    public string PrimeiroNome { get; set; } = default!;
    public string UltimoNome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NomeUsuario { get; set; } = default!;
    public string Senha { get; set; } = default!;
}
