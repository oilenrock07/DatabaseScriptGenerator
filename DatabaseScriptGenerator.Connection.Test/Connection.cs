using System;
using DatabaseScriptGenerator.Connections;
using DatabaseScriptGenerator.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using DatabaseScriptGenerator.DataConverter;

namespace DatabaseScriptGenerator.Connection.Test
{
    [TestClass]
    public class Connection
    {
        private string _username = "sa";
        private string _password = "1234567890a!";
        private string _server = "(local)";
        private string _database = "DEV_Burnsco";
        private Authentication _authentication = Authentication.SQLAuthentication;
        private MSSQL database = new MSSQL();

        public Connection()
        {
            database.UserName = _username;
            database.Authentication = _authentication;
            database.Server = _server;
            database.Password = _password;
            database.DatabaseName = _database;
        }

        [TestMethod]
        public void TestSQLConnection()
        {
            database.Connect();
        }

        [TestMethod]
        public void TestGetRowsByQuery()
        {
            database.Connect();
            DataSet ds = database.GetRows("SELECT * FROM Max_Panels");
            string[] identityColumns = database.GetIdentityColumns("Max_Panels");
            database.Close();

            string insertScript = Converter.CreateInsertScript(ds.Tables[0], identityColumns, "Max_Panels");
        }
    }
}
