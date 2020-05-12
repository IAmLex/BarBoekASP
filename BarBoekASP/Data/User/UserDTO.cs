using System;
using BarBoekASP.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int BondNumber { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Insertion { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Addition { get; set; }
        public string ZipCode { get; set; }
        public string Residence { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int VerenigingsNummer { get; set; }
        public string Email { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneMobile { get; set; }

        public UserDTO() { }

        public UserDTO(User user) 
        {
            this.Id = user.Id;
            this.BondNumber = user.BondNumber;
            this.LastName = user.LastName;
            this.Initials = user.Initials;
            this.Insertion = user.Insertion;
            this.Name = user.Name;
            this.Street = user.Street;
            this.HouseNumber = user.HouseNumber;
            this.Addition = user.Addition;
            this.ZipCode = user.ZipCode;
            this.Residence = user.Residence;
            this.Country = user.Country;
            this.PhoneNumber = user.PhoneNumber;
            this.Gender = user.Gender;
            this.BirthDate = user.BirthDate;
            this.VerenigingsNummer = user.VerenigingsNummer;
            this.Email = user.Email;
            this.PhoneWork = user.PhoneWork;
            this.PhoneMobile = user.PhoneMobile;
        }
    }
}
