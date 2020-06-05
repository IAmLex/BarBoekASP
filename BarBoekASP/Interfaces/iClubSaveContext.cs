using BarBoekASP.Logic.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iClubSaveContext
    {
        void InsertClub(ClubModel club);
        void InsertAddress(ClubModel club);
    }
}
