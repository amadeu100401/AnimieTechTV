using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace AnimieTechTv.API.Infrastructure.Migrations;

public abstract class VersionBase : ForwardOnlyMigration
{
    public ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName, string? schema = null)
    {
        var create = String.IsNullOrWhiteSpace(schema) ? Create.Table(tableName) : Create.Table(tableName).InSchema(schema);

        return create.WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
            .WithColumn("CreatedAt").AsDateTime2().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
            .WithColumn("UpdatedAt").AsDateTime2().Nullable();
    }
}
