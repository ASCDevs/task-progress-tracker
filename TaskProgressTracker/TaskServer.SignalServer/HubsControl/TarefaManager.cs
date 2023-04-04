using System.Collections.Concurrent;
using TaskServer.SignalServer.Hubs;
using TaskServer.SignalServer.Interfaces;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.HubsControl
{
    public class TarefaManager : ITarefaManager
    {
        private readonly ITarefaHubControl _tarefaHubControl;
        private readonly IInterfaceHubControl _interfaceHubControl;
        private readonly ConcurrentDictionary<string, Tarefa> tasks = new();
        private readonly ILogger<TarefaManager> _logger;

        public TarefaManager(ITarefaHubControl tarefaHubControl, IInterfaceHubControl interfaceHubControl, ILogger<TarefaManager> logger)
        {
            _interfaceHubControl = interfaceHubControl;
            _tarefaHubControl = tarefaHubControl;
            _logger = logger;
        }

        public bool AddTask(Tarefa task)
        {
            if(tasks.TryAdd(task.IdTarefa, task))
            {
                _interfaceHubControl.UIAddTask(ConvertToTaskInfo(tasks[task.IdTarefa]));
                return true;
            }
                
            return false;
        }

        public bool StartTask(string idTarefa)
        {
            Tarefa taskToStart = tasks[idTarefa];
            taskToStart.InicioTarefa = DateTime.Now;
            taskToStart.Status = "Em progresso - Fase 1";
            if(tasks.TryUpdate(idTarefa, taskToStart, tasks[idTarefa]))
            {
                _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(tasks[idTarefa]));
                return true;
            }
            return false ;
        }

        public bool ChangeTaskStatus(string idTarefa, string status)
        {
            Tarefa taskToUpdate = tasks[idTarefa];
            taskToUpdate.Status = status;
            if (tasks.TryUpdate(idTarefa, taskToUpdate, tasks[idTarefa]))
            {
                _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(tasks[idTarefa]));
                return true;
            }
            return false;
        }

        public Tarefa CompleteTask(string idTarefa)
        {
            Tarefa finishedTask = tasks[idTarefa];
            

            if(!tasks.TryRemove(idTarefa,out finishedTask)) return null;
            finishedTask.FimTarefa = DateTime.Now;
            finishedTask.Status = "Concluído";

            _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(tasks[idTarefa]));

            return finishedTask;
        }

        public List<TaskInfoView> GetListTaskInfo()
        {
            return tasks.Select(x => ConvertToTaskInfo(x.Value)).ToList();
        }
        public Tarefa GetTaskNotInExecution()
        {
            return tasks.FirstOrDefault(x => x.Value.InicioTarefa == null).Value;
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
