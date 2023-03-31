using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;

namespace TaskServer.SignalServer.Hubs
{
    public class TarefaHub : Hub
    {
        private readonly ILogger<TarefaHub> _logger;

        public TarefaHub(ILogger<TarefaHub> logger)
        {
            _logger = logger;
        }
        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
        {
            while (true)
            {
                yield return DateTime.Now;
                await Task.Delay(1000, cancellationToken);
            }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                PanelsHandler._connectedPanels.Add(Context.ConnectionId);
                Console.WriteLine("[Panel on] Painel " + Context.ConnectionId + " conectou (" + DateTime.Now + ")");
                await Clients.All.SendAsync("updatePanelsOn", PanelsHandler._connectedPanels.Count());
                await Clients.All.SendAsync("updateQtdUsersOnline", CostumersHandler._connectedCostumers.Count());
                await Clients.All.SendAsync("updateClientsOn", _webSocket.CountClients());
                await GetListClientsOn();
                await Clients.AllExcept(Context.ConnectionId).SendAsync("sendPanelLog", "Um painel conectou (" + DateTime.Now + ")");
                _logger.LogInformation("[Info PanelHub] Painel de monitoramento foi conectado (" + DateTime.Now + "), conn-id: " + Context.ConnectionId + ")");

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("[Erro PanelHub] > Erro ao receber conexão no Hub (" + DateTime.Now + "), Erro: " + ex.Message);
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
