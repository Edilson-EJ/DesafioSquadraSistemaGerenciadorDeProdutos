using SistemaGerenciadorDeProdutos.Models;

namespace SistemaGerenciadorDeProdutos.Services
{
    public class ProdutoService : IProdutoInterface
    {
        private Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }
}
