using backend_se.Data.Models;

namespace backend_se.Common.Consts
{
    public static class StaticData
    {
        public static List<UserModel> Users { get; set; } = new List<UserModel> {
                                                                new() { Id = 1, Name = "Name1", Email = "name1@gmail.com", Username = "username1", Password = "password1", Role = (short)eUserRole.Basic },
                                                                new() { Id = 2, Name = "Name2", Email = "name2@gmail.com", Username = "username2", Password = "password2", Role = (short)eUserRole.Admin },
                                                                new() { Id = 3, Name = "Name3", Email = "name3@gmail.com", Username = "username3", Password = "password3", Role = (short)eUserRole.Basic },
                                                                new() { Id = 4, Name = "Name4", Email = "name4@gmail.com", Username = "username4", Password = "password4", Role = (short)eUserRole.Basic },
                                                                new() { Id = 5, Name = "Name5", Email = "name5@gmail.com", Username = "username5", Password = "password5", Role = (short)eUserRole.Admin }};

        public static List<RefreshTokenModel> RefreshTokens { get; set; } = new List<RefreshTokenModel>();
        public static List<ChatHistoryModel> ChatHistory { get; set; } = new List<ChatHistoryModel>
        {
            new() { Id = 1, Message = "message1", SenderId = 2, ReceiverId = 1, SentTime = new DateTime(2025, 7, 18, 4, 28, 26) },
            new() { Id = 2, Message = "message2", SenderId = 3, ReceiverId = 1, SentTime = new DateTime(2025, 7, 17, 7, 16, 35) },
            new() { Id = 3, Message = "message3", SenderId = 4, ReceiverId = 1, SentTime = new DateTime(2025, 7, 16, 9, 10, 7) },
            new() { Id = 4, Message = "message4", SenderId = 3, ReceiverId = 2, SentTime = new DateTime(2025, 7, 15, 7, 16, 35) }
        };
    }
}
