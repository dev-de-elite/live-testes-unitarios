using UsuariosCRUD.DomainModel.Validators;

namespace UsuariosCRUD.DomainModel.Models;

public sealed class Email : IValidadorModelo
{
    public string Endereco { get; set; } = default!;

    public bool ValidarModelo() =>
        !string.IsNullOrWhiteSpace(Endereco) &&
        Endereco.Contains('@');

    public override string ToString() => Endereco;

    #region Conversion Operators
    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string endereco) => new() { Endereco = endereco };
    #endregion
}
