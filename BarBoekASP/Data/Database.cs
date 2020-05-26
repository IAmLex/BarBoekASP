using System;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BarBoekASP.Data
{
    class Database
    {
        private string server;
        private string username;
        private string password;
        private string database;

        private MySqlConnection connection;
        public MySqlCommand command;

        public Database()
        {
            // TODO: Put in config.
            this.server = "84.31.134.4";
            this.username = "newuser";
            this.password = "test";
            this.database = "barboekmain";
        }

        public bool OpenConnection()
        {
            string connectionString = $"server={this.server};userid={this.username};password={this.password};database={this.database}";

            this.connection = new MySqlConnection(connectionString);

            try
            {
                this.connection.Open();

                this.command = this.connection.CreateCommand();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void CloseConnection()
        {
            this.connection.Close();
        }
    }
}
