using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public struct ClubDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public string ClubNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
