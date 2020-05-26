using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iScheduleRetrieveContext
    {
        List<ScheduleDTO> GetAllSchedules();
        ScheduleDTO FindScheduleById(int id);
    }
}
