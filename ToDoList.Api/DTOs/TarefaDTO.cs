namespace ToDoList.Api.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public bool Concluida { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ListaId { get; set; }
    }
}
