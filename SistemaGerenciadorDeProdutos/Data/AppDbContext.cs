using Microsoft.EntityFrameworkCore;
using SistemaGerenciadorDeProdutos.Models;

namespace SistemaGerenciadorDeProdutos.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
    }
}
