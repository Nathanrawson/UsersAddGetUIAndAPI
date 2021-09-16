using UsersApi.Eums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Managers
{
    public interface IUserManager: IRepository<UserModel>
    {
    }
}
