using Microsoft.AspNetCore.Mvc;
using TaskTracker.Domain;
using TaskTracker.Domain.Contracts;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Repositories;
using TaskTracker.Infrastructure.ConnectionsServices;
using TaskTracker.Infrastructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskTracker.TaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IConnectionTaskServer _taskServer;
        private readonly ITarefaRepo _tarefaRepo;

        public TaskController(ILogger<TaskController> logger, IConnectionTaskServer taskServer, ITarefaRepo tarefaRepo)
        {
            _logger = logger;
            _taskServer = taskServer;
            _tarefaRepo = tarefaRepo;
        }
        // GET: api/<TaskController>
        [HttpGet("inexecution")]
        public async Task<List<TaskInfoView>> Get()
        {
            return await _taskServer.GetTasksOnInfoView();
        }

        [HttpGet("saved")]
        public async Task<List<TaskInfoView>> GetSaved()
        {
            return _tarefaRepo.GetAll()?.Select(t => ConvertToTaskInfo(t))?.ToList() ?? new List<TaskInfoView>();
        }

        // POST api/<TaskController>
        [HttpPost]
        public void Post([FromBody] TarefaRequest tarefa)
        {
            var novaTask = new Tarefa
            {
                NomeTarefa = tarefa.NomeTarefa,
            };
            try
            {
                var tarefaOk  = _tarefaRepo.Add(novaTask);
                if (tarefaOk != null) _taskServer.UpdateUI(tarefaOk);
            }
            catch (Exception ex)
            {
                _logger.LogError("[Erro] Erro ao adicionar> " + ex.Message);
            }
            
        }

        private TaskInfoView ConvertToTaskInfo(Tarefa task)
        {
            return new TaskInfoView
            {
                id = task.Id,
                idTask = task.IdTarefa ?? "-",
                dtSolicitacao = task.PedidoTarefa?.ToString("dd/MM/yyyy HH:mm:ss") ?? "-",
                dtFinalizacao = task.FimTarefa?.ToString("dd/MM/yyyy HH:mm:ss") ?? "-",
                dtInicio = task.InicioTarefa?.ToString("dd/MM/yyyy HH:mm:ss") ?? "-",
                status = task.Status ?? "-",
                taskName = task.NomeTarefa ?? "-"
            };
        }

        //Att tarefa

        //Deletar tarefa (Cancelar tarefa)
    }
}
