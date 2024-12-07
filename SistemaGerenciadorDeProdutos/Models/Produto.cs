using System.ComponentModel.DataAnnotations;

namespace SistemaGerenciadorDeProdutos.Models
{
    public class Produto
    {
        // Propriedades públicas para mapeamento do Entity Framework
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Status { get; set; } = "Em estoque";

        public decimal Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser um número positivo.")]
        public int QuantidadeEstoque { get; set; }

        public Produto() { }

        public Produto(int id, string nome, string? descricao, decimal preco, int quantidadeEstoque, string status = "Em estoque")
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            QuantidadeEstoque = quantidadeEstoque;
            Status = status;
        }
    }
}
