using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;
using TaskTracker.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace TaskTracker.Infrastructure.ConnectionsServices
{
    public class ConnectionTaskServer : IConnectionTaskServer
    {
        private readonly ILogger<ConnectionTaskServer> _logger;
        const string url = "https://localhost:7006/tarefas";
        private readonly HubConnection _connection;

        public ConnectionTaskServer(ILogger<ConnectionTaskServer> logger)
        {

            _connection = new HubConnectionBuilder().WithUrl(url).Build();
            _connection.StartAsync();
        }

        public async void AddTaskTeste()
        {
            try
            {
                await _connection.InvokeAsync("AddTarefaTest");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro API > Signal : " + ex.Message);
            }
        }

        public async Task<List<TaskInfoView>> GetTasksOn()
        {
            List<TaskInfoView> result = new();
            try
            {
                return await _connection.InvokeAsync<List<TaskInfoView>>("GetTarefas");
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro API > Signal : " + ex.Message);
            }
            return result;
        }

        public int CountTasks()
        {
            return _connection.InvokeAsync<List<TaskInfoView>>("GetTarefas").Result.Count; ;
        }
    }
}
