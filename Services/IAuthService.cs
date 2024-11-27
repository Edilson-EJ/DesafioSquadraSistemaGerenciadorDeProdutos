namespace SistemaGerenciadorDeProdutos.Services
{
    public interface IAuthService
    {
        Task<string?> Authenticate(string username, string password);
    }
}
