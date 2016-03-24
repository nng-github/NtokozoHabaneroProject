using FluentMigrator;

namespace NtokozoHabanero.DB.Migrations.Migrations
{
    [Migration(201603241503)]
    public class _201603241503_CreateTableNtokozo : Migration
    {
        public override void Up()
        {
            Create.Table("Ntokozo")
                .WithColumn("NtokozoId").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("DateOfBirth").AsDate().Nullable()
                .WithColumn("Education").AsString(255).Nullable()
                .WithColumn("HomeTown").AsString(255).Nullable()
                .WithColumn("Gender").AsInt32().Nullable();
        }

        public override void Down()
        {}
    }
}
