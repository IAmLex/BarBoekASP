using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class ScheduleMySQLContext : BaseMySQLContext, iScheduleRetrieveContext
    {
        public ScheduleMySQLContext(string connString) : base(connString)
        {

        }

        public List<ScheduleDTO> GetAllSchedules()
        {
            string query = "Select * from schema";
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
            string query = "Select * from schema where id=@id";
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
    }
}
