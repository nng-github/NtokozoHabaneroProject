using FluentMigrator;

namespace LendingLibrary.Habanero.DB.Migrations.Migrations
{
    [Migration(201603290855)]
    public class _201603301539_CreateTablePerson : Migration
    {
        public override void Up()
        {
            Create.Table("Person")
                .WithColumn("PersonId").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("DateOfBirth").AsDate().Nullable()
                .WithColumn("Education").AsString(255).Nullable()
                .WithColumn("HomeTown").AsString(255).Nullable();
        }

        public override void Down()
        {}
    }
}
