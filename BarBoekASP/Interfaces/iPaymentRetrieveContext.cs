using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Interfaces
{
    public interface iPaymentRetrieveContext
    {
        List<PaymentDTO> GetAllPayments();
        PaymentDTO FindPaymentById(int id);
    }
}
