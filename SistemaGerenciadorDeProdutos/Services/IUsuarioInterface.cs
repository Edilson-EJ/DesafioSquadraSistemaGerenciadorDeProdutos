using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Services
{
    public interface IUsuarioInterface
    {
        Task AdicionarUsuario(Usuario usuario);
        Task<Usuario?> ObterUsuarioPorId(int id);
        Task AtualizarUsuario(Usuario usuario);
        Task ExcluirUsuario(int id);
        Task<IEnumerable<Usuario>> ObterTodosUsuarios();

        Task<Usuario?> ObterUsuarioPorEmail(string email);
    }
}
