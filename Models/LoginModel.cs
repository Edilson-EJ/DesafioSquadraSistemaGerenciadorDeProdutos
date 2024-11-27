namespace SistemaGerenciadorDeProdutos.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Construtor para inicializar as propriedades
        public LoginModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
