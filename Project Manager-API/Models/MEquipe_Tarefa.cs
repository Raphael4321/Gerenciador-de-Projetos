namespace Project_Manager_API.Models
{
    public class MEquipe_Tarefa
    {
        public int ID { get; set; }
        public Tarefa Tarefa { get; set; }
        public MEquipe Membro { get; set; }
    }
}
