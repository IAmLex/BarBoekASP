using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class ShiftSaveRepository
    {
        iShiftSaveContext context;

        public ShiftSaveRepository(iShiftSaveContext contextsave)
        {
            context = contextsave;
        }

        public void SaveShift(ShiftDTO shift)
        {
            context.InsertShift(shift);
        }

        public void SaveLidShift(ShiftDTO shift)
        {
            context.InsertLidShift(shift);
        }
    }
}
