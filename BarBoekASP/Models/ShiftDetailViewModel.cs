using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public class ShiftDetailViewModel
    {
       public List<ShiftViewModel> Shifts { get; set; } = new List<ShiftViewModel>();
    }
}
