using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;
using TaskTracker.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using TaskTracker.Domain.Entities;

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

        public async void AddTarefa(Tarefa tarefa)
        {
            try
            {
                await _connection.InvokeAsync("AddNewTarefa", tarefa);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro API > Signal : " + ex.Message);
            }
        }

        public async void StartTask(Tarefa tarefa)
        {
            await _connection.InvokeAsync("StartTarefa",tarefa);
        }

        public async void UpdateTask(Tarefa tarefa)
        {
            await _connection.InvokeAsync("UpdateTarefa", tarefa);
        }

        public async Task<List<TaskInfoView>> GetTasksOnInfoView()
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

        public async Task<List<Tarefa>> GetTasksObj()
        {
            return await _connection.InvokeAsync<List<Tarefa>>("GetTarefasToDo");
        }

        public async void CompleteTask(Tarefa tarefa)
        {
            await _connection.InvokeAsync("CompleteTarefa",tarefa);
        }
    }
}
