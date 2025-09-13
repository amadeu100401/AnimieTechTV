
using AnimieTechTv.Exceptions;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimieTechTv.Infrastructure.Migrations;

public class DatabaseMigration
{
    private static int tryCount = 0;

    public static void Migrate(string connectionString, IServiceProvider serviceProvider, ILogger logger)
    {
        EnsureDatabaseCreated(connectionString, logger);
        MigrateDatabase(serviceProvider);
    }

    private static void EnsureDatabaseCreated(string connectionString, ILogger logger)
    {
        try
        {
            tryCount += 1;
            DoConnection(connectionString);
        } 
        catch (Exception ex)
        {
            if (tryCount <= 3)
            {
                logger.LogInformation("Retry to connect with data base");
                DoConnection(connectionString);
            }
            else
            {
                logger.LogError(ResourceMessageExceptions.DATABASE_ERROR);
                throw new Exception("Não foi possível conectar ao banco de dados.", ex);
            }
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
