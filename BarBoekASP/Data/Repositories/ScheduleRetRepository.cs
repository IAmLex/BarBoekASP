using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class ScheduleRetRepository
    {
        iScheduleRetrieveContext ContextRet;
        public ScheduleRetRepository(iScheduleRetrieveContext contextRet)
        {
            ContextRet = contextRet;
        }
        public List<ScheduleDTO> GetAll()
        {
            return ContextRet.GetAllSchedules();
        }
        public ScheduleDTO GetByID(int id)
        {
            return ContextRet.FindScheduleById(id);
        }
    }
}
