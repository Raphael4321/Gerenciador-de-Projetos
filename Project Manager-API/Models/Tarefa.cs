namespace Project_Manager_API.Models
{
    public class Tarefa
    {
        public int ID { get; set; }
        public int ProjetoID { get; set; } 
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public StatusTarefa Status { get; set; }
    }

    public enum StatusTarefa
    {
        Incompleto,
        EmProgresso,
        Completo
    }
}
