namespace SistemaGerenciadorDeProdutos.Services;

using SistemaGerenciadorDeProdutos.Models;
using System.Collections.Generic;

{
    public interface IProdutoInterface
    {
        // Adicionar um novo produto
        void AdicionarProduto(Produto produto);

        void AtualizarProduto(Produto produto);

        
}
}
