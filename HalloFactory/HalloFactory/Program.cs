using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data.Common;

Console.WriteLine("Hello, World!");

var conString = "Server=(localdb)\\mssqllocaldb;Database=Northwnd;Trusted_Connection=true";
conString = "Data Source=C:\\DB\\Northwind.sqlite;";
//DbProviderFactory factory = SqlClientFactory.Instance;
DbProviderFactory factory = SqliteFactory.Instance;

DbConnection con = factory.CreateConnection();
con.ConnectionString = conString;
con.Open();

DbCommand cmd = factory.CreateCommand();
cmd.Connection = con;
cmd.CommandText = "SELECT * FROM Employees";

DbDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine($"{reader.GetString(reader.GetOrdinal("LastName"))} ");
}
