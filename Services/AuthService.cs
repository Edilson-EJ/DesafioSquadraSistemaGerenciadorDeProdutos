using Microsoft.IdentityModel.Tokens;
using SistemaGerenciadorDeProdutos.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioInterface _usuarioService;
        private const string SecretKey = "sua_chave_secreta";
        private const string Issuer = "seu_issuer";
        private const string Audience = "seu_audience";

        public AuthService(IUsuarioInterface usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<string?> Authenticate(string username, string password)
        {
            var usuario = await _usuarioService.ObterUsuarioPorEmail(username);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.GetSenhaHash()))
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.GetNome()),
                new Claim(ClaimTypes.Role, usuario.GetFuncao())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
