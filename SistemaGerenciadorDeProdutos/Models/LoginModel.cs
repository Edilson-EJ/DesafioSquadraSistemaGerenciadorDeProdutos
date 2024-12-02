namespace SistemaGerenciadorDeProdutos.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        // Construtor para inicializar as propriedades
        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
