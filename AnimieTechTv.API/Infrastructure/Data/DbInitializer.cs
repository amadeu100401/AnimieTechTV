using Dapper;
using Microsoft.Data.SqlClient;

namespace AnimieTechTv.API.Infrastructure.Data;

public class DbInitializer
{
    private readonly IConfiguration _config;

    public DbInitializer(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnsureDatabaseAsync()
    {
        var databaseName = _config.GetValue<string>("DatabaseName") ?? "animiedb";
        var defaultConn = _config.GetConnectionString("Default")
                          ?? _config.GetConnectionString("DefaultConnection");

        var builder = new SqlConnectionStringBuilder(defaultConn)
        {
            InitialCatalog = "master"
        };
        var masterConnection = builder.ConnectionString;

        int attempts = 0;
        while (attempts < 20)
        {
            try
            {
                using var connection = new SqlConnection(masterConnection);
                await connection.OpenAsync();

                var dbExists = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM sys.databases WHERE name = @name",
                    new { name = databaseName });

                if (dbExists == 0)
                    await connection.ExecuteAsync($"CREATE DATABASE [{databaseName}]");

                Console.WriteLine($"Banco '{databaseName}' pronto.");
                break;
            }
            catch (SqlException)
            {
                attempts++;
                Console.WriteLine($"SQL Server não pronto ({attempts}/20). Tentando em 5s...");
                await Task.Delay(5000);
            }
        }

        if (attempts == 20)
            throw new Exception("SQL Server não ficou disponível após múltiplas tentativas.");
    }
}
