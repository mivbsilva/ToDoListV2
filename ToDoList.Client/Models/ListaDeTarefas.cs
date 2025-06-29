using System;
using System.Collections.Generic;

namespace ToDoList.Client.Models
{
    public class ListaDeTarefas
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new();
    }
}
