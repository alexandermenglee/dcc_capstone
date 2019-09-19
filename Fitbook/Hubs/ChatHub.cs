using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Fitbook.Models;
using Newtonsoft.Json;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            /*var serializedMessage = JsonConvert.SerializeObject(message);*/
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);

        }
    }
}