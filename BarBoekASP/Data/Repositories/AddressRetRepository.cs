using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class AddressRetRepository
    {
        iAddressRetrieveContext ContextRet;
        public AddressRetRepository(iAddressRetrieveContext contextRet)
        {
            ContextRet = contextRet;
        }
        public List<AddressDTO> GetAll()
        {
            return ContextRet.GetAllAddresses();
        }
        public AddressDTO GetByID(int id)
        {
            return ContextRet.FindAddressById(id);
        }
    }
}
