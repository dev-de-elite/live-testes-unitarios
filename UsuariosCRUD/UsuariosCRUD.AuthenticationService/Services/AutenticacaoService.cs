using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsuariosCRUD.AuthenticationService.Configurations;
using UsuariosCRUD.DomainService.Authentications;

namespace UsuariosCRUD.AuthenticationService.Services;

internal class AutenticacaoService : IAutenticacaoService
{
    private readonly JwtConfig _jwtConfig;

    public AutenticacaoService(IOptions<JwtConfig> jwtConfigOptions)
    {
        _jwtConfig = jwtConfigOptions.Value;
    }

    public (string token, DateTime dataExpiracao) CriarToken(string chaveUnica)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = GerarTokenDescriptor(chaveUnica);        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), tokenDescriptor.Expires.GetValueOrDefault());
    }

    private static Claim[] GerarClaims(string chaveUnica) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, chaveUnica),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

    private SecurityTokenDescriptor GerarTokenDescriptor(string chaveUnica) =>
        new()
        {
            Subject = new ClaimsIdentity(GerarClaims(chaveUnica)),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(GerarSecurityKey(), SecurityAlgorithms.HmacSha256),
        };

    private SymmetricSecurityKey GerarSecurityKey() => new(_jwtConfig.GetKeyAsByteArray());

}
