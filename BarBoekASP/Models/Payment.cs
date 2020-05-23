using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public struct PaymentDTO
    {
        public int ID { get; set; }
        public int MemberShiftID { get; set; }
        public bool Succesful { get; set; }
    }
}
