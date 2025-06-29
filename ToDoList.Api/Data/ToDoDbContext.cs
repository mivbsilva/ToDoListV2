using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ListaDeTarefas> Listas { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListaDeTarefas>()
                .HasOne(l => l.Usuario)
                .WithMany(u => u.Listas)
                .HasForeignKey(l => l.UsuarioId);

            modelBuilder.Entity<Tarefa>()
                .HasOne(t => t.Lista)
                .WithMany(l => l.Tarefas)
                .HasForeignKey(t => t.ListaId);
        }
    }
}