using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Data;
using ToDoList.Api.Models;

namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListaController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ListaController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetListasUsuario(int usuarioId)
        {
            var listas = await _context.Listas
                .Where(l => l.UsuarioId == usuarioId)
                .Include(l => l.Tarefas)
                .ToListAsync();

            return Ok(listas);
        }

        [HttpPost]
        public async Task<IActionResult> CriarLista([FromBody] CriarListaRequest request)
        {
            var lista = new ListaDeTarefas
            {
                Nome = request.Nome,
                UsuarioId = request.UsuarioId
            };

            _context.Listas.Add(lista);
            await _context.SaveChangesAsync();
            
            return Ok(lista);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirLista(int id)
        {
            var lista = await _context.Listas
                .Include(l => l.Tarefas)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lista == null)
                return NotFound();

            _context.Listas.Remove(lista);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

    public class CriarListaRequest
    {
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
    }
}
