using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iShiftSaveContext
    {
        void InsertShift(ShiftDTO shift);
        void DeleteShift(int id);
        void UpdateShift(ShiftDTO shift);

       
        //Save(Shift s);
        // ¯\_(ツ)_/¯ Do something 
        // Save(shift:ShiftDTO)
        // AKA NOT YET IMPLEMENTED
    }
}
