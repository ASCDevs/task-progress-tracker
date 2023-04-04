using Microsoft.AspNetCore.SignalR;
using TaskServer.SignalServer.Hubs;
using TaskServer.SignalServer.Interfaces;
using TaskTracker.Domain.Entities;

namespace TaskServer.SignalServer.HubsControl
{
    public class TarefaHubControl : ITarefaHubControl
    {
        private readonly ILogger<TarefaHubControl> _logger;
        private readonly IHubContext<TarefaHub> _tarefaHub;

        public TarefaHubControl(ILogger<TarefaHubControl> logger, IHubContext<TarefaHub> tarefaHub)
        {
            _logger = logger;
            _tarefaHub = tarefaHub;
        }

        public Task ChangeStatusTask()
        {
            throw new NotImplementedException();
        }

        public Task CompleteTask()
        {
            throw new NotImplementedException();
        }

        public int CountTasks()
        {
            throw new NotImplementedException();
        }

        public Task GetAllTasks()
        {
            throw new NotImplementedException();
        }
    }
}
