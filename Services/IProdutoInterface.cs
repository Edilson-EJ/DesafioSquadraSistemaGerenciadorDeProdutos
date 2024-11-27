using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public interface IProdutoInterface
    {
        // Adicionar um novo produto
        Task AdicionarProduto(Produto produto);

        // Obter um produto pelo ID
        Task<Produto?> ObterProdutoPorId(int id);

        // Atualizar um produto existente
        Task AtualizarProduto(Produto produto);

        // Excluir um produto pelo ID
        Task ExcluirProduto(int id);

        // Obter todos os produtos
        Task<IEnumerable<Produto>> ObterTodosProdutos();
    }
}
