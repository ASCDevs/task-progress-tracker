using System.Collections.Concurrent;
using System.Threading.Tasks;
using TaskServer.SignalServer.Hubs;
using TaskServer.SignalServer.Interfaces;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Repositories;

namespace TaskServer.SignalServer.HubsControl
{
    public class TarefaManager : ITarefaManager
    {
        private readonly ITarefaHubControl _tarefaHubControl;
        private readonly IInterfaceHubControl _interfaceHubControl;
        private readonly ConcurrentDictionary<string, Tarefa> tasks = new();
        private readonly ILogger<TarefaManager> _logger;
        private readonly ITarefaRepo _tarefaRepo;
        private const int nrTasksPerCall = 3;

        public TarefaManager(ITarefaHubControl tarefaHubControl, IInterfaceHubControl interfaceHubControl, ILogger<TarefaManager> logger, ITarefaRepo tarefaRepo)
        {
            _interfaceHubControl = interfaceHubControl;
            _tarefaHubControl = tarefaHubControl;
            _logger = logger;
            _tarefaRepo = tarefaRepo;
        }

        public bool AddTask(Tarefa task)
        {
            task.IdTarefa = Guid.NewGuid().ToString();
            task.PedidoTarefa = DateTime.Now;
            task.Status = "Solicitado";
            if(tasks.TryAdd(task.IdTarefa, task))
            {
                _tarefaRepo.Update(task);
                _interfaceHubControl.UIAddTask(ConvertToTaskInfo(task));
                return true;
            }
                
            return false;
        }

        public void UpdateTask(Tarefa task)
        {
            Tarefa taskInList;
            if(tasks.TryGetValue(task.IdTarefa, out taskInList)){

                if(tasks.TryUpdate(task.IdTarefa, task, taskInList)){
                    _tarefaRepo.Update(task);
                    _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(task));
                }
            }
        }

        public bool StartTask(Tarefa tarefa)
        {
            Tarefa taskInList;
            tasks.TryGetValue(tarefa.IdTarefa, out taskInList);

            if(taskInList != null)
            {
                if (tasks.TryUpdate(tarefa.IdTarefa, tarefa, taskInList))
                {
                    _tarefaRepo.Update(tarefa);
                    _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(tarefa));
                    return true;
                }
            }
            
            return false ;
        }

        public bool ChangeTaskStatus(string idTarefa, string status)
        {
            Tarefa taskToUpdate;
            Tarefa taskInList;
            tasks.TryGetValue(idTarefa, out taskInList);
            if(taskInList != null)
            {
                taskToUpdate = taskInList;
                taskToUpdate.Status = status;
                if (tasks.TryUpdate(idTarefa, taskToUpdate, taskInList))
                {
                    _tarefaRepo.Update(taskToUpdate);
                    _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(taskToUpdate));
                    return true;
                }
            }
            
            return false;
        }

        public void CompleteTask(Tarefa task)
        {
            Tarefa taskInList;

            if (tasks.TryGetValue(task.IdTarefa, out taskInList))
            {

                if(tasks.TryRemove(task.IdTarefa, out taskInList))
                {
                    _tarefaRepo.Update(task);
                    _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(task));
                }
            }
        }

        public List<TaskInfoView> GetListTaskInfo()
        {
            return tasks.Select(x => ConvertToTaskInfo(x.Value)).ToList();
        }
        public Tarefa GetTaskNotInExecution()
        {
            return tasks.FirstOrDefault(x => x.Value.InicioTarefa == null && x.Value.FimTarefa == null).Value;
        }

        public List<Tarefa> GetTasksNotInExecution()
        {
            return tasks.Where(t => t.Value.InicioTarefa == null && t.Value.FimTarefa == null).Select(x => x.Value).Take(3).ToList();
        }

        private TaskInfoView ConvertToTaskInfo(Tarefa task)
        {
            return new TaskInfoView
            {
                IdTask = task.IdTarefa ?? "-",
                DtSolicitacao = task.PedidoTarefa?.ToString("dd/MM/yyyy HH:mm:ss") ?? "-",
                DtFinalizacao = task.FimTarefa?.ToString("dd/MM/yyyy HH:mm:ss") ?? "-",
                DtInicio = task.InicioTarefa?.ToString("dd/MM/yyyy HH:mm:ss") ?? "-",
                Status = task.Status ?? "-",
                TaskName = task.NomeTarefa ?? "-"
            };
        }

    }
}
