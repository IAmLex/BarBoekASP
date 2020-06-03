using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Models
{
    public class ShiftViewModel
    {
       public List<ShiftDetailViewModel> Shifts { get; set; } = new List<ShiftDetailViewModel>();
    }
}
