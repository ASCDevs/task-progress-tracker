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
    public class CheckerNewTasks : BackgroundService
    {
        private readonly ILogger<CheckerNewTasks> _logger;
        private readonly IConnectionTaskServer _taskServer;
        public CheckerNewTasks(ILogger<CheckerNewTasks> logger, IConnectionTaskServer taskServer)
        {
            _logger = logger;
            _taskServer = taskServer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Tarefa> tasksToDo = _taskServer.GetTasksObj().Result; 
                if(tasksToDo.Any())
                {
                    _logger.LogInformation($"Tarefas Encontradas ({tasksToDo.Count}) - {DateTime.Now} ");
                    Parallel.ForEach(tasksToDo, tarefa => DoTask(tarefa));
                }
                else
                {
                    _logger.LogInformation($"Sem tarefas - {DateTime.Now}");
                }
                await Task.Delay(15000);
            }
        }

        private async void DoTask(Tarefa tarefa)
        {
            _logger.LogInformation($"Fazendo - {tarefa.NomeTarefa}");
            Random rdn = new Random();
            int[] intervalos = new int[] { 15000, 30000, 60000, 20000 };

            tarefa.InicioTarefa = DateTime.Now;
            tarefa.Status = "Em progresso - Fase 1";

            _taskServer.StartTask(tarefa);
            
            int intervalo = intervalos[rdn.Next(intervalos.Length)];
            _logger.LogInformation($"{tarefa.NomeTarefa} - Intervalo: {intervalo}");

            await Task.Delay(intervalo);
            int fases = rdn.Next(intervalos.Length);

            _logger.LogInformation($"{tarefa.NomeTarefa} - Fases: {fases}");
            for (int i = 0;i < fases; i++)
            {
                int nFase = Int32.Parse(tarefa.Status.Substring(tarefa.Status.Length - 1));
                tarefa.Status = $"Em progesso - Fase {nFase+1}";
                _taskServer.UpdateTask(tarefa);

                _logger.LogInformation($"{tarefa.NomeTarefa} - {tarefa.Status}");

                int esperarPor = intervalos[rdn.Next(intervalos.Length)];
                _logger.LogInformation($"{tarefa.NomeTarefa} - Esperar por: {esperarPor}");
                await Task.Delay(esperarPor);
            }

            tarefa.Status = "Concluído";
            tarefa.FimTarefa = DateTime.Now;
            _logger.LogInformation($"{tarefa.NomeTarefa} - Concluído");
            _taskServer.CompleteTask(tarefa);
        }
    }
}
