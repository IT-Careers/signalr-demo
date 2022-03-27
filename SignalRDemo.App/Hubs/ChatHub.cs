using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Data;
using SignalRDemo.Data.Entities;

namespace SignalRDemo.App.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext appDbContext;

        public ChatHub(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task SendMessage(string username, string message)
        {
            // TODO: Fix SignalR Auth
            // ZERO Security
            // EVERY USER CAN enter any username and have himself represented as someone else
            var user = this.appDbContext.Users.SingleOrDefault(user => user.Username == username);

            var messageEntity = new Message
            {
                Content = message,
                UserId = user.Id
            };

            this.appDbContext.Add(messageEntity);
            this.appDbContext.SaveChanges();

            await this.Clients.All.SendAsync("ReceiveMessage", $"[{username}]: {message}"); // [Pesho]: Hello World!
        }
    }
}
