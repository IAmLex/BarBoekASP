using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.DAL.Contexts
{
    class ShiftMySQLContext : BaseMySQLContext, iShiftRetrieveContext
    {
        public ShiftMySQLContext(string connString) : base(connString)
        {

        }

        public List<ShiftDTO> GetAllShift()
        {
            string query = "Select * from dienst";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<ShiftDTO> shifts = new List<ShiftDTO>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    ShiftDTO s = DataSetParser.DataSetToShift(results, x);
                    shifts.Add(s);
                }
            }
            return shifts;
        }

        public ShiftDTO FindShiftById(int id)
        {
            string query = "Select * from shift where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            DataSet results = ExecuteQuery(query, parameters);
            ShiftDTO s = new ShiftDTO();

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                s = DataSetParser.DataSetToShift(results, 0);
            }
            return s;
        }
    }
}

