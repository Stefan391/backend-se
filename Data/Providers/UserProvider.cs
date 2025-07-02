using backend_se.Common.Consts;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using System.Reflection;

namespace backend_se.Data.Providers
{
    public class UserProvider : IDataProvider<UserModel>
    {
        private static List<UserModel> _users { get; set; } = new List<UserModel> {
                                                                new UserModel { Id = 1, Name = "Name1", Email = "name1@gmail.com", Username = "username1", Password = "password1", Role = (short)eUserRole.Basic },
                                                                new UserModel { Id = 2, Name = "Name2", Email = "name2@gmail.com", Username = "username2", Password = "password2", Role = (short)eUserRole.Admin },
                                                                new UserModel { Id = 3, Name = "Name3", Email = "name3@gmail.com", Username = "username3", Password = "password3", Role = (short)eUserRole.Basic },
                                                                new UserModel { Id = 4, Name = "Name4", Email = "name4@gmail.com", Username = "username4", Password = "password4", Role = (short)eUserRole.Basic },
                                                                new UserModel { Id = 5, Name = "Name5", Email = "name5@gmail.com", Username = "username5", Password = "password5", Role = (short)eUserRole.Admin }};

        public UserModel? GetById(long id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public UserModel? Add(UserModel model)
        {
            if (_users.FirstOrDefault(x => x.Id == model.Id) != null)
                return null;

            _users.Add(model);

            return model;
        }

        public UserModel? Update(UserModel model)
        {
            if (_users.FirstOrDefault(x => x.Id == model.Id) == null)
                return null;

            var user = _users.FirstOrDefault(x => x.Id == model.Id);
            user = model;

            return user;
        }

        public bool Delete(long id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return false;

            _users.Remove(user);

            return true;
        }

        public List<UserModel> GetAll()
        {
            return _users.ToList();
        }

        public UserModel? Login(LoginDTO req)
        {
            var user = _users.FirstOrDefault(x => (x.Username == req.username || x.Email == req.username) && x.Password == req.password);

            return user;
        }
    }
}
