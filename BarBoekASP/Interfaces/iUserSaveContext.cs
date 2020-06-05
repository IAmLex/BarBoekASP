using BarBoekASP.Logic.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iUserSaveContext
    {
        void InsertUser(UserTest user);
    }
}
