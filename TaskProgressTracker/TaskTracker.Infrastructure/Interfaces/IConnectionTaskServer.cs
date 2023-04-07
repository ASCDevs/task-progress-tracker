using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;

namespace TaskTracker.Infrastructure.Interfaces
{
    public interface IConnectionTaskServer
    {
        void AddTaskTeste();
        Task<List<TaskInfoView>> GetTasksOn();
        int CountTasks();
    }
}
