using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Models;

namespace BarBoekASP.Interfaces
{
    public interface iMemberSaveContext
    {
        void InsertMember(MemberDTO mem);
        void DeleteMember(int id);
        void UpdateMember(MemberDTO mem);
        void RemoveAllMembers(int clubId);
        //Save(Member m);
        // ¯\_(ツ)_/¯ Do something 
        // Save(member:MemberDTO)
        // AKA NOT YET IMPLEMENTED
    }
}
