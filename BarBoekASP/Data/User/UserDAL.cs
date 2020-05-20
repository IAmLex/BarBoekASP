using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarBoekASP.Data
{
    public class UserDAL
    {
        public static bool Save(UserDTO user)
        {
            Database database = new Database();

            if (!database.OpenConnection())
                return false;

            database.command.CommandText = @"INSERT INTO users(bond_number, lastname, initials, insertion, firstname, street, housenumber, 
                                             addition, zipcode, residence, country, phonenumber, gender, email, workphone, mobilephone) 
                                             VALUES (@BondNumber, @LastName, @Initials, @Insertion, @Name, @Street, @HouseNumber, 
                                             @Addition, @ZipCode, @Residence, @Country, @PhoneNumber, @Gender, @Email, @PhoneWork, @PhoneMobile)";

            database.command.Parameters.AddWithValue("@BondNumber", user.BondNumber);
            database.command.Parameters.AddWithValue("@LastName", user.LastName);
            database.command.Parameters.AddWithValue("@Initials", user.Initials);
            database.command.Parameters.AddWithValue("@Insertion", user.Insertion);
            database.command.Parameters.AddWithValue("@Name", user.Name);
            database.command.Parameters.AddWithValue("@Street", user.Street);
            database.command.Parameters.AddWithValue("@HouseNumber", user.HouseNumber);
            database.command.Parameters.AddWithValue("@Addition", user.Addition);
            database.command.Parameters.AddWithValue("@ZipCode", user.ZipCode);
            database.command.Parameters.AddWithValue("@Residence", user.Residence);
            database.command.Parameters.AddWithValue("@Country", user.Country);
            database.command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            database.command.Parameters.AddWithValue("@Gender", user.Gender);
            database.command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
            database.command.Parameters.AddWithValue("@VerenigingsNummer", user.VerenigingsNummer);
            database.command.Parameters.AddWithValue("@Email", user.Email);
            database.command.Parameters.AddWithValue("@PhoneWork", user.PhoneWork);
            database.command.Parameters.AddWithValue("@PhoneMobile", user.PhoneMobile);

            int affectedRows = database.command.ExecuteNonQuery();

            database.CloseConnection();

            if (affectedRows > 0)
                return true;
            return false;
        }

        public static List<UserDTO> GetAll(int limit)
        {
            Database database = new Database();

            if (!database.OpenConnection())
                // FIXME: throw exception;
                return null;
            
            if (limit == -1) 
            {
                database.command.CommandText = "SELECT * FROM users WHERE deleted_at IS NULL";
            } 
            else 
            {
                database.command.CommandText = "SELECT * FROM users WHERE deleted_at IS NULL LIMIT @limit";

                database.command.Parameters.AddWithValue("limit", limit);
            }

            MySqlDataReader result = database.command.ExecuteReader();

            List<UserDTO> users = new List<UserDTO>();
            while (result.Read())
            {
                UserDTO temp = new UserDTO();

                temp.Id = result.GetInt32(1);
                temp.BondNumber = int.Parse(result["bond_number"].ToString());
                temp.LastName = result["lastname"].ToString();
                temp.Initials = result["initials"].ToString();
                temp.Insertion = result["insertion"].ToString();
                temp.Name = result["firstname"].ToString();
                temp.Street = result["street"].ToString();
                temp.HouseNumber = int.Parse(result["housenumber"].ToString());
                temp.Addition = result["addition"].ToString();
                temp.ZipCode = result["zipcode"].ToString();
                temp.Residence = result["residence"].ToString();
                temp.Country = result["country"].ToString();
                temp.PhoneNumber = result["phonenumber"].ToString();
                temp.Gender = result["gender"].ToString();
                temp.Email = result["email"].ToString();
                temp.PhoneWork = result["workphone"].ToString();
                temp.PhoneMobile = result["mobilephone"].ToString();

                users.Add(temp);
            }

            database.CloseConnection();

            return users;
        }

        public static bool RemoveAll()
        {
            Database database = new Database();

            if (!database.OpenConnection())
                return false;

            database.command.CommandText = "DELETE FROM users";

            int affectedRows = database.command.ExecuteNonQuery();

            if (affectedRows > 0)
                return true;
            return false;
        }
    }
}
