using System.ComponentModel.DataAnnotations;

namespace UsuariosCRUD.API.Dtos.Usuarios;

public class UsuarioInputDto
{
    [Required]
    public string PrimeiroNome { get; set; } = default!;
    
    [Required]
    public string UltimoNome { get; set; } = default!;
    
    [Required]
    public string Email { get; set; } = default!;
    
    [Required]
    public string NomeUsuario { get; set; } = default!;
    
    [Required]
    public string Senha { get; set; } = default!;
}
