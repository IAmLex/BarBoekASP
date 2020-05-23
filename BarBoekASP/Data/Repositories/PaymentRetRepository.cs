using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.Repositories
{
    public class PaymentRetRepository
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
