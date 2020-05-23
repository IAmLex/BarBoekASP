using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
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
        public void InsertPayment(PaymentDTO pay)
        {
            string sql = "INSERT INTO betaling (ID,Lid_Dienst_ID,geslaagd) VALUES(@ID,@ldID,@complete);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", pay.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("ldID", pay.MemberShiftID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("complete", pay.Succesful.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteMember(int id)
        {
            string sql = "DELETE FROM betaling where ID=@id;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            ExecuteQuery(sql, parameters);
        }

        public void UpdatePayment(PaymentDTO pay)
        {
            string sql = "UPDATE betaling SET ID=@id, Lid_Dienst_ID = @ldID, geslaagd = @complete";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", pay.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("ldID", pay.MemberShiftID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("complete", pay.Succesful.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }
    }
}
