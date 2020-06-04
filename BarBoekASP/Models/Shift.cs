using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public enum Soort
    {
        Eenmalig = 0,
        Wekelijks = 1,
        Maandelijks = 2,
        Evenement = 3
    }

    public class ShiftDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartMoment { get; set; }
        public DateTime EndMoment { get; set; }
        public Soort EventType { get; set; }
        public int MaxMemberCount { get; set; }
        public MemberDTO Members { get; set; }
        // public /*List<*/MemberDTO/*>*/ Members { get; set; }
    }
}