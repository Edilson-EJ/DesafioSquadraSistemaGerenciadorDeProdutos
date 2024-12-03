using Microsoft.AspNetCore.Mvc;
using SistemaGerenciadorDeProdutos.Models;
using SistemaGerenciadorDeProdutos.Services;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            // Validação básica para garantir que o email e a senha não sejam nulos ou vazios
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            // Tenta autenticar o usuário
            var token = await _authService.Authenticate(login.Email, login.Password);

            // Se o token for nulo, significa que as credenciais estão incorretas
            if (token == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            // Retorna o token JWT gerado
            return Ok(new { Token = token });
        }
    }
}
