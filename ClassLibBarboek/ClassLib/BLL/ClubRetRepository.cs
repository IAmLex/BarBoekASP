using Barboek.ClassLib.DAL.Contexts;
using Barboek.ClassLib.DAL.Model;
using ClassLibBarboek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.BLL
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
