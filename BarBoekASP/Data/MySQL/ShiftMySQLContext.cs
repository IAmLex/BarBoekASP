using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class ShiftMySQLContext : BaseMySQLContext, iShiftRetrieveContext
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
        public void InsertShift(ShiftDTO shift)
        {
            string sql = "INSERT INTO dienst (ID,naam,startMoment,eindMoment,soort,maxLeden) VALUES(@ID,@name,@sMom,@eMom,@type,@maxMem);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", shift.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", shift.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("sMom", shift.StartMoment.ToString()));
            parameters.Add(new KeyValuePair<string, string>("eMom", shift.EndMoment.ToString()));
            parameters.Add(new KeyValuePair<string, string>("type", shift.EventType.ToString()));
            parameters.Add(new KeyValuePair<string, string>("maxMem", shift.MaxMemberCount.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteSchedule(int id)
        {
            string sql = "DELETE FROM [dienst] where ID=@id; DELETE FROM [schema-dienst-combo] WHERE dienstID=@id; UPDATE [lid-dienst-combo] SET dienstID=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            ExecuteQuery(sql, parameters);
        }

        public void UpdateSchedule(ShiftDTO shift)
        {
            string sql = "UPDATE [dienst] SET ID=@id, Naam=@name, startMoment=@sMom, eindMoment=@eMom, soort=@type, maxLeden=@maxMem; UPDATE [schema-dienst-combo] SET dienstID=@id; UPDATE [lid-dienst-combo] SET dienstID=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", shift.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", shift.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("sMom", shift.StartMoment.ToString()));
            parameters.Add(new KeyValuePair<string, string>("eMom", shift.EndMoment.ToString()));
            parameters.Add(new KeyValuePair<string, string>("type", shift.EventType.ToString()));
            parameters.Add(new KeyValuePair<string, string>("maxMem", shift.MaxMemberCount.ToString())); 
            ExecuteQuery(sql, parameters);
        }
    }
}
