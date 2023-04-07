using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Infrastructure.Interfaces;

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
                string guidTask = Guid.NewGuid().ToString(); 
                _logger.LogInformation($"Checagem de tarefas: ativas({_taskServer.CountTasks()}) {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:ff")}");
                _logger.LogInformation($"Inicio tarefa ({guidTask}) {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:ff")} ");

                new Task(() =>
                {
                   DoTask(guidTask);
                }).Start();
                await Task.Delay(5000);
            }
        }

        private async void DoTask(string guidTask)
        {
            _logger.LogInformation($"Doing... - Entrada da task {guidTask}");
            await Task.Delay(15000);
            _logger.LogInformation($"Finish - Saida da task {guidTask} {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:ff")}");
        }
    }
}
