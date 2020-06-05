using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class ShiftMySQLContext : BaseMySQLContext, iShiftRetrieveContext, iShiftSaveContext
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
            string query = "Select * from dienst where id=@id";
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
            string sql = "INSERT INTO dienst (naam,startMoment,eindMoment,soort,maxLeden) VALUES(@name,@sMom,@eMom,@type,@maxMem);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("name", shift.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("sMom", shift.StartMoment.ToString("yyyy-MM-dd hh:mm:ss")));
            parameters.Add(new KeyValuePair<string, string>("eMom", shift.EndMoment.ToString("yyyy-MM-dd hh:mm:ss")));
            parameters.Add(new KeyValuePair<string, string>("type", ((int)shift.EventType).ToString()));
            parameters.Add(new KeyValuePair<string, string>("maxMem", shift.MaxMemberCount.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteShift(int id)
        {
            string sql = "delete from dienst where ID=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            ExecuteQuery(sql, parameters);
        }
        public void UpdateShift(ShiftDTO shift)
        {
            string sql = "update dienst set StartMoment=@startmoment EindMoment=@eindmoment Soort=@soort where ID=@id ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", shift.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("startmoment", shift.StartMoment.ToString()));
            parameters.Add(new KeyValuePair<string, string>("eindmoment", shift.EndMoment.ToString()));
            parameters.Add(new KeyValuePair<string, string>("soort", shift.EventType.ToString()));

            ExecuteQuery(sql, parameters);
        }
        

       
        public List<ShiftDTO> GetAllShiftsForClub(string month)
        {
            string query = "SELECT * FROM dienst WHERE MONTH(startMoment) = @month";



            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            //  parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            parameters.Add(new KeyValuePair<string, string>("month", month));


            DataSet results = ExecuteQuery(query, parameters);


            List<ShiftDTO> shifts = new List<ShiftDTO>();
            if (results != null)
            {
                for (int i = 0; i < results.Tables[0].Rows.Count; i++)
                {
                    ShiftDTO shift = DataSetParser.DataSetToShift(results, i);
                    if (shift.ID != 0)
                    { 
                        // Get members ID
                        query = "SELECT lidID FROM `lid-dienst-combo` WHERE dienstID=@id";
                        parameters.Clear();
                        parameters.Add(new KeyValuePair<string, string>("id", shift.ID.ToString()));

                    DataSet res = ExecuteQuery(query, parameters);
                        if (res.Tables[0].Rows.Count != 0)
                        {
                            int memberId = (int)res.Tables[0].Rows[0][0];

                            // Get actual member
                            query = "SELECT * FROM leden WHERE id = @id";
                            parameters.Clear();
                            parameters.Add(new KeyValuePair<string, string>("id", memberId.ToString()));

                            DataSet member = ExecuteQuery(query, parameters);

                            // Add member to shift
                            shift.Members = DataSetParser.DataSetToMember(member, 0);
                        }
                    }
                    shifts.Add(shift);
                }
            }



            return shifts;
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
