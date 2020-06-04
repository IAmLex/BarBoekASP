using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using BarBoekASP.Logic.UserLogin;
using BarBoekASP.Interfaces;

namespace BarBoekASP.Data.MySQL
{
    public class UserMySQLContext:BaseMySQLContext,iUserRetrieveContext,iUserSaveContext
    {
        public UserMySQLContext(string connString) : base(connString)
        {

        }
        public List<UserTest> GetAllUsers()
        {
            string query = "Select * from user ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteQuery(query, parameters);

            List<UserTest> users = new List<UserTest>();

            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    UserTest user = DataSetParser.DataSetToUser(results, x);
                    users.Add(user);
                }
            }
            return users;
        }
        public void InsertUser(UserTest user)
        {
            string sql = " INSERT INTO user(email, password) VALUES(@email,@password);";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("email", user.UEmail));
            parameters.Add(new KeyValuePair<string, string>("password", user.Password));
            DataSet result = ExecuteQuery(sql, parameters);
        }
        public bool Inloggen(UserTest user)
        {
            bool check = false;
            string mysql = "Select * from user where email=@email and password=@password";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("email", user.UEmail));
            parameters.Add(new KeyValuePair<string, string>("password", user.Password));
            DataSet results = ExecuteQuery(mysql, parameters);
            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                check = true;
            }
            return check;
        }
        public bool CheckValidate(UserTest user)
        {
            bool check = false;
            string mysql = "Select * from user where email=@email";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("email", user.UEmail));
            DataSet results = ExecuteQuery(mysql, parameters);
            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                check = true;
            }
            return check;
        }
    }
}
