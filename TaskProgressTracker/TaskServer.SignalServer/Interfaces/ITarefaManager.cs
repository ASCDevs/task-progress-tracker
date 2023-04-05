using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.Interfaces
{
    public interface ITarefaManager
    {
        bool AddTask(Tarefa task);
        bool StartTask(string idTarefa);
        void UpdateTask(Tarefa task);
        bool ChangeTaskStatus(string idTarefa, string status);
        Tarefa CompleteTask(string idTarefa);
        List<TaskInfoView> GetListTaskInfo();
        Tarefa GetTaskNotInExecution();
    }
}
