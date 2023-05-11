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
        return CreateConnection("Data Source=:memory:");
    }

    public static DbConnection CreateInFileConnection()
    {
        return CreateConnection("Data Source=DevelopDB.db;Pooling=False;");
    }

    private static DbConnection CreateConnection(string connectionString)
    {
        DbConnection connection = new DataBaseConnection(connectionString);
        connection.Open();
        connection.Execute("CREATE TABLE IF NOT EXISTS Projects (Name VARCHAR(255) PRIMARY KEY, Description TEXT);");
        return connection;
    }
}