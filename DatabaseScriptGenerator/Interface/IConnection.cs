
using System.Data;
using DatabaseScriptGenerator.Enums;

namespace DatabaseScriptGenerator.Interface
{
    public interface IConnection
    {
        string Server { get; set; }
        string DatabaseName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        Authentication Authentication { get; set; }

        void Connect();
        void Close();
        string[] GetIdentityColumns(string tableName);
    }
}
