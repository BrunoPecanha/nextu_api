using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace UFF.Service.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendTestMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
