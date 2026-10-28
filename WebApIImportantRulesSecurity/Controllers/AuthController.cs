using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApIImportantRulesSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lOvU5X/NtDdZuTflPmfXIcrRaZXUE/pvxm9yM8IdUAoUfmygu3SdcLpme6Dv017s5ow4zanqiLtXULxFezTYUw=="));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(
            issuer: "MyApi",
            audience: "MyClient",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
