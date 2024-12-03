using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaGerenciadorDeProdutos.Data;
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
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> Authenticate(string email, string password)
        {
            // Encontra o usuário pelo email
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.SenhaHash))
            {
                return null; // Credenciais inválidas
            }

            // Criação dos Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Funcao)
            };

            try
            {
                // Recupera as configurações do JWT
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings.GetValue<string>("SecretKey");

                if (string.IsNullOrEmpty(secretKey))
                {
                    throw new InvalidOperationException("A chave secreta JWT não está configurada.");
                }

                // Configura a chave secreta e o algoritmo de assinatura
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Criação do token JWT com as configurações
                var token = new JwtSecurityToken(
                    issuer: jwtSettings.GetValue<string>("Issuer"),
                    audience: jwtSettings.GetValue<string>("Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(jwtSettings.GetValue<int>("ExpireMinutes")),
                    signingCredentials: creds
                );

                // Usando Console.WriteLine para debugar
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.WriteToken(token);

                // Logando a estrutura do token gerado
                Console.WriteLine("Token gerado: " + jwtToken); 

                // Verifique a estrutura do JWT
                if (string.IsNullOrEmpty(jwtToken) || !jwtToken.Contains('.'))
                {
                    throw new InvalidOperationException("O token JWT gerado não está no formato correto.");
                }

                // Retorna o token JWT em formato string
                return jwtToken;
            }
            catch (Exception ex)
            {
                // Lide com a exceção, por exemplo, logando o erro.
                Console.WriteLine("Erro ao gerar token: " + ex.Message); // Logando o erro
                throw new InvalidOperationException("Erro ao gerar o token JWT.", ex);
            }
        }
    }
}
