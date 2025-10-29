using Microsoft.AspNetCore.Mvc;
using _2_Application.Interfaces;
using _1_Domain.Entities;
using _3_Infrastructure.Data;
using _3_Infrastructure.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            var produtos = await _repository.GetAllAsync();
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            var produto = await _repository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            await _repository.AddAsync(produto);

            // Retorna 201 com localização do recurso criado
            return CreatedAtAction(nameof(GetById), new { id = produto.ID }, produto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
        {
            if (produto == null || id != produto.ID) return BadRequest();

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Nome = produto.Nome;
            existing.Preco = produto.Preco;
            existing.Quantidade = produto.Quantidade;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            if (!string.IsNullOrEmpty(produto.Nome))
                existing.Nome = produto.Nome;

            if (produto.Preco != 0)
                existing.Preco = produto.Preco;

            if (produto.Quantidade != 0)
                existing.Quantidade = produto.Quantidade;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }
    }
}
