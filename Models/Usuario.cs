namespace SistemaGerenciadorDeProdutos.Models
{
    public class Usuario
    {
        private int _id;
        private string _nome = string.Empty;
        private string _email = string.Empty;
        private string _senha = string.Empty;
        private string _funcao = "Funcionario";

        // Construtor
        public Usuario() { }

        public Usuario(int id, string nome, string email, string senha, string funcao = "Funcionario")
        {
            _id = id;
            _nome = nome;
            _email = email;
            _senha = senha;
            SetFuncao(funcao);
        }

        // Getters e Setters públicos
        public int GetId() => _id;
        public string GetNome() => _nome;
        public void SetNome(string nome) => _nome = nome;

        public string GetEmail() => _email;
        public void SetEmail(string email) => _email = email;

        public string GetSenhaHash() => _senha;
        public void SetSenhaHash(string senhaHash) => _senha = senhaHash;

        public string GetFuncao() => _funcao;
        public void SetFuncao(string funcao)
        {
            if (funcao != "Gerente" && funcao != "Funcionario")
            {
                throw new ArgumentException("Função inválida. Permitido apenas 'Gerente' ou 'Funcionario'.");
            }
            _funcao = funcao;
        }
    }
}
