using System.Collections.Concurrent;
using System.Net.WebSockets;
using TaskServer.SignalServer.Interfaces;
using TaskTracker.Domain;

namespace TaskServer.SignalServer.SocketsConnections
{
    public class ListInterfaceSocketHolder : IListInterfaceSocketHolder
    {
        private readonly ILogger<ListInterfaceSocketHolder> _logger;
        private readonly ConcurrentDictionary<string, UIListInterfaceConnection> interfaces = new();
        private readonly ITarefaHubControl _taskHubControl;

        public ListInterfaceSocketHolder(ILogger<ListInterfaceSocketHolder> logger, ITarefaHubControl taskHubControl)
        {
            _logger = logger;
            _taskHubControl = taskHubControl;
        }
        public async Task AddInterfaceList(HttpContext context)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            try
            {
                string idConnection = Guid.NewGuid().ToString();
                if (interfaces.TryAdd(idConnection,new UIListInterfaceConnection(webSocket,idConnection)))
                {
                    _logger.LogInformation($"Interface Conectada! id - {idConnection}");
                }
            }
            catch (Exception ex)
            {
                //fazer tratativa de remoção de conexão de interface
                _logger.LogError("Erro ao adicionar conexão de interface");
            }

        }

        //private async Task EchoAsycn(WebSocket webSocket)
        //{
        //    try
        //    {
        //        byte[] butter = new byte[1024*];
        //        while (true)
        //        {
        //            WebSocketReceiveResult result = await webSocket.ReceiveAsync( new ArraySegment<byte>)
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError("Erro ao observar interface conectada");
        //    }
        //}

        public int CountOnlineInterfaces()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStatusTaskInterfaceList(TaskInfoView updateInfo)
        {
            throw new NotImplementedException();
        }
    }
}
