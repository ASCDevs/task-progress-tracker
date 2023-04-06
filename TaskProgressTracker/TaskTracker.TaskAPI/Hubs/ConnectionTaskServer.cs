using Microsoft.AspNetCore.SignalR.Client;
using TaskTracker.Domain;
using TaskTracker.TaskAPI.Controllers;

namespace TaskTracker.TaskAPI.Hubs
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
                _logger.LogError("Erro API > Signal : "+ex.Message);
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
    }
}
