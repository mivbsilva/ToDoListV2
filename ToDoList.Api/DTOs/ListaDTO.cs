namespace ToDoList.Api.DTOs
{
    public class ListaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public List<TarefaDTO> Tarefas { get; set; } = new();
    }
}
