using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iMemberRetrieveContext
    {
        List<MemberDTO> GetAllMembers();
        MemberDTO FindMemberById(int id);
    }
}
