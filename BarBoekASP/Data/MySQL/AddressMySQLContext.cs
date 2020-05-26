using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class AddressMySQLContext : BaseMySQLContext, iAddressRetrieveContext
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

        public void InsertAddress(AddressDTO addr)
        {
            string sql = "INSERT INTO adres (ID,zipcode,number,addition) VALUES(@ID,@zip,@num,@add);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", addr.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("zip", addr.ZipCode.ToString()));
            parameters.Add(new KeyValuePair<string, string>("num", addr.Number.ToString()));
            parameters.Add(new KeyValuePair<string, string>("add", addr.Addition.ToString()));

            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteAddress(int id)
        {
            string sql = "DELETE FROM adres where ID=@id;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            ExecuteQuery(sql, parameters);
        }

        public void UpdateAddress(AddressDTO addr)
        {
            string sql = "UPDATE adres SET ID=@id, zipcode=@zip, number=@num, addition=@add";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", addr.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("zip", addr.ZipCode.ToString()));
            parameters.Add(new KeyValuePair<string, string>("num", addr.Number.ToString()));
            parameters.Add(new KeyValuePair<string, string>("add", addr.Addition.ToString()));
            ExecuteQuery(sql, parameters);
        }
    }
}
