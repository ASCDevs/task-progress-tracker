using TaskTracker.Domain;

namespace TaskServer.SignalServer.Interfaces
{
    public interface IListInterfaceSocketHolder
    {
        Task AddInterfaceList(HttpContext context);
        Task UpdateStatusTaskInterfaceList(UpdateTaskInfo updateInfo);
        int CountOnlineInterfaces();


    }
}
