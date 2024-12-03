using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaGerenciadorDeProdutos.Models;
using SistemaGerenciadorDeProdutos.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaGerenciadorDeProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoInterface _produtoService;

        public ProdutoController(IProdutoInterface produtoService)
        {
            _produtoService = produtoService;
        }

        // GET: api/produto
        [HttpGet("getProduto")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoService.ObterTodosProdutos();
            return Ok(produtos);
        }

        // GET: api/produto/{id}
        [HttpGet("getDetailProduto/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _produtoService.ObterProdutoPorId(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // POST: api/produto
        [HttpPost("postProduto")]
        //[Authorize(Roles = "Gerente, Funcionario")]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            await _produtoService.AdicionarProduto(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // PUT: api/produto/{id}
        [HttpPut("updateProduto/{id}")]
        //[Authorize(Roles = "Gerente, Funcionario")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            await _produtoService.AtualizarProduto(produto);

            return NoContent();
        }

        // DELETE: api/produto/{id}
        [HttpDelete("deleteProduto/{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _produtoService.ObterProdutoPorId(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoService.ExcluirProduto(id);

            return NoContent();
        }
    }
}
