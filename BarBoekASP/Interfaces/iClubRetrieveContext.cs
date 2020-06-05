using BarBoekASP.Logic.Club;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iClubRetrieveContext
    {
        ClubDTO FindClubById(int id);
        List<ClubDTO> GetAllClubs();
        bool Inloggen(ClubModel club);
        bool CheckValidate(ClubModel club);
        List<ClubModel> GetAll();
        void FindAddressBy(ClubModel club);
    }
}
