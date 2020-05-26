using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class ClubMySQLContext : BaseMySQLContext, iClubRetrieveContext
    {
        public ClubMySQLContext(string connString) : base(connString)
        {

        }

        public List<ClubDTO> GetAllClubs()
        {
            string query = "Select * from vereniging ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<ClubDTO> clubs = new List<ClubDTO>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    ClubDTO c = DataSetParser.DataSetToClub(results, x);
                    clubs.Add(c);

                    //clubs.Add(c);
                }
            }
            return clubs;
        }

        public ClubDTO FindClubById(int id)
        {
            string query = "Select * from vereniging where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            DataSet results = ExecuteQuery(query, parameters);
            ClubDTO c = new ClubDTO();

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                c = DataSetParser.DataSetToClub(results, 0);
            }
            return c;
        }

        public void InsertClub(ClubDTO club)
        {
            string sql = "INSERT INTO vereniging (ID,naam,AdresID,,email,wachtwoord,schemaID) VALUES(@ID,@name,@aID,@email,@passw,@sID);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", club.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", club.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("aID", club.Address.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("email", club.Email.ToString()));
            parameters.Add(new KeyValuePair<string, string>("passw", club.Password.ToString()));
            parameters.Add(new KeyValuePair<string, string>("sID", club.Schedule.ID.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteClub(int id)
        {
            string sql = "DELETE FROM vereniging where ID=@id;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            ExecuteQuery(sql, parameters);
        }

        public void UpdateClub(ClubDTO club)
        {
            string sql = "UPDATE vereniging SET ID=@id, Naam=@name, adresID =@aID, email=@email, wachtwoord=@passw, schemaID=@sID; UPDATE [lid-dienst-combo] SET ID=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", club.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", club.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("aID", club.Address.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("email", club.Email.ToString()));
            parameters.Add(new KeyValuePair<string, string>("passw", club.Password.ToString()));
            parameters.Add(new KeyValuePair<string, string>("sID", club.Schedule.ID.ToString()));
            ExecuteQuery(sql, parameters);
        }
    }
}
