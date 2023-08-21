using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AutenticaoService
{
    public  async Task<string> GenerateToken(UsuarioLogadoResponse user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("e3f9b5a0-0b7a-4e3a-8b7a-4e3a8b7a4e3a");
        var claims = new List<Claim>();
        claims.AddRange(new []
        {
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        });
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}