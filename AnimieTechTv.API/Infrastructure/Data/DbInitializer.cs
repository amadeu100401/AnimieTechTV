using AnimieTechTv.API.Controllers;
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

    public async Task EnsureDataBaseAsync()
    {
        var databaseName = _config.GetValue<string>("DatabaseName") ?? "animiedb";
        var defaultConn = _config.GetConnectionString("DefaultConnection");

        var builder = new SqlConnectionStringBuilder(defaultConn)
        {
            InitialCatalog = "master"
        };

        var masterConnectionString = builder.ConnectionString;

        using var connection = new SqlConnection(masterConnectionString);
        await connection.OpenAsync();

        var dbExists = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM sys.databases WHERE name = @name",
            new { name = databaseName });

        if (dbExists == 0)
            await connection.ExecuteAsync($"CREATE DATABASE [{databaseName}]");
    }
}
