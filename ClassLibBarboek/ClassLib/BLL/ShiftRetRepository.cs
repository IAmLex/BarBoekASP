using Barboek.ClassLib.DAL.Contexts;
using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.BLL
{
    class ShiftRetRepository
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
    }
}
