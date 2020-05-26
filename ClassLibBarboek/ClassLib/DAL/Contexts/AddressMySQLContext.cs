using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.DAL.Contexts
{
    class AddressMySQLContext : BaseMySQLContext, iAddressRetrieveContext
    {
        public AddressMySQLContext(string connString) : base(connString)
        {

        }

        public List<AddressDTO> GetAllAddresses()
        {
            string query = "Select * from adres ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<AddressDTO> addresses = new List<AddressDTO>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    AddressDTO a = DataSetParser.DataSetToAddress(results, x);
                    addresses.Add(a);
                }
            }
            return addresses;
        }

        public AddressDTO FindAddressById(int id)
        {
            string query = "Select * from adres where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            DataSet results = ExecuteQuery(query, parameters);
            AddressDTO a = new AddressDTO();

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                a = DataSetParser.DataSetToAddress(results, 0);
            }
            return a;
        }
    }
}
