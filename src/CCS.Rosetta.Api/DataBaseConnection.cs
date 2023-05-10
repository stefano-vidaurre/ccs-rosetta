using System.Data.Common;
using Microsoft.Data.Sqlite;
using Dapper;

namespace CSS.Rosetta.Test;

public class DataBaseConnection : SqliteConnection
{
    private DataBaseConnection(string connectionString) : base(connectionString)
    {
        
    }

    public static DbConnection CreateInMemoryConnection()
    {
        DbConnection connection = new DataBaseConnection("Data Source=:memory:");
        connection.Open();
        connection.Execute("CREATE TABLE IF NOT EXISTS Projects (Name VARCHAR(255) PRIMARY KEY, Description TEXT);");
        return connection;
    }
    
    public static DbConnection CreateInFileConnection()
    {
        DbConnection connection = new DataBaseConnection("Data Source=DevelopDB.db;Pooling=False;");
        connection.Open();
        connection.Execute("CREATE TABLE IF NOT EXISTS Projects (Name VARCHAR(255) PRIMARY KEY, Description TEXT);");
        return connection;
    }
}