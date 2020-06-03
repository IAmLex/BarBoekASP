using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class MemberSaveRepository
    {
        iMemberSaveContext SaveContext;

        public MemberSaveRepository(iMemberSaveContext saveContext)
        {
            SaveContext = saveContext;
           
        }

        public void InsertMember(MemberDTO member) 
        {
            SaveContext.InsertMember(member);
        }

        public void RemoveAllMembers(int clubId) 
        {
            SaveContext.RemoveAllMembers(clubId);
        }




       
    }
}
