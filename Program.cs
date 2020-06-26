using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace EFGames
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Checks for ColumnName when missing column name:

            // using var conn = new SqlConnection(@"Server=localhost;Database=test;User=SA;Password=Abcd5678;Connect Timeout=60;ConnectRetryCount=0");
            // using var conn = new NpgsqlConnection("Host=localhost;Username=test;Password=test");
            // using var conn = new SqliteConnection("Filename=:memory:");
            // using var conn = new System.Data.SQLite.SQLiteConnection("DataSource=:memory:");
            // using var conn = new MySqlConnector.MySqlConnection(@"Server=localhost;User ID=test;Password=test;Database=test");
            // using var conn = new MySql.Data.MySqlClient.MySqlConnection(@"Server=localhost;User ID=test;Password=test;Database=test");
            // await CheckMissingColumName(conn);

            // DbProviderFactory checks:

            // Report(Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            // Report(System.Data.SqlClient.SqlClientFactory.Instance);
            // Report(Npgsql.NpgsqlFactory.Instance);
            // Report(MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            // Report(MySqlConnector.MySqlClientFactory.Instance);
            // Report(Microsoft.Data.Sqlite.SqliteFactory.Instance);
            // Report(Microsoft.Data.Sqlite.SqliteFactory.Instance);
            // Report(System.Data.SQLite.SQLiteFactory.Instance);
            // Report(Oracle.ManagedDataAccess.Client.OracleClientFactory.Instance);
        }

        static void Report(DbProviderFactory factory)
        {
            Console.Write($"Factory: {factory.GetType().FullName}");
            Console.WriteLine($@"
  {nameof(factory.CreateCommand)} returns null: {factory.CreateCommand() is null}
  {nameof(factory.CreateCommandBuilder)} returns null: {factory.CreateCommandBuilder() is null}
  {nameof(factory.CreateConnection)} returns null: {factory.CreateConnection() is null}
  {nameof(factory.CreateConnectionStringBuilder)} returns null: {factory.CreateConnectionStringBuilder() is null}
  {nameof(factory.CreateDataAdapter)} return nulls: {factory.CreateDataAdapter() is null}
  {nameof(factory.CreateParameter)} return nulls: {factory.CreateParameter() is null}
  {nameof(factory.CreateDataSourceEnumerator)} returns null: {factory.CreateDataSourceEnumerator() is null}");
            Console.WriteLine();
        }

        static async Task CheckMissingColumName(DbConnection conn)
        {
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT 1";
            using var reader = await cmd.ExecuteReaderAsync();
            var schema = reader.GetColumnSchema();

            Console.WriteLine($"Connection {conn.GetType().FullName} returns null for missing column name: {schema[0].ColumnName is null}");
        }
    }
}
