using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly List<Produto> _produtos;

        public ProdutoService()
        {
            _produtos = new List<Produto>();
        }

        // Obter todos os produtos
        public Task<IEnumerable<Produto>> ObterTodosProdutos()
        {
            return Task.FromResult(_produtos.AsEnumerable());
        }

        // Obter um produto pelo ID
        public Task<Produto?> ObterProdutoPorId(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.GetId() == id);
            return Task.FromResult(produto);
        }

        // Adicionar um novo produto
        public Task AdicionarProduto(Produto produto)
        {
            _produtos.Add(produto);
            return Task.CompletedTask;
        }

        // Atualizar um produto existente
        public Task AtualizarProduto(Produto produto)
        {
            var produtoExistente = _produtos.FirstOrDefault(p => p.GetId() == produto.GetId());
            if (produtoExistente != null)
            {
                produtoExistente.SetNome(produto.GetNome());
                produtoExistente.SetDescricao(produto.GetDescricao());
                produtoExistente.SetStatus(produto.GetStatus());
                produtoExistente.SetPreco(produto.GetPreco());
                produtoExistente.SetQuantidadeEstoque(produto.GetQuantidadeEstoque());
            }
            return Task.CompletedTask;
        }

        // Excluir um produto pelo ID
        public Task ExcluirProduto(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.GetId() == id);
            if (produto != null)
            {
                _produtos.Remove(produto);
            }
            return Task.CompletedTask;
        }
    }
}
