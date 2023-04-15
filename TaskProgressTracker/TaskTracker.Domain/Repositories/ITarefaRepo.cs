using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Domain.Repositories
{
    public interface ITarefaRepo
    {
        Tarefa Add(Tarefa tarefa);
        Tarefa Update(Tarefa tarefa);
        List<Tarefa> GetAll();
        List<Tarefa> GetNotExecuted();
    }
}
