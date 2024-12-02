using Microsoft.EntityFrameworkCore;
using SistemaGerenciadorDeProdutos.Data;
using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        // Obter todos os produtos
        public async Task<IEnumerable<Produto>> ObterTodosProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        // Obter um produto pelo ID
        public async Task<Produto?> ObterProdutoPorId(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        // Adicionar um novo produto
        public async Task AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        // Atualizar um produto existente
        public async Task AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        // Excluir um produto pelo ID
        public async Task ExcluirProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
