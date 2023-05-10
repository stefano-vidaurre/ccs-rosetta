using System.Data.Common;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CSS.Rosetta.Test;

public class DataBaseTestConnection : SqliteConnection
{
    private DataBaseTestConnection(string connectionString) : base(connectionString)
    {
        
    }

    public static DbConnection CreateInMemoryConnection()
    {
        DbConnection connection = new DataBaseTestConnection("Data Source=:memory:");
        connection.Open();
        connection.Execute("CREATE TABLE IF NOT EXISTS Projects (Name VARCHAR(255) PRIMARY KEY, Description TEXT);");
        return connection;
    }
}