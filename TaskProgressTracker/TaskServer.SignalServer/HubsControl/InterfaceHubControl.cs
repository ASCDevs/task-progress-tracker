using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Threading.Tasks;
using TaskServer.SignalServer.Hubs;
using TaskServer.SignalServer.Interfaces;
using TaskTracker.Domain;

namespace TaskServer.SignalServer.HubsControl
{
    public class InterfaceHubControl : IInterfaceHubControl
    {
        private readonly ILogger<InterfaceHubControl> _logger;
        private IHubContext<InterfacesHub> _interfacesHub;

        public InterfaceHubControl(IHubContext<InterfacesHub> interfacesHub, ILogger<InterfaceHubControl> logger) 
        {
            _logger = logger;
            _interfacesHub = interfacesHub;
        }

        public async Task UIAddTask(TaskInfoView task)
        {
            try
            {
                await _interfacesHub.Clients.All.SendAsync("addMostRecentTask",task);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro enviar AddTask para o hub Interfaces [{ex.Message}]");
            }
        }

        public async Task UISendAvailablesTasks(List<TaskInfoView> tasks)
        {
            try
            {
                await _interfacesHub.Clients.All.SendAsync("receiveAvailablesTasks", tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro enviar taks disponíveis para o hub Interfaces [{ex.Message}]");
            }
        }

        public async Task UIUpdateTask(TaskInfoView task)
        {
            try
            {
                await _interfacesHub.Clients.All.SendAsync("updateTask", task);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro enviar update de task para o hub Interfaces [{ex.Message}]");
            }
        }
    }
}
