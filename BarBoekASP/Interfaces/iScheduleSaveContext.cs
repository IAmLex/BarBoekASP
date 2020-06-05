using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iScheduleSaveContext
    {
        void InsertSchedule(ScheduleDTO sched);
        //Save(Schedule s);
        // ¯\_(ツ)_/¯ Do something 
        // Save(schedule:ScheduleDTO)
        // AKA NOT YET IMPLEMENTED
    }
}
