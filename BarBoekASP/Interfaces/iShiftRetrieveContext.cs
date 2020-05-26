using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iShiftRetrieveContext
    {
        List<ShiftDTO> GetAllShift();
        ShiftDTO FindShiftById(int id);
    }
}
