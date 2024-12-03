using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SistemaGerenciadorDeProdutos.Data;
using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> ObterUsuarioPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AdicionarUsuario(Usuario usuario)
        {
            ValidarFuncao(usuario.Funcao);
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash); 
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            ValidarFuncao(usuario.Funcao);
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        private void ValidarFuncao(string funcao)
        {
            if (funcao != "Gerente" && funcao != "Funcionario" && funcao != "gerente" && funcao != "funcionario")
            {
                throw new ArgumentException("Função inválida. Permitido apenas 'Gerente' ou 'Funcionario'.");
            }
        }

    }
}
