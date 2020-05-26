using Barboek.ClassLib.DAL.Model;
using ClassLibBarboek;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.DAL.Contexts
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

    }
}
