using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Data;
using ToDoList.Api.Models;

namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public TarefaController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa([FromBody] CriarTarefaRequest request)
        {
            var tarefa = new Tarefa
            {
                Texto = request.Texto,
                ListaId = request.ListaId,
                Concluida = false,
                DataCriacao = DateTime.Now
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] AtualizarTarefaRequest request)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            tarefa.Texto = request.Texto;
            tarefa.Concluida = request.Concluida;

            await _context.SaveChangesAsync();
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

    public class CriarTarefaRequest
    {
        public string Texto { get; set; } = string.Empty;
        public int ListaId { get; set; }
    }

    public class AtualizarTarefaRequest
    {
        public string Texto { get; set; } = string.Empty;
        public bool Concluida { get; set; }
    }
}
