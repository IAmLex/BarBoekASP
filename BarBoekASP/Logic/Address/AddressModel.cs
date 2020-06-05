using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Logic.Address
{
    public class AddressModel
    {
        public int AID { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Addition { get; set; }
        public string ZipCode { get; set; }
        public string Residence { get; set; }
        public string Country { get; set; }
    }
}
