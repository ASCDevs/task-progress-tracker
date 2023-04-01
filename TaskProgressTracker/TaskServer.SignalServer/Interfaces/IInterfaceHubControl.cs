using TaskTracker.Domain;

namespace TaskServer.SignalServer.Interfaces
{
    public interface IInterfaceHubControl
    {
        Task UIAddTask(TaskInfoView task);
        Task UIUpdateTask(TaskInfoView task);
        Task UISendAvailablesTasks(List<TaskInfoView> tasks);
    }
}
