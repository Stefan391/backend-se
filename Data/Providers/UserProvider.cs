using backend_se.Common.Consts;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Search;
using System.Reflection;

namespace backend_se.Data.Providers
{
    public class UserProvider : IDataProvider<UserModel, UserSearch>
    {
        public UserModel? GetById(long id)
        {
            return StaticData.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserModel? Add(UserModel model)
        {
            if (StaticData.Users.FirstOrDefault(x => x.Id == model.Id) != null)
                return null;

            StaticData.Users.Add(model);

            return model;
        }

        public UserModel? Update(UserModel model)
        {
            if (StaticData.Users.FirstOrDefault(x => x.Id == model.Id) == null)
                return null;

            var user = StaticData.Users.FirstOrDefault(x => x.Id == model.Id);
            user = model;

            return user;
        }

        public UserModel? Save(UserModel model)
        {
            if(StaticData.Users.Where(x => x.Id == model.Id) != null)
            {
                var user = StaticData.Users.FirstOrDefault(x => x.Id == model.Id);
                user = model;
                return user;
            }

            StaticData.Users.Add(model);
            return model;
        }

        public bool Delete(long id)
        {
            var user = StaticData.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return false;

            StaticData.Users.Remove(user);

            return true;
        }

        public IQueryable<UserModel> GetList(UserSearch search)
        {
            return StaticData.Users.AsQueryable();
        }

        public UserModel? Login(LoginDTO req)
        {
            var user = StaticData.Users.FirstOrDefault(x => (x.Username == req.username || x.Email == req.username) && x.Password == req.password);

            return user;
        }
    }
}
