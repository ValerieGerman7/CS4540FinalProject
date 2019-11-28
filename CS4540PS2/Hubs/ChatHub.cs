using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int sender, int receiver, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, receiver, message);
        }
    }
}