using Barboek.ClassLib.DAL.Contexts;
using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.BLL
{
    class MemberRetRepository
    {
        iMemberRetrieveContext ContextRet;
        public MemberRetRepository(iMemberRetrieveContext contextRet)
        {
            ContextRet = contextRet;
        }
        public List<MemberDTO> GetAll()
        {
            return ContextRet.GetAllMembers();
        }
        public MemberDTO GetByID(int id)
        {
            return ContextRet.FindMemberById(id);
        }
    }
}
