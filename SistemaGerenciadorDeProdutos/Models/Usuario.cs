namespace SistemaGerenciadorDeProdutos.Models
{
    public class Usuario
    {
        public int Id { get; set; } // Chave primária
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Funcao { get; set; } = "Funcionario";

        // Construtor
        public Usuario() { }

        public Usuario(int id, string nome, string email, string senha, string funcao = "Funcionario")
        {
            Id = id;
            Nome = nome;
            Email = email;
            SenhaHash = senha;
            SetFuncao(funcao);
        }

        // Métodos de Set e Get
        public string GetNome() => Nome;
        public void SetNome(string nome) => Nome = nome;

        public string GetEmail() => Email;
        public void SetEmail(string email) => Email = email;

        public string GetSenhaHash() => SenhaHash;
        public void SetSenhaHash(string senhaHash) => SenhaHash = senhaHash;

        public string GetFuncao() => Funcao;
        public void SetFuncao(string funcao)
        {
            if (funcao != "Gerente" && funcao != "Funcionario")
            {
                throw new ArgumentException("Função inválida. Permitido apenas 'Gerente' ou 'Funcionario'.");
            }
            Funcao = funcao;
        }
    }
}
