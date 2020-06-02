using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class ScheduleSaveRepository
    {
        iScheduleSaveContext SaveContext;
        public List<ShiftDTO> Shifts = new List<ShiftDTO>();

        public ScheduleSaveRepository(iScheduleSaveContext saveContext)
        {
            SaveContext = saveContext;
        }

        /*
        public void PlanShifts(List<MemberDTO> members)
        {
            int count = 0;
            foreach(ShiftDTO shift in this.Shifts)
            {
                foreach(MemberDTO member in members)
                {
                    if (member.AddShift(shift))
                    {
                        this.Shifts[count].Members = member;
                        break;
                    }
                }
            }
            count++;
        }
        */


    }
}
