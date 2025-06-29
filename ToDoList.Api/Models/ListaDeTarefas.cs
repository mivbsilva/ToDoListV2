using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Api.Models
{
    public class ListaDeTarefas
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; } = string.Empty;
        
        public int UsuarioId { get; set; }
        
        [JsonIgnore]
        public Usuario Usuario { get; set; } = null!;

        public List<Tarefa> Tarefas { get; set; } = new();
    }
}