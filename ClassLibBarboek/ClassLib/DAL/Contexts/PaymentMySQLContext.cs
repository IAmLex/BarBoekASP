using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.DAL.Contexts
{
    class PaymentMySQLContext : BaseMySQLContext, iPaymentRetrieveContext
    {
        public PaymentMySQLContext(string connString) : base(connString)
        {

        }

        public List<PaymentDTO> GetAllPayments()
        {
            string query = "Select * from betaling";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<PaymentDTO> payments = new List<PaymentDTO>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    PaymentDTO p = DataSetParser.DataSetToPayment(results, x);
                    payments.Add(p);
                }
            }
            return payments;
        }

        public PaymentDTO FindPaymentById(int id)
        {
            string query = "Select * from betaling where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            DataSet results = ExecuteQuery(query, parameters);
            PaymentDTO p = new PaymentDTO();

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                p = DataSetParser.DataSetToPayment(results, 0);
            }
            return p;
        }
    }
}
