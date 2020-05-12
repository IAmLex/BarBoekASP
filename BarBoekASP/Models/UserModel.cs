using System;
using Microsoft.AspNetCore.Http;

namespace BarBoekASP.Models
{
    public class UserModel
    {
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Insertion { get; set; }
        public string Name { get; set; }
    }
}