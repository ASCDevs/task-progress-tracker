namespace TaskServer.SignalServer.Interfaces
{
    public interface ITarefaHubControl
    {
        Task AddTask();
        Task ChangeStatusTask();
        Task CompleteTask();
        Task GetAllTasks();
        int CountTasks();
    }
}
