using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Task_Management_API.Models;

namespace Task_Management_API.lib;

public class JwtService(IConfiguration configuration)
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username!)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Secret")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

    }
}