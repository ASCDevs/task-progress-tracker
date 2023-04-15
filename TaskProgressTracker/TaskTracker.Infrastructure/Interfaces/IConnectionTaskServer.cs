using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Infrastructure.Interfaces
{
    public interface IConnectionTaskServer
    {
        void UpdateTask(Tarefa tarefa);
        void UpdateUI(Tarefa tarefa);
        Task<List<TaskInfoView>> GetTasksOnInfoView();
        Task<List<Tarefa>> GetTasksObj();
        int CountTasks();
        
    }
}
