namespace UsuariosCRUD.DatabaseService.Entities;

internal class UsuarioEntity
{
    public long Id { get; set; }
    public string PrimeiroNome { get; set; } = default!;
    public string UltimoNome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NomeUsuario { get; set; } = default!;
    public string Senha { get; set; } = default!;

    public IEnumerable<UsuarioTokenEntity> Tokens { get; set; } = default!;
}
