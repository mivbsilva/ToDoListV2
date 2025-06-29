using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Data;
using ToDoList.Api.Models;

namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public UsuarioController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuario(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.NomeUsuario == usuario.NomeUsuario))
            {
                return BadRequest("Usuário já existe");
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario == request.NomeUsuario && u.Senha == request.Senha);

            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            return Ok(usuario);
        }
    }

    public class LoginRequest
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}