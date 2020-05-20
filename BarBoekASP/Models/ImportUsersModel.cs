using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

namespace BarBoekASP.Models
{
    public class ImportUsersModel
    {
        [Required(ErrorMessage = "Selecteer een bestand.")]
        public IFormFile File { get; set; }

        public bool RemoveCurrent { get; set; }
    }
}