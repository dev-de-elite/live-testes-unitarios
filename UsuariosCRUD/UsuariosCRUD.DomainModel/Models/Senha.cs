using UsuariosCRUD.DomainModel.Validators;

namespace UsuariosCRUD.DomainModel.Models;

public sealed class Senha : IValidadorModelo
{
    public string Valor { get; set; } = default!;

    public bool ValidarModelo() =>
        !string.IsNullOrWhiteSpace(Valor);

    public override string ToString() => Valor;

    #region Conversion Operators
    public static implicit operator string(Senha senha) => senha.ToString();
    public static implicit operator Senha(string valor) => new() { Valor = valor };
    #endregion
}
