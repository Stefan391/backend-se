using backend_se.Common.Consts;
using backend_se.Common.Controllers;
using backend_se.Common.Helpers;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Providers;
using backend_se.Data.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_se.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseController
    {
        private UserProvider _userProvider;
        public ChatController(IDataProvider<UserModel, UserSearch> userProvider)
        {
            _userProvider = (UserProvider)userProvider;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var res = new List<ChatHistoryResponse>();

            var messageList = StaticData.ChatHistory.Where(x => x.SenderId == UserId || x.ReceiverId == UserId)
                                .SelectMany(x => new[] { new { x.Id, OtherUserId = (x.SenderId != UserId ? x.SenderId : x.ReceiverId), x.Message, x.SenderId, x.ReceiverId, x.SentTime, x.ReadTime } })
                                .GroupBy(x => x.OtherUserId)
                                .Select(x => x.OrderByDescending(y => y.SentTime).FirstOrDefault())
                                .Where(x => x != null)
                                .Take(10)
                                .ToList().OrderByDescending(x => x!.SentTime);

            foreach (var msg in messageList)
            {
                if (msg == null)
                    continue;

                var user = StaticData.Users.FirstOrDefault(x => x.Id == msg.OtherUserId);
                if (user == null)
                    continue;

                var unreadCount = msg.ReadTime != null && msg.SenderId != UserId ? 0 : StaticData.ChatHistory.Where(x => x.SenderId == user.Id && x.ReceiverId == UserId && x.ReadTime == null).Take(11).ToList().Count;
                res.Add(new ChatHistoryResponse { messageId = msg.Id, userId = user.Id, username = user.Username, message = msg.Message, senderId = msg.SenderId, isRead = msg.ReadTime != null, sentTime = msg.SentTime.ToString("yyyy-MM-ddTHH:mm:ss"), unreadCount = unreadCount });
            }

            return Ok(res);
        }

        [HttpGet("message-history")]
        [Authorize]
        public IActionResult GetMessageHistory(long userId, long? lastMessageId = null)
        {
            var user = _userProvider.GetById(userId);
            if (user == null || userId == UserId)
                return BadRequest();

            var res = new MessageHistoryResponse();

            var finalMessage = StaticData.ChatHistory.Where(x => ((x.SenderId == UserId && x.ReceiverId == user.Id) || (x.SenderId == user.Id && x.ReceiverId == UserId))).OrderBy(x => x.SentTime).FirstOrDefault();
            if (finalMessage == null)
                return Ok(res);

            var chatHistory = StaticData.ChatHistory.Where(x => ((x.SenderId == UserId && x.ReceiverId == user.Id) || (x.SenderId == user.Id && x.ReceiverId == UserId)) && (lastMessageId == null || x.Id < lastMessageId)).OrderByDescending(x => x.SentTime).Take(25).OrderBy(x => x.SentTime);
            res.isLastMessage = chatHistory.Any(x => x.Id == finalMessage.Id);
            foreach (var msg in chatHistory)
                res.list.Add(new MessageHistoryDTO { senderId = msg.SenderId, isRead = msg.ReadTime != null, message = msg.Message, sentTime = msg.SentTime.ToString("yyyy-MM-ddTHH:mm:ss"), messageId = msg.Id, userId = (msg.SenderId == UserId ? msg.ReceiverId : msg.SenderId) });

            return Ok(res);
        }

        [HttpGet("chatuser")]
        public IActionResult GetChatUser(long userId)
        {
            var user = _userProvider.GetById(userId);

            if (user == null)
                return BadRequest("User doesn't exist");

            return Ok(new ChatUserDTO { UserId = user.Id, Username = user.Username });
        }
    }
}
