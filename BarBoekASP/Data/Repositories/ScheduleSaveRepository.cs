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
        public List<ShiftDTO> Shifts { get; set;}// = new List<ShiftDTO>();

        public ScheduleSaveRepository(iScheduleSaveContext saveContext)
        {
            SaveContext = saveContext;
            this.Shifts = new List<ShiftDTO>();
        }




        public bool AddShift(ShiftDTO newShift)
        {
            bool add = true;
            foreach (ShiftDTO shift in this.Shifts)
            {
                // Als de shift niet in een bestaande shift valt wordt hij toegevoegd.

                // Een shift valt niet binnen een bestaande shift als ->
                // de start datum tijd na een de shift valt.
                // de eind datum tijd voor de shift valt.

                // if start of old shift is before end of new shift -> overlap
                if (shift.StartMoment.CompareTo(newShift.EndMoment) == 1)
                {
                    add = false;
                    break;
                }

                // if end of old shift is before start of new shift -> overlap
                if (shift.EndMoment.CompareTo(newShift.StartMoment) == 1)
                {
                    add = false;
                    break;
                }
            }

            if (add)
                this.Shifts.Add(newShift);

            return add;
        }

        public void PlanShifts(List<MemberDTO> members)
        {
            //int count = 0;

            // TODO: Add stuffs
            for (int i = 0; i < this.Shifts.Count; i++)
            {
                this.Shifts[i].Members = members[i];
            }

            //foreach(ShiftDTO shift in this.Shifts)
            //{
            //    foreach(MemberDTO member in members)
            //    {
            //       //if (AddShift(shift))
            //        //{
                        
            //            this.Shifts[count].Members = member;
            //            break;
            //        //}
            //    }
            //    count++;
            //}
           
        }
        


    }
}
