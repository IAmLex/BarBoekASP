using BarBoekASP.Logic.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iUserRetrieveContext
    {
        List<UserTest> GetAllUsers();
        bool Inloggen(UserTest user);
        bool CheckValidate(UserTest user);
    }
}
