using Microsoft.AspNetCore.Mvc;
using TaskTracker.Domain.Contracts;
using TaskTracker.Domain.Entities;
using TaskTracker.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskTracker.TaskAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }
        // GET: api/<TaskController>
        [HttpGet]
        public List<Tarefa> Get()
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            tarefas = TasksEmExecucao.TaskList.Select(t => t.Value).ToList();
            return tarefas;
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public Tarefa Get(string id)
        {
            Tarefa tarefa = TasksEmExecucao.TaskList.FirstOrDefault(t => t.Value.IdTarefa == id).Value;
            return tarefa;
        }

        // POST api/<TaskController>
        [HttpPost]
        public void Post([FromBody] TarefaRequest tarefa)
        {
            string idGuid = Guid.NewGuid().ToString();
            TasksEmExecucao.TaskList.TryAdd(idGuid, new Tarefa
            {
                IdTarefa = idGuid,
                NomeTarefa = tarefa.NomeTarefa,
                PedidoTarefa = DateTime.Now,
                Status = "Solicitado"
            });
        }

        // PUT api/<TaskController>/5
        [HttpPut]
        public void Put([FromBody] TarefaRequest tarefa)
        {
            _logger.LogInformation("Solicitado atualização");
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public Tarefa Delete(string id)
        {
            Tarefa tarefaRemovida;
            if (TasksEmExecucao.TaskList.TryRemove(id, out tarefaRemovida)){
                return tarefaRemovida;
            }

            return tarefaRemovida;
        }

        [HttpGet("total")]
        public int TotalTarefas()
        {
            return TasksEmExecucao.CountTasks();
        }
    }
}
