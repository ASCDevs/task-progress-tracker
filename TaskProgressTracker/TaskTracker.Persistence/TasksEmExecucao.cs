using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistence
{
    public static class TasksEmExecucao
    {
        public static ConcurrentDictionary<string,Tarefa> TaskList = new ConcurrentDictionary<string,Tarefa>();

        public static int CountTasks()
        {
            return TaskList.Count;
        }
    }
}
