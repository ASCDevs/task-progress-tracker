using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using System.Threading.Tasks;
using TaskServer.SignalServer.Interfaces;
using TaskServer.SignalServer.SocketsConnections;
using TaskTracker.Domain;
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

        public List<Tarefa> GetTarefasToDo()
        {
            return _tarefaManager.GetTasksNotInExecution();
        }


        public List<TaskInfoView> GetTarefas()
        {
            return _tarefaManager.GetListTaskInfo();
        }


        public void UpdateTarefa(Tarefa task)
        {
            _tarefaManager.UpdateTask(task);
        }

        public void UpdateUI(Tarefa task)
        {
            _tarefaManager.UpdateUI(task);
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
