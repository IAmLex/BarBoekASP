using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BarBoekASP.Models
{
    public class UserListViewModel
    {
        public List<UserModel> Users { get; set; }
    }
}