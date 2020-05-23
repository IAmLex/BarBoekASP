using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class MemberRetRepository
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
