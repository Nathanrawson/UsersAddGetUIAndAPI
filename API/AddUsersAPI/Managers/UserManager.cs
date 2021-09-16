using UsersApi.Eums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Managers
{
    public class UserManager: IUserManager
    {
        private IMemoryCache users;

        public UserManager(IMemoryCache _users)
        {
            users = _users;
        }

        public UserManager()
        {
            users = new MemoryCache(new MemoryCacheOptions());
        }

        public List<UserModel> GetAll(List<string> names = null)
        {
            var allUsers = names == null ? users?.Get<List<UserModel>>("users") :
                users.Get<List<UserModel>>("users").Where(x => names.Contains(x.Name) && x.Surname != null);

            if(allUsers == null)
            {
                SeedUserCache();
                GetAll(names);
            }

            return allUsers?.ToList();
        }

        public UserModel Get(string name)
        {
            return users.Get<List<UserModel>>("users").Where(x => x.Name == name).FirstOrDefault() ;
        }

        protected void SeedUserCache()
        {
            var strings = new List<string>()
            {
                "Dr Nice",
                "Narco",
                "Bombasto",
                "Celeritas",
            };

            var userList = new List<UserModel>();

            for(var i = 0; i< strings.Count; i++)
            {
                userList.Add(new UserModel() {Id= i.ToString(), Name = strings[i] });
            }

            users.Set<List<UserModel>>("users", userList);
        }

        public Enum Add(UserModel model)
        {          
            try
            {
                var userList = users?.Get<List<UserModel>>("users");

                if (userList == null)
                {
                    users.Set<List<UserModel>>("users", new List<UserModel>() { model });
                }
                else if(!userList.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Surname.ToLower() == model.Surname.ToLower()))
                {
                    userList.Add(model);
                    users.Set<List<UserModel>>("users", userList);
                }
                else
                {
                    return UserResultEnum.Duplicate;
                }
            }

            catch
            {
                return UserResultEnum.Failed;
            }

            return UserResultEnum.Success;
        }

    
    }
}
