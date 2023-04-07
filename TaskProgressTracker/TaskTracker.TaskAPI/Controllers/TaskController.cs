using Microsoft.AspNetCore.Mvc;
using TaskTracker.Domain;
using TaskTracker.Domain.Contracts;
using TaskTracker.Domain.Entities;
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

        public TaskController(ILogger<TaskController> logger, IConnectionTaskServer taskServer)
        {
            _logger = logger;
            _taskServer = taskServer;
        }
        // GET: api/<TaskController>
        [HttpGet]
        public async Task<List<TaskInfoView>> Get()
        {
            return await _taskServer.GetTasksOn();
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public Tarefa Get(string id)
        {
            //Tarefa tarefa = TasksEmExecucao.TaskList.FirstOrDefault(t => t.Value.IdTarefa == id).Value;
            return null;
        }

        // POST api/<TaskController>
        [HttpPost]
        public void Post([FromBody] TarefaRequest tarefa)
        {
            _taskServer.AddTaskTeste();
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
            //Tarefa tarefaRemovida;
            //if (TasksEmExecucao.TaskList.TryRemove(id, out tarefaRemovida)){
            //    return tarefaRemovida;
            //}

            return null;
        }

        [HttpGet("total")]
        public int TotalTarefas()
        {
            return _taskServer.CountTasks();
        }
    }
}
