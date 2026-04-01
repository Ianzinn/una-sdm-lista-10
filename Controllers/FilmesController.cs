using Microsoft.AspNetCore.Mvc;
using OscarFilmeApi.Data;
using OscarFilmeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OscarFilmeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Filme filme)
        {
            if (filme.AnoLancamento < 1929)
                return BadRequest("O Oscar começou em 1929!");

            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            return Ok(filme);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Filmes.ToListAsync());
        }

        [HttpGet("vencedores")]
        public async Task<IActionResult> GetVencedores()
        {
            var vencedores = await _context.Filmes
                .Where(f => f.Venceu)
                .ToListAsync();

            return Ok(vencedores);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Filme filme)
        {
            if (id != filme.Id)
                return BadRequest();

            if (filme.Venceu)
                Console.WriteLine($"Temos um novo vencedor: {filme.Titulo}!");

            _context.Entry(filme).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
                return NotFound();

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("estatisticas")]
        public async Task<IActionResult> Estatisticas()
        {
            var total = await _context.Filmes.CountAsync();
            var vencedores = await _context.Filmes.CountAsync(f => f.Venceu);

            return Ok(new { total, vencedores });
        }
    }
}