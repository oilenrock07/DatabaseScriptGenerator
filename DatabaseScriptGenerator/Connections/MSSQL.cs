using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DatabaseScriptGenerator.Enums;
using DatabaseScriptGenerator.Interface;
using System;

namespace DatabaseScriptGenerator.Connections
{
    //SQL Server 2005, 2008 and 2012
    public class MSSQL : IConnection
    {
        public string DatabaseName { get; set; }
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Authentication Authentication { get; set; }

        private SqlConnection _connection;

        public MSSQL()
        {
            
        }

        public void Connect()
        {
            _connection = new SqlConnection();

            string connectionString = "";
            if(Authentication == Authentication.SQLAuthentication)
                connectionString = String.Format("Server={0};uid={1};pwd={2};Database={3};Min Pool Size=10;", Server, UserName, Password, DatabaseName);

            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public void Close()
        {
            if(_connection.State == ConnectionState.Open)
                _connection.Close();
        }


        public void GetRows(string tableName, params string[] ids)
        {
            //get the primary key of the table
        }

        public DataSet GetRows(string query)
        {
            var ds = new DataSet();
            using (var adapter = new SqlDataAdapter(query, _connection))
            {
                adapter.Fill(ds);
                return ds;
            }
        }


        public string[] GetIdentityColumns(string tableName)
        {
            var identityColumns = new List<string>();
            string query = String.Format("SELECT name, is_identity FROM sys.columns WHERE object_id = object_id('{0}') AND is_identity=1", tableName);
            using (var command = new SqlCommand(query, _connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        identityColumns.Add(reader[0].ToString());
                    }
                }
            }

            return identityColumns.ToArray();

        }
    }
}
