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

        public void DeleteShift(int id)
        {
            context.DeleteShift(id);
        }

        public void SaveShift(ShiftDTO shift)
        {
            context.InsertShift(shift);
        }

        public void UpdateShift(ShiftDTO shift)
        {
            context.UpdateShift(shift);
        }
    }
}
