using Barboek.ClassLib.DAL.Contexts;
using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.BLL
{
    class ScheduleRetRepository
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
