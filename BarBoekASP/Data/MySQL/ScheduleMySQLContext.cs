using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class ScheduleMySQLContext : BaseMySQLContext, iScheduleRetrieveContext, iScheduleSaveContext
    {
        public ScheduleMySQLContext(string connString) : base(connString)
        {

        }

        public List<ScheduleDTO> GetAllSchedules()
        {
            string query = "Select * from `schema` ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<ScheduleDTO> schedules = new List<ScheduleDTO>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    ScheduleDTO s = DataSetParser.DataSetToSchedule(results, x);
                    schedules.Add(s);
                }
            }
            return schedules;
        }

        public ScheduleDTO FindScheduleById(int id)
        {
            string query = "Select * from `schema` where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            DataSet results = ExecuteQuery(query, parameters);
            ScheduleDTO s = new ScheduleDTO();

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                s = DataSetParser.DataSetToSchedule(results, 0);
            }
            return s;
        }

        public void InsertSchedule(ScheduleDTO sched)
        {
            string sql = "INSERT INTO schema (ID,Naam,verenigingID) VALUES(@ID,@name,@vID);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", sched.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", sched.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("vID", sched.VerenigingID.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteSchedule(int id)
        {
            string sql = "DELETE FROM [schema] where ID=@id; DELETE FROM [schema-dienst-combo] WHERE schemaID=@id;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            ExecuteQuery(sql, parameters);
        }

        public void UpdateSchedule(ScheduleDTO sched)
        {
            string sql = "UPDATE [schema] SET ID=@id, Naam=@name, verenigingID=@vID; UPDATE [schema-dienst-combo] SET ID=@id;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", sched.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", sched.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("vID", sched.VerenigingID.ToString()));
            ExecuteQuery(sql, parameters);
        }
        public void InsertLidShift(ShiftDTO shift)
        {
            string sql = "INSERT INTO `lid-dienst-combo` (lidID, dienstID) VALUES(@lidid, @dienstid);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("lidid", shift.Members.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("dienstid", shift.ID.ToString()));


            ExecuteQuery(sql, parameters);
        }
    }
   
}
