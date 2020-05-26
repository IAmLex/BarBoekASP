using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    class MemberMySQLContext : BaseMySQLContext, iMemberRetrieveContext, iMemberSaveContext
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
            string sql = @"INSERT INTO leden (ID,   naam,  adresID,  geboortedatum,  email,  wachtwoord,  toegang,  bondsnummer,  achternaam,  voorletters,  tussenvoegsel,  telefoon,  geslacht,  telefoonWerk,  telefoonMobiel,  verenigingsID) 
                                       VALUES(@ID, @naam, @adresID, @geboortedatum, @email, @wachtwoord, @toegang, @bondsnummer, @achternaam, @voorletters, @tussenvoegsel, @telefoon, @geslacht, @telefoonWerk, @telefoonMobiel, @verenigingsID);";
            
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            
            parameters.Add(new KeyValuePair<string, string>("ID", mem.ID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("naam", mem.Name.ToString()));

            // TODO: Make address
            parameters.Add(new KeyValuePair<string, string>("adresID", "1"));
            parameters.Add(new KeyValuePair<string, string>("geboortedatum", mem.BirthDate.ToString("yyyy-MM-dd HH:mm:ss")));
            parameters.Add(new KeyValuePair<string, string>("email", mem.Email.ToString()));

            // FIXME: beter validatie
            try {
                parameters.Add(new KeyValuePair<string, string>("wachtwoord", mem.Password.ToString()));
            } 
            catch (Exception) 
            {
                parameters.Add(new KeyValuePair<string, string>("wachtwoord", ""));
            }
            
            parameters.Add(new KeyValuePair<string, string>("toegang", ((int)mem.Access).ToString()));
            parameters.Add(new KeyValuePair<string, string>("bondsnummer", mem.BondNummer.ToString()));
            parameters.Add(new KeyValuePair<string, string>("achternaam", mem.LastName.ToString()));
            parameters.Add(new KeyValuePair<string, string>("voorletters", mem.Initials.ToString()));
            parameters.Add(new KeyValuePair<string, string>("tussenvoegsel", mem.Insertion.ToString()));
            parameters.Add(new KeyValuePair<string, string>("telefoon", mem.PhoneNumber.ToString()));
            parameters.Add(new KeyValuePair<string, string>("geslacht", mem.Gender.ToString()));
            parameters.Add(new KeyValuePair<string, string>("telefoonWerk", mem.PhoneWork.ToString()));
            parameters.Add(new KeyValuePair<string, string>("telefoonMobiel", mem.PhoneMobile.ToString()));
            parameters.Add(new KeyValuePair<string, string>("verenigingsID", mem.clubID.ToString()));

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
            string sql = "UPDATE [leden] SET ID=@id, Naam=@name, adresID =@aID, geboortedatum=@bDate, email=@email, wachtwoord=@passw, toegang=@rank; UPDATE [lid-dienst-combo] SET ID=@id";
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

        public void RemoveAllMembers(int clubId) 
        {
            string query = "DELETE FROM leden WHERE verenigingsID = @clubId";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("clubID", clubId.ToString()));

            ExecuteQuery(query, parameters);

            // TODO: Remove members from these columns aswell
            // certificaat-lid-combo
            // lid-dienst-combo
            // nietbeschikbaar
            // betaling
        }
    }
}
