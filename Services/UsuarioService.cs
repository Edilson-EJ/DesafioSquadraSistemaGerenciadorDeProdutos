using BCrypt.Net;
using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly List<Usuario> _usuarios;

        public UsuarioService()
        {
            _usuarios = new List<Usuario>
            {
                new Usuario(1, "Gerente", "gerente@example.com", BCrypt.Net.BCrypt.HashPassword("senha123"), "Gerente"),
                new Usuario(2, "Funcionario", "funcionario@example.com", BCrypt.Net.BCrypt.HashPassword("senha123"), "Funcionario")
            };
        }

        public Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return Task.FromResult(_usuarios.AsEnumerable());
        }

        public Task<Usuario?> ObterUsuarioPorId(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.GetId() == id);
            return Task.FromResult(usuario);
        }

        public Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.GetEmail() == email);
            return Task.FromResult(usuario);
        }

        public Task AdicionarUsuario(Usuario usuario)
        {
            ValidarFuncao(usuario.GetFuncao());
            _usuarios.Add(usuario);
            return Task.CompletedTask;
        }

        public Task AtualizarUsuario(Usuario usuario)
        {
            ValidarFuncao(usuario.GetFuncao());
            var usuarioExistente = _usuarios.FirstOrDefault(u => u.GetId() == usuario.GetId());
            if (usuarioExistente != null)
            {
                usuarioExistente.SetNome(usuario.GetNome());
                usuarioExistente.SetEmail(usuario.GetEmail());
                usuarioExistente.SetSenhaHash(usuario.GetSenhaHash());
                usuarioExistente.SetFuncao(usuario.GetFuncao());
            }
            return Task.CompletedTask;
        }

        public Task ExcluirUsuario(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.GetId() == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
            }
            return Task.CompletedTask;
        }

        private void ValidarFuncao(string funcao)
        {
            if (funcao != "Gerente" && funcao != "Funcionario")
            {
                throw new ArgumentException("Função inválida. Permitido apenas 'Gerente' ou 'Funcionario'.");
            }
        }
    }
}
