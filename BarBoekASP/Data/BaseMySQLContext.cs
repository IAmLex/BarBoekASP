using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public abstract class BaseMySQLContext
    {
        private string connectionString;
        public BaseMySQLContext(string connString)
        {
            connectionString = connString;
        }

        public DataSet ExecuteQuery(string query, List<KeyValuePair<string, string>> parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlDataAdapter da = new MySqlDataAdapter();
                MySqlCommand cmd = conn.CreateCommand();
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    MySqlParameter para = new MySqlParameter();
                    para.ParameterName = "@" + kvp.Key;
                    para.Value = kvp.Value;
                    cmd.Parameters.Add(para);
                }
                cmd.CommandText = query;
                da.SelectCommand = cmd;

                conn.Open();
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return null;
            }
            return ds;
        }
    }
}
