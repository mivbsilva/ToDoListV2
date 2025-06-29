using System.ComponentModel.DataAnnotations;

namespace ToDoList.Api.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        public string NomeUsuario { get; set; } = string.Empty;
        
        [Required]
        public string Senha { get; set; } = string.Empty;

        public List<ListaDeTarefas> Listas { get; set; } = new();
    }
}