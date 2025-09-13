using FluentMigrator;

namespace AnimieTechTv.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_ANIMIE, "Create animie table")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        var schema = SchemaNames.CATALOG_SCHEMA;

        Create.Schema(schema);

        CreateTable("animies", schema)
            .WithColumn("Name").AsString(150).NotNullable()
            .WithColumn("Director").AsString(150).NotNullable()
            .WithColumn("Resume").AsString(int.MaxValue).NotNullable();
    }
}
