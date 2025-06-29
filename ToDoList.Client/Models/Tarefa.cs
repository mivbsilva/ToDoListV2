using System;

namespace ToDoList.Client.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public bool Concluida { get; set; } = false;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int ListaId { get; set; }
    }
}
