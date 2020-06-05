using BarBoekASP.Interfaces;
using BarBoekASP.Logic.Club;
using BarBoekASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data.MySQL
{
    public class ClubMySQLContext : BaseMySQLContext, iClubRetrieveContext,iClubSaveContext
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
        public void InsertClub(ClubModel club)
        {
            string sql = "SET FOREIGN_KEY_CHECKS=0;INSERT INTO club (name,adresId,email,password,clubnumber,type,comment) VALUES(@name,@adresId,@email,@password,@clubnumber,@type,@comment);SET FOREIGN_KEY_CHECKS=0;";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("name", club.Name.ToString()));
            parameters.Add(new KeyValuePair<string, string>("adresId", club.AID.ToString()));
            parameters.Add(new KeyValuePair<string, string>("email", club.Email.ToString()));
            parameters.Add(new KeyValuePair<string, string>("password", club.Postcode.ToString()));
            parameters.Add(new KeyValuePair<string, string>("clubnumber", club.ClubNumber));
            parameters.Add(new KeyValuePair<string, string>("type", club.Test));
            parameters.Add(new KeyValuePair<string, string>("comment", club.Comment));

            DataSet result = ExecuteQuery(sql, parameters);

        }
        public void InsertAddress(ClubModel club)
        {
            string sql = " INSERT INTO adres(zipcode, number, addition) VALUES(@zipcode, @number, @addition);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("zipcode", club.ZipCode));
            parameters.Add(new KeyValuePair<string, string>("number", club.Number.ToString()));
            parameters.Add(new KeyValuePair<string, string>("addition", club.Addition));

            DataSet result = ExecuteQuery(sql, parameters);
        }
        public bool Inloggen(ClubModel club)
        {
            bool check = false;
            string mysql = "Select * from club where clubnumber=@clubnumber and password=@password";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("clubnumber", club.ClubNumber));
            parameters.Add(new KeyValuePair<string, string>("password", club.Postcode));
            DataSet results = ExecuteQuery(mysql, parameters);
            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                check = true;
            }
            return check;
        }
        public bool CheckValidate(ClubModel club)
        {
            bool check = false;
            string mysql = "Select * from club where name=@name";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("name", club.Name));
            DataSet results = ExecuteQuery(mysql, parameters);
            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                check = true;
            }
            return check;
        }
        public List<ClubModel> GetAll()
        {
            string query = "Select * from club ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<ClubModel> clubs = new List<ClubModel>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    ClubModel c = DataSetParser.DataSetToclub(results, x);
                    clubs.Add(c);
                }
            }
            return clubs;
        }
    }
}
