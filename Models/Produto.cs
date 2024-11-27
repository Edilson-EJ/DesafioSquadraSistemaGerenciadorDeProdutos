namespace SistemaGerenciadorDeProdutos.Models
{
    public class Produto
    {
        private int _id;
        private string _nome = string.Empty;
        private string? _descricao;
        private string _status = "Em estoque";
        private decimal _preco;
        private int _quantidadeEstoque;

        // Construtor
        public Produto() { }

        public Produto(int id, string nome, string? descricao, decimal preco, int quantidadeEstoque, string status = "Em estoque")
        {
            _id = id;
            _nome = nome;
            _descricao = descricao;
            _preco = preco;
            _quantidadeEstoque = quantidadeEstoque;
            _status = status;
        }

        // Getters e Setters públicos
        public int GetId() => _id;
        public string GetNome() => _nome;
        public void SetNome(string nome) => _nome = nome;

        public string? GetDescricao() => _descricao;
        public void SetDescricao(string? descricao) => _descricao = descricao;

        public string GetStatus() => _status;
        public void SetStatus(string status) => _status = status;

        public decimal GetPreco() => _preco;
        public void SetPreco(decimal preco) => _preco = preco;

        public int GetQuantidadeEstoque() => _quantidadeEstoque;
        public void SetQuantidadeEstoque(int quantidade) => _quantidadeEstoque = quantidade;
    }
}
