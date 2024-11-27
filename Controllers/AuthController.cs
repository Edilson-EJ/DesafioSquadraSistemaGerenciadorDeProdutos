using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaGerenciadorDeProdutos.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaGerenciadorDeProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Aqui você deve validar o usuário com seu repositório de dados
            // Para fins de demonstração, estamos assumindo que o login foi bem-sucedido

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua_chave_secreta"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, "Funcionario") // Aqui você pode definir o papel (Role) do usuário
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "seu_issuer",
                audience: "seu_audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }
    }
}
