namespace UsuariosCRUD.DatabaseService.Entities;

internal class UsuarioTokenEntity
{
    public long Id { get; set; }
    public string Token { get; set; } = default!;
    public DateTime DataExpiracao { get; set; }
    public long UsuarioId { get; set; }

    public UsuarioEntity Usuario { get; set; } = default!;
}
