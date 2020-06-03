using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;

using System.Collections.Generic;

namespace BarBoekASP.Models
{
    public class ImportUsersModel
    {
        [Required(ErrorMessage = "Selecteer een bestand.")]
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public bool RemoveCurrent { get; set; }
        public List<string> Columns { get; set; } = new List<string>();
        public List<string> DatabaseColumns { get; } = new List<string>() {
            "Bondsnr",
            "Achternaam",
            "Voorletters",
            "Tussenvoegsel",
            "Roepnaam",
            "Straat",
            "Huisnr",
            "Toevoeging",
            "Postcode",
            "Woonplaats",
            "Land",
            "Telefoon",
            "ManVrouw",
            "GeboorteDatum",
            "VerenigingsLidnummer",
            "Email",
            "TelefoonWerk",
            "TelefoonMobiel"
        };
        public string Bondsnr { get; set; }
        public string Achternaam { get; set; }
        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Roepnaam { get; set; }
        public string Straat { get; set; }
        public string Huisnr { get; set; }
        public string Toevoeging { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public string Land { get; set; }
        public string Telefoon { get; set; }
        public string ManVrouw { get; set; }
        public string GeboorteDatum { get; set; }
        public string VerenigingsLidnummer { get; set; }
        public string Email { get; set; }
        public string TelefoonWerk { get; set; }
        public string TelefoonMobiel { get; set; }
    }
}