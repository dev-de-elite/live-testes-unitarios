using System.Text;

namespace UsuariosCRUD.AuthenticationService.Configurations;

public class JwtConfig
{
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;

    public byte[] GetKeyAsByteArray() => Encoding.UTF8.GetBytes(Key);
}
