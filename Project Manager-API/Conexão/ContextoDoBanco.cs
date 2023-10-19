using Microsoft.EntityFrameworkCore;
using Project_Manager_API.Models;

namespace Project_Manager_API.Conexão
{
    public class ContextoDoBanco : DbContext
    {
        public ContextoDoBanco(DbContextOptions<ContextoDoBanco> options) : base(options)
        {
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<MEquipe> Membros { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<MEquipe_Tarefa> MEquipe_Tarefas { get; set; }
    }
}
