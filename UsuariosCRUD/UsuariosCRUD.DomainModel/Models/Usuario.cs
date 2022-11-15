using UsuariosCRUD.DomainModel.Validators;

namespace UsuariosCRUD.DomainModel.Models;

public sealed class Usuario : IValidadorModelo
{
    public long Codigo { get; set; }
    public Nome Nome { get; set; } = default!;
    public Email Email { get; set; } = default!;
    public string NomeUsuario { get; set; } = default!;
    public Senha Senha { get; set; } = default!;

    public bool ValidarModelo() =>
        Nome.ValidarModelo() &&
        Email.ValidarModelo() &&
        Senha.ValidarModelo() &&
        !string.IsNullOrWhiteSpace(NomeUsuario);
}
