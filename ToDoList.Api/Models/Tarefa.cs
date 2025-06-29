using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Api.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Texto { get; set; } = string.Empty;
        
        public bool Concluida { get; set; } = false;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public int ListaId { get; set; }
        
        [JsonIgnore]
        public ListaDeTarefas Lista { get; set; } = null!;
    }
}