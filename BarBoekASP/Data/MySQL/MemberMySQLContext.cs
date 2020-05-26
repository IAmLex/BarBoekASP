using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
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

        public void InsertMember(MemberDTO mem)
        {
            string sql = "INSERT INTO leden (ID,naam,adresID,geboortedatum,email,wachtwoord,rechten) VALUES(@ID,@name,@aID,@bDate,@email,@passw,@rank);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", mem.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", mem.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("aID", mem.Address.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("bDate", mem.BirthDate.ToString()));
            parameters.Add(new KeyValuePair<string, string>("email", mem.Email.ToString()));
            parameters.Add(new KeyValuePair<string, string>("passw", mem.Password.ToString()));
            parameters.Add(new KeyValuePair<string, string>("rank", mem.Access.ToString()));
            DataSet result = ExecuteQuery(sql, parameters);
        }

        public void DeleteMember(int id)
        {
            string sql = "DELETE FROM [leden] where ID=@id; DELETE FROM [lid-dienst-combo] WHERE lidID=@id;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("id", id.ToString()));
            ExecuteQuery(sql, parameters);
        }

        public void UpdateMember(MemberDTO mem)
        {
            string sql = "UPDATE [leden] SET ID=@id, Naam=@name, adresID =@aID, geboortedatum=@bDate, email=@email, wachtwoord=@passw, rechten=@rank; UPDATE [lid-dienst-combo] SET ID=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("ID", mem.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("name", mem.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("aID", mem.Address.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("bDate", mem.BirthDate.ToString()));
            parameters.Add(new KeyValuePair<string, string>("email", mem.Email.ToString()));
            parameters.Add(new KeyValuePair<string, string>("passw", mem.Password.ToString()));
            parameters.Add(new KeyValuePair<string, string>("rank", mem.Access.ToString()));
            ExecuteQuery(sql, parameters);
        }
    }
}
