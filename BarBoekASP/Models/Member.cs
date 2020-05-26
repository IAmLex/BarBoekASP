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
        public int BondNummer { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Insertion { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int ClubNumber { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneMobile { get; set; }
        public AddressDTO Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<PaymentDTO> Payments { get; set; }
        //public List<UnavailableDTO> Unavailable {get;set;}
        public List<ShiftDTO> PrefferedShifts { get; set; }
        public AccessLevel Access { get; set; }
        public int clubID { get; set; }
    }
}
