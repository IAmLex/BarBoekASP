using Barboek.ClassLib.DAL.Contexts;
using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.BLL
{
    class AddressRetRepository
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
