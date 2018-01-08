using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using WebRole1;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
        public override Task OnConnected()
        {
            LogState.logs.Add($"{DateTime.Now}: Connected: "+ Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            LogState.logs.Add($"{DateTime.Now}: DisConnected: {Context.ConnectionId}");
            return base.OnDisconnected(stopCalled);
        }
    }
}