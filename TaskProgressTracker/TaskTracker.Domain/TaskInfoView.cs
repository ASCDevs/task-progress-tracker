namespace TaskTracker.Domain
{
    public class TaskInfoView
    {
        public int id { get; set; }
        public string idTask { get; set; }
        public string taskName { get; set; }
        public string dtSolicitacao { get; set; }
        public string dtInicio { get; set; }
        public string dtFinalizacao { get; set; }
        public string status { get; set; }
    }
}
