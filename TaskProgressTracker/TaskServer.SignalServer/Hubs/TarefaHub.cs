using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using TaskServer.SignalServer.SocketsConnections;

namespace TaskServer.SignalServer.Hubs
{
    public class TarefaHub : Hub
    {
        private readonly ILogger<TarefaHub> _logger;

        public TarefaHub(ILogger<TarefaHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                ChannelsForTasksDemand._connectedChannels.Add(Context.ConnectionId);
                await Clients.AllExcept(Context.ConnectionId).SendAsync("sendPanelLog", "Um painel conectou (" + DateTime.Now + ")");
                _logger.LogInformation($"Um canal do Hub de Tarefa foi aberto - {DateTime.Now}");

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[Erro PanelHub] > Erro ao receber conexão na Tarefa Hub (" + DateTime.Now + "), Erro: " + ex.Message);
            }


        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                PanelsHandler._connectedPanels.Remove(Context.ConnectionId);
                Console.WriteLine("[Panel off] Painel " + Context.ConnectionId + " desconectou");
                await Clients.All.SendAsync("updatePanelsOn", PanelsHandler._connectedPanels.Count());
                await Clients.All.SendAsync("updateClientsOn", _webSocket.CountClients());
                await Clients.AllExcept(Context.ConnectionId).SendAsync("sendPanelLog", "Um painel desconectou (" + DateTime.Now + ")");
                await base.OnConnectedAsync();

                _logger.LogInformation("[Info PanelHub] Painel de monitoramento foi desconectado (" + DateTime.Now + "), conn-id: " + Context.ConnectionId + ")");
            }
            catch (Exception ex)
            {
                _logger.LogError("[Erro PanelHub] > Erro em desconectar um Painel de monitoramento (" + DateTime.Now + "), conn-id" + Context.ConnectionId + ", Erro: " + ex.Message);
            }

        }
    }
}
