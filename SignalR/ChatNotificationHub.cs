using backend_se.Common.Consts;
using backend_se.Data.ChatNotificationHub;
using backend_se.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace backend_se.SignalR
{
    [Authorize]
    public class ChatNotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception();

            Groups.AddToGroupAsync(Context.ConnectionId, userId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(SendMessageRequest req)
        {
            if(string.IsNullOrWhiteSpace(req.message))
                return;

            var dbUser = StaticData.Users.FirstOrDefault(x => x.Id == req.userId);
            if (dbUser == null)
                throw new Exception();

            var userId = Context.User?.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            if (string.IsNullOrEmpty(userId) || userId == dbUser.Id.ToString())
                throw new Exception();

            var loggedUser = StaticData.Users.FirstOrDefault(x => x.Id == long.Parse(userId));
            if (loggedUser == null)
                throw new Exception();

            StaticData.ChatHistoryModels.Add(new Data.Models.ChatHistoryModel { Message = req.message, ReceiverId = dbUser.Id, SenderId = loggedUser.Id, SentTime = DateTime.Now });
            await Clients.Group(dbUser.Id.ToString()).SendAsync("ReceiveMessage", new SendMessageResponse { username = loggedUser.Username, userId = loggedUser.Id, senderId = loggedUser.Id, message = req.message, isRead = false, sentTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")});
            await Clients.Group(userId).SendAsync("ReceiveMessage", new SendMessageResponse { username = dbUser.Username, userId = dbUser.Id, senderId = loggedUser.Id, message = req.message, isRead = false, sentTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")});
        }
    }
}
