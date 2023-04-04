using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.Interfaces
{
    public interface ITarefaHubControl
    {
        Task ChangeStatusTask();
        Task CompleteTask();
        Task GetAllTasks();
        int CountTasks();
    }
}
