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
            if (string.IsNullOrWhiteSpace(req.message) || req.message.Length > 100)
                return;

            var dbUser = StaticData.Users.FirstOrDefault(x => x.Id == req.userId);
            if (dbUser == null)
                throw new Exception();

            var u = Context.User?.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            if (!long.TryParse(u, out long userId) || userId == dbUser.Id)
                throw new Exception();

            var loggedUser = StaticData.Users.FirstOrDefault(x => x.Id == userId);
            if (loggedUser == null)
                throw new Exception();

            var lid = StaticData.ChatHistory.OrderByDescending(x => x.Id).FirstOrDefault();
            var id = lid == null ? 1 : lid.Id + 1;
            StaticData.ChatHistory.Add(new Data.Models.ChatHistoryModel { Id = id, Message = req.message, ReceiverId = dbUser.Id, SenderId = loggedUser.Id, SentTime = DateTime.Now });
            var unreadCount = StaticData.ChatHistory.Where(x => x.SenderId == loggedUser.Id && x.ReceiverId == dbUser.Id && x.ReadTime == null).Take(11).ToList().Count;
            await Clients.Group(dbUser.Id.ToString()).SendAsync("ReceiveMessage", new SendMessageResponse { messageId = id, username = loggedUser.Username, userId = loggedUser.Id, senderId = loggedUser.Id, message = req.message, isRead = false, sentTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), unreadCount = unreadCount });
            await Clients.Group(userId.ToString()).SendAsync("ReceiveMessage", new SendMessageResponse { messageId = id, username = dbUser.Username, userId = dbUser.Id, senderId = loggedUser.Id, message = req.message, isRead = false, sentTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), unreadCount = 0 });
        }

        public async Task ReadMessage(ReadMessageRequest req)
        {
            var u = Context.User?.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            if (!long.TryParse(u, out long userId))
                throw new Exception();

            var message = StaticData.ChatHistory.FirstOrDefault(x => x.Id == req.messageId && x.ReceiverId == userId);
            if (message == null)
                return;

            var msgs = StaticData.ChatHistory.Where(x => x.SenderId == message.SenderId && x.ReceiverId == userId && x.ReadTime == null).ToList();
            foreach (var msg in msgs)
                msg.ReadTime = DateTime.Now;

            await Clients.Group(message.SenderId.ToString()).SendAsync("MessageRead", new ReadMessageRequest { messageId = message.Id, readerId = message.ReceiverId });
        }

        public async Task StartTyping(StartTypingRequest req)
        {
            var u = Context.User?.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            if (!long.TryParse(u, out long userId) || userId != req.senderId || req.senderId == req.receiverId)
                throw new Exception();

            await Clients.Group(req.receiverId.ToString()).SendAsync("StartedTyping", req);
        }
    }
}
