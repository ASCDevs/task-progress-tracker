using Microsoft.AspNetCore.SignalR;

namespace TaskServer.SignalServer.Hubs
{
    public class TarefaHub : Hub
    {
        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
        {
            while (true)
            {
                yield return DateTime.Now;
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
