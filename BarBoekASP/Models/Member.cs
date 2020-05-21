﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public struct MemberDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public List<PaymentDTO> Payments { get; set; }
        //public List<UnavailableDTO> Unavailable {get;set;}
        public List<ShiftDTO> PrefferedShifts { get; set; }
        //public AccessLevel Access {get; set;}
    }
}
