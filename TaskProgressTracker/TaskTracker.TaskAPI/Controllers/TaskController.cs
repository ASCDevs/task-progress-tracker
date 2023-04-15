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
        public async Task<List<Tarefa>> GetSaved()
        {
            return _tarefaRepo.GetAll();
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

        //Att tarefa

        //Deletar tarefa (Cancelar tarefa)
    }
}
