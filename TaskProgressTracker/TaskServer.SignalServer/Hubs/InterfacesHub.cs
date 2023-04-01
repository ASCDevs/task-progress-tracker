using Microsoft.AspNetCore.SignalR;
using TaskServer.SignalServer.SocketsConnections;

namespace TaskServer.SignalServer.Hubs
{
    public class InterfacesHub : Hub
    {
        private readonly ILogger<InterfacesHub> _logger;

        public InterfacesHub(ILogger<InterfacesHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                ChannelsForUIConnections._connectedUIs.Add(Context.ConnectionId);
                await Clients.All.SendAsync("updateCountUIs", ChannelsForUIConnections._connectedUIs.Count);
                _logger.LogInformation($"Interface foi conectada - {DateTime.Now}");

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na conexão do Hub de interfaces - [{ex.Message}]");
            }
        }
    }
}
