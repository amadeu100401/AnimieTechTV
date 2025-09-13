
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace AnimieTechTv.Infrastructure.Migrations;

public class DatabaseMigration
{
    private static int tryCount = 0;

    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        EnsureDatabaseCreated(connectionString);
        MigrateDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated(string connectionString)
    {
        try
        {
            tryCount += 1;
            DoConnection(connectionString);
        } 
        catch (Exception ex)
        {
            if (tryCount <= 3)
                DoConnection(connectionString);
            else
                throw new Exception("Não foi possível conectar ao banco de dados.", ex);
        }
    }

    private static void DoConnection(string connectionString)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var dbName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.InitialCatalog = "master";

        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("name", dbName);

        var records = dbConnection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM sys.databases WHERE name = @name",
            parameters);

        if (records == 0)
            dbConnection.Execute($"CREATE DATABASE [{dbName}]");
    }

    private static void MigrateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp();
    }
}
