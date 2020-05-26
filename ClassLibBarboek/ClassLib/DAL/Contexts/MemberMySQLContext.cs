using Barboek.ClassLib.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barboek.ClassLib.DAL.Contexts
{
    class MemberMySQLContext : BaseMySQLContext, iMemberRetrieveContext
    {
        public MemberMySQLContext(string connString) : base(connString)
        {

        }

        public List<MemberDTO> GetAllMembers()
        {
            string query = "Select * from leden";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<MemberDTO> members = new List<MemberDTO>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    MemberDTO m = DataSetParser.DataSetToMember(results, x);
                    members.Add(m);
                }
            }
            return members;
        }

        public MemberDTO FindMemberById(int id)
        {
            string query = "Select * from leden where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));

            DataSet results = ExecuteQuery(query, parameters);
            MemberDTO m = new MemberDTO();

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                m = DataSetParser.DataSetToMember(results, 0);
            }
            return m;
        }
    }
}
