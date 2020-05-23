using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class ClubRetRepository
    {
        iClubRetrieveContext ContextRet;
        public ClubRetRepository(iClubRetrieveContext contextRet)
        {
            ContextRet = contextRet;
        }
        public List<ClubDTO> GetAll()
        {
            return ContextRet.GetAllClubs();
        }
        public ClubDTO GetByID(int id)
        {
            return ContextRet.FindClubById(id);
        }
    }
}
