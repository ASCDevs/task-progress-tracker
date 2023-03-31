using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain
{
    public class UIListInterfaceConnection
    {
        public string IdConnection { get; set; }
        public DateTime? DtConexao { get; set; }
        public WebSocket WebSocketConn { get; set; }
        public UIListInterfaceConnection(WebSocket webSocket, string idConn) 
        { 
            IdConnection = idConn;
            WebSocketConn = webSocket;
            DtConexao = DateTime.Now;
        }
        public UIListInterfaceConnection()
        {
            
        }
        
    }
}
