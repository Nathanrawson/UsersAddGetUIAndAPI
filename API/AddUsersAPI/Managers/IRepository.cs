using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersApi.Managers
{
    public interface IRepository<T>
    {
        List<T> GetAll (List<string> names = null);

        T Get(string name);

        Enum Add(T model);
    }
}
