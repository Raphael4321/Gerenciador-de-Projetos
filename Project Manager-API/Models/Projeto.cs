namespace Project_Manager_API.Models
{
    public class Projeto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public StatusProjeto Status { get; set; }
        public List<Tarefa> Tarefas { get; set; }
        public List<MEquipe> Membros { get; set; }
    }

    public enum StatusProjeto
    {
        Incompleto,
        EmProgresso,
        Completo
    }
}
