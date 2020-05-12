using System;
using BarBoekASP.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BarBoekASP.Logic
{
    public class User
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

        public User() { }

        public User(UserDTO userDTO) 
        {
            this.Id = userDTO.Id;
            this.BondNumber = userDTO.BondNumber;
            this.LastName = userDTO.LastName;
            this.Initials = userDTO.Initials;
            this.Insertion = userDTO.Insertion;
            this.Name = userDTO.Name;
            this.Street = userDTO.Street;
            this.HouseNumber = userDTO.HouseNumber;
            this.Addition = userDTO.Addition;
            this.ZipCode = userDTO.ZipCode;
            this.Residence = userDTO.Residence;
            this.Country = userDTO.Country;
            this.PhoneNumber = userDTO.PhoneNumber;
            this.Gender = userDTO.Gender;
            this.BirthDate = userDTO.BirthDate;
            this.VerenigingsNummer = userDTO.VerenigingsNummer;
            this.Email = userDTO.Email;
            this.PhoneWork = userDTO.PhoneWork;
            this.PhoneMobile = userDTO.PhoneMobile;
        }

        public override string ToString()
        {
            if (this.Insertion != "")
                return $"{this.LastName}, {this.Initials} {this.Name} {this.Insertion}";
            return $"{this.LastName}, {this.Initials} {this.Name}";
        }
    }
}
