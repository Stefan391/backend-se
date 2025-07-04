using backend_se.Data.Models;

namespace backend_se.Common.Consts
{
    public static class StaticData
    {
        public static List<UserModel> Users { get; set; } = new List<UserModel> {
                                                                new UserModel { Id = 1, Name = "Name1", Email = "name1@gmail.com", Username = "username1", Password = "password1", Role = (short)eUserRole.Basic },
                                                                new UserModel { Id = 2, Name = "Name2", Email = "name2@gmail.com", Username = "username2", Password = "password2", Role = (short)eUserRole.Admin },
                                                                new UserModel { Id = 3, Name = "Name3", Email = "name3@gmail.com", Username = "username3", Password = "password3", Role = (short)eUserRole.Basic },
                                                                new UserModel { Id = 4, Name = "Name4", Email = "name4@gmail.com", Username = "username4", Password = "password4", Role = (short)eUserRole.Basic },
                                                                new UserModel { Id = 5, Name = "Name5", Email = "name5@gmail.com", Username = "username5", Password = "password5", Role = (short)eUserRole.Admin }};

        public static List<RefreshTokenModel> RefreshTokens { get; set; } = new List<RefreshTokenModel>();
    }
}
