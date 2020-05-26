using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public struct MemberDTO
    {
        public enum AccessLevel
        {
            Member,
            Admin,
            Super
        }
        public int ID { get; set; }
        public int BondNummer { get; set; } //ADD TO PARSER
        public string LastName { get; set; } //ADD TO PARSER
        public string Initials { get; set; } //ADD TO PARSER
        public string Insertion { get; set; } //ADD TO PARSER
        public string Name { get; set; }
        public string PhoneNumber { get; set; } //ADD TO PARSER
        public string Gender { get; set; } //ADD TO PARSER
        public int VerenigingsNumber { get; set; } //ADD TO PARSER
        public int PhoneWork { get; set; } //ADD TO PARSER
        public int PhoneMobile { get; set; } //ADD TO PARSER
        public AddressDTO Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<PaymentDTO> Payments { get; set; }
        //public List<UnavailableDTO> Unavailable {get;set;}
        public List<ShiftDTO> PrefferedShifts { get; set; }
        public AccessLevel Access {get; set;}
    }
}
