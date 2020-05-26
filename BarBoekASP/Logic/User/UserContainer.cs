using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Data;

namespace BarBoekASP.Logic
{
    public class UserContainer
    {
        public static List<User> GetAllUsers(int limit = -1)
        {
            List<User> users = new List<User>();
            foreach (UserDTO userDto in UserDAL.GetAll(limit)) {
                users.Add(new User(userDto));
            }

            return users;
        }

        public static bool Save(User user)
        {
            return UserDAL.Save(new UserDTO(user));
        }

        public static bool RemoveAll()
        {
            return UserDAL.RemoveAll();
        }
    }
}
