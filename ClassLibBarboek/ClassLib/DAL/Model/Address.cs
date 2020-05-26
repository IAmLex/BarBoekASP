using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.DAL.Model
{
    public struct AddressDTO
    {
        public int ID { get; set; }
        public string ZipCode { get; set; }
        public int Number { get; set; }
        public string Addition { get; set; }
    }
}
