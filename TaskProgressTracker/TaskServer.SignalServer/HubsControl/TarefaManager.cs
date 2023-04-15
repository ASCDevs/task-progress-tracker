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
        //private readonly ConcurrentDictionary<string, Tarefa> tasks = new();
        private readonly ILogger<TarefaManager> _logger;
        private readonly ITarefaRepo _tarefaRepo;

        public TarefaManager(ITarefaHubControl tarefaHubControl, IInterfaceHubControl interfaceHubControl, ILogger<TarefaManager> logger, ITarefaRepo tarefaRepo)
        {
            _interfaceHubControl = interfaceHubControl;
            _tarefaHubControl = tarefaHubControl;
            _logger = logger;
            _tarefaRepo = tarefaRepo;
            _logger.LogInformation("[INSTANCIADO] - Tarefa manager foi instanciada");
        }

        public void UpdateTask(Tarefa task)
        {
            var tarefaOk = _tarefaRepo.Update(task);
            if(tarefaOk != null ) _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(task));
        }

        public void UpdateUI(Tarefa task)
        {
            _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(task));
        }

        public bool StartTask(Tarefa tarefa)
        {
            var tarefaOk = _tarefaRepo.Update(tarefa);
            if(tarefaOk != null)
            {
                _interfaceHubControl.UIUpdateTask(ConvertToTaskInfo(tarefa));
                return true;
            }
            return false ;
        }

        public List<TaskInfoView> GetListTaskInfo()
        {
            return _tarefaRepo.GetAll().Select(x => ConvertToTaskInfo(x)).ToList();
        }

        public List<Tarefa> GetTasksNotInExecution()
        {
            return _tarefaRepo.GetNotExecuted();
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
