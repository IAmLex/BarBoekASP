using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public struct ScheduleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int VerenigingID { get; set; }
        public List<ShiftDTO> Shifts { get; set; }
    }
}
