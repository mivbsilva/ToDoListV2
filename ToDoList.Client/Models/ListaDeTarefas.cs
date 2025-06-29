using System;
using System.Collections.Generic;

namespace ToDoList.Client.Models
{
    public class ListaDeTarefas
    {
        public string Nome { get; set; }
        public List<string> Tarefas { get; set; }

        public ListaDeTarefas()
        {
            Tarefas = new List<string>();
        }    
    }
}
