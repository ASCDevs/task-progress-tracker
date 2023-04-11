using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.Interfaces
{
    public interface ITarefaManager
    {
        bool AddTask(Tarefa task);
        bool StartTask(Tarefa idTarefa);
        void UpdateTask(Tarefa task);
        bool ChangeTaskStatus(string idTarefa, string status);
        void CompleteTask(Tarefa task);
        List<TaskInfoView> GetListTaskInfo();
        Tarefa GetTaskNotInExecution();
        List<Tarefa> GetTasksNotInExecution();
    }
}
