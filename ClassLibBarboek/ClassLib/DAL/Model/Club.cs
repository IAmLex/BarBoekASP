using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibBarboek;

namespace Barboek.ClassLib.DAL.Model
{
    public struct ClubDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set;}
        public string ClubNumber { get; set; }
        public string Email { get; set; }
        //public Schedule Schedule { get; }

    }
}
