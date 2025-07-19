using backend_se.Common.Consts;
using backend_se.Common.Controllers;
using backend_se.Common.Helpers;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Providers;
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
        public ChatController(IDataProvider<UserModel> userProvider)
        {
            _userProvider = (UserProvider)userProvider;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var res = new List<ChatDTO>();

            var messageList = StaticData.ChatHistoryModels.Where(x => x.SenderId == UserId || x.ReceiverId == UserId)
                                .SelectMany(x => new[] { new { OtherUserId = (x.SenderId != UserId ? x.SenderId : x.ReceiverId), x.Message, x.SenderId, x.ReceiverId, x.SentTime } })
                                .GroupBy(x => x.OtherUserId)
                                .Select(x => x.OrderByDescending(y => y.SentTime)
                                .FirstOrDefault())
                                .ToList();

            foreach(var msg in messageList)
            {
                if (msg == null)
                    continue;

                var user = StaticData.Users.FirstOrDefault(x => x.Id == msg.OtherUserId);
                if (user == null)
                    continue;

                res.Add(new ChatDTO { userId = user.Id, username = user.Username, message = msg.Message, senderId = msg.SenderId, isRead = true, sentTime = msg.SentTime.ToString("yyyy-MM-ddTHH:mm:ss") });
            }

            return Ok(res);
        }

        [HttpGet("message-history")]
        [Authorize]
        public IActionResult GetMessageHistory(long userId)
        {
            var user = _userProvider.GetById(userId);
            if (user == null || userId == UserId)
                return BadRequest();

            var res = new List<MessagesHistory>();
            var chatHistory = StaticData.ChatHistoryModels.Where(x => (x.SenderId == UserId && x.ReceiverId == user.Id) || (x.SenderId == user.Id && x.ReceiverId == UserId)).OrderByDescending(x => x.SentTime).Take(20).OrderBy(x => x.SentTime);
            foreach (var msg in chatHistory)
                res.Add(new MessagesHistory { senderId = msg.SenderId, isRead = true, message = msg.Message, sentTime = msg.SentTime.ToString("yyyy-MM-ddTHH:mm:ss") });

            return Ok(chatHistory);
        }
    }
}
