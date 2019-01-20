using Cornhacks2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cornhacks2019.Interfaces.AccessorInterfaces
{
    public interface IUserAccessor
    {
        List<User> Select();

        User Insert(User user);

        User Delete(User user); 
    }
}
