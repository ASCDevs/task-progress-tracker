using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;
using TaskTracker.Infrastructure.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TaskTracker.BackgroundTasks
{
    public class CheckerOldTasks : BackgroundService
    {
        private readonly ILogger<CheckerOldTasks> _logger;
        private readonly IConnectionTaskServer _taskServer;
        public CheckerOldTasks(ILogger<CheckerOldTasks> logger, IConnectionTaskServer taskServer)
        {
            _logger = logger;
            _taskServer = taskServer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Tarefa> tasksToDo = _taskServer.GetTasksObj().Result; 
                Parallel.ForEach(tasksToDo, tarefa => DoTask(tarefa));
                await Task.Delay(15000);
            }
        }

        private async void DoTask(Tarefa tarefa)
        {
            Random rdn = new Random();
            int[] intervalos = new int[] { 15000, 30000, 60000, 20000 };

            tarefa.InicioTarefa = DateTime.Now;
            tarefa.Status = "Em progresso - Fase 1";

            _taskServer.StartTask(tarefa);
            await Task.Delay(intervalos[rdn.Next(intervalos.Length)]);
            for (int i = 0;i < rdn.Next(intervalos.Length); i++)
            {
                int nFase = Int32.Parse(tarefa.Status.Substring(tarefa.Status.Length - 1));
                tarefa.Status = $"Em progesso - Fase{nFase}";
                _taskServer.UpdateTask(tarefa);
                await Task.Delay(intervalos[rdn.Next(intervalos.Length)]);
            }

            tarefa.Status = "Concluído";
            _taskServer.CompleteTask(tarefa);
        }
    }
}
