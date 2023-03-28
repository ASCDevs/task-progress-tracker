using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Persistence;

namespace TaskTracker.BackgroundTasks
{
    public class CheckerOldTasks : BackgroundService
    {
        private readonly ILogger<CheckerOldTasks> _logger;
        public CheckerOldTasks(ILogger<CheckerOldTasks> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Checagem de tarefas: ativas({TasksEmExecucao.CountTasks()}) {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:ff")}");
                await Task.Delay(5000);
            }
        }
    }
}
