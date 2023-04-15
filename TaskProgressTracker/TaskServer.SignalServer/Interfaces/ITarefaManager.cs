using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.Interfaces
{
    public interface ITarefaManager
    {
        void UpdateTask(Tarefa task);
        void UpdateUI(Tarefa task);
        List<TaskInfoView> GetListTaskInfo();
        List<Tarefa> GetTasksNotInExecution();
    }
}
