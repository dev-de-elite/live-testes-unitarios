using UsuariosCRUD.DomainModel.Validators;

namespace UsuariosCRUD.DomainModel.Models;

public sealed class Nome : IValidadorModelo
{
    public string PrimeiroNome { get; set; } = default!;
    public string UltimoNome { get; set; } = default!;

    public bool ValidarModelo() =>
        !string.IsNullOrWhiteSpace(PrimeiroNome) &&
        !string.IsNullOrWhiteSpace(UltimoNome);

    public override string ToString() => $"{PrimeiroNome} {UltimoNome}";

    #region Conversion Operators
    public static implicit operator string(Nome nome) => nome.ToString();
    public static implicit operator Nome(string nome)
    {
        ArgumentNullException.ThrowIfNull(nome, nameof(nome));
        var nomes = nome.Split(' ');
        if(nomes.Length != 2) 
            throw new ArgumentException($"Valor do nome '{nome}' não é válido", nameof(nome));

        return new()
        {
            PrimeiroNome = nomes[0],
            UltimoNome = nomes[1]
        };
    }
    #endregion
}
