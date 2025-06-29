using System.ComponentModel.DataAnnotations;

namespace ToDoList.Client.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Texto { get; set; }
        
        public bool Concluida { get; set; } = false;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        public int ListaId { get; set; }
        public ListaDeTarefas Lista { get; set; }
    }
}