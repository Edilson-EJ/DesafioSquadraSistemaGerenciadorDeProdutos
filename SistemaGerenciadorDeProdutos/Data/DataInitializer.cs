using Microsoft.EntityFrameworkCore;
using SistemaGerenciadorDeProdutos.Models;
using System.Linq;

namespace SistemaGerenciadorDeProdutos.Data
{
    public static class DataInitializer
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.Usuarios.Any(u => u.Email == "edilson@ex.com"))
            {
                var usuario = new Usuario
                {
                    Nome = "Edilson de França",
                    Email = "edilson@ex.com",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("root"),
                    Funcao = "Gerente"
                };

                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }
    }
}
