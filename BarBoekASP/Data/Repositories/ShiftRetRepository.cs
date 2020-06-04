using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class ShiftRetRepository
    {
        iShiftRetrieveContext ContextRet;
        public ShiftRetRepository(iShiftRetrieveContext contextRet)
        {
            ContextRet = contextRet;
        }
        public List<ShiftDTO> GetAll()
        {
            return ContextRet.GetAllShift();
        }
        public ShiftDTO GetByID(int id)
        {
            return ContextRet.FindShiftById(id);
        }
        public List<ShiftDTO> GetAllShiftsForClub(string month)
        {
           return ContextRet.GetAllShiftsForClub(month);
        }
    }
}
