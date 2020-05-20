using System;
using Microsoft.AspNetCore.Http;

namespace BarBoekASP.Models
{
    public class ImportUsersModel
    {
        public IFormFile File { get; set; }
        public bool RemoveCurrent { get; set; }
    }
}