using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using TaskServer.SignalServer.Interfaces;
using TaskServer.SignalServer.SocketsConnections;
using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.Hubs
{
    public class TarefaHub : Hub
    {
        private readonly ILogger<TarefaHub> _logger;
        private readonly ITarefaManager _tarefaManager;

        public TarefaHub(ILogger<TarefaHub> logger, ITarefaManager tarefaManager)
        {
            _logger = logger;
            _tarefaManager = tarefaManager;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                ChannelsForTasksDemand._connectedChannels.Add(Context.ConnectionId);
                //Adicionar notificação de server conectado ao Tarefa Hub
                _logger.LogInformation($"Um canal do Hub de Tarefa foi aberto - {DateTime.Now} - {Context.ConnectionId}");

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(" Erro ao receber conexão na Tarefa Hub (" + DateTime.Now + "), Erro: " + ex.Message);
            }


        }

        //Recebe a tarefa da API
        public void AddNewTarefa(Tarefa task)
        {
            //Encaminha para o Tarefa manager adicionar na lista
            _tarefaManager.AddTask(task);
        }

        public void StartTarefa(Tarefa task)
        {
            _tarefaManager.StartTask(task.IdTarefa);
        }

        public Tarefa GetTarefaToExecute()
        {
            return _tarefaManager.GetTaskNotInExecution();
        }

        public bool ChangeTaskStatus(string idTarefa, string status)
        {
            if(_tarefaManager.ChangeTaskStatus(idTarefa, status)) return true;

            _logger.LogError($"Erro change task status: {status} - id: {idTarefa}");

            return false;
        }

        public bool CompleteTask(string idTarefa)
        {
            if(_tarefaManager.CompleteTask(idTarefa) != null) return true;

            _logger.LogError($"Erro complete task: {idTarefa}");
            return false;
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                ChannelsForTasksDemand._connectedChannels.Remove(Context.ConnectionId);
                _logger.LogInformation($"Um canal do Hub de Tarefa foi fechado - {DateTime.Now} - {Context.ConnectionId}");
                //Notificar desconexão de server da tarefa hub

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(" Erro ao receber conexão na Tarefa Hub (" + DateTime.Now + "), Erro: " + ex.Message);
            }

        }
    }
}
