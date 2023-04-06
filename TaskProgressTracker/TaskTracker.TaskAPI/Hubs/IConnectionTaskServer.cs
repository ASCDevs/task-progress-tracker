using TaskTracker.Domain;

namespace TaskTracker.TaskAPI.Hubs
{
    public interface IConnectionTaskServer
    {
        void AddTaskTeste();
        Task<List<TaskInfoView>> GetTasksOn();
    }
}
