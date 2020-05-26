using Barboek.ClassLib.DAL.Contexts;
using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.BLL
{
    class PaymentRetRepository
    {
        iPaymentRetrieveContext ContextRet;
        public PaymentRetRepository(iPaymentRetrieveContext contextRet)
        {
            ContextRet = contextRet;
        }
        public List<PaymentDTO> GetAll()
        {
            return ContextRet.GetAllPayments();
        }
        public PaymentDTO GetByID(int id)
        {
            return ContextRet.FindPaymentById(id);
        }
    }
}
