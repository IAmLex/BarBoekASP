using BarBoekASP.Logic.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BarBoekASP.Logic.Club
{
    public class ClubModel: AddressModel
    {
        public enum Typetype
        {
            Demo = 0,
            Jaarabonnemnet = 1
        }

        public int ID { get; set; }
        [Display(Name = "Naam vereniging*")]
        [Required(ErrorMessage = "U moet de naam van de vereniging invullen.")]
        public string Name { get; set; }
        [Display(Name = "Clubbondnummer*")]
        [Required(ErrorMessage = "U moet de bondnummer invullen.")]
        public string ClubNumber { get; set; }
        [Display(Name = "E-mail*")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "U moet de email invullen.")]
        public string Email { get; set; }
        [Display(Name = "Postcode*")]
        [Required(ErrorMessage = "U moet de postcode invullen.")]
        public string Postcode { get; set; }
        [Required(ErrorMessage = "Selecteer een type.")]
        public Typetype Type { get; set; }
        public string Comment { get; set; }
        public string Test { get; set; }
    }
}
