using FluentMigrator;
using Habanero.Util;

namespace LendingLibrary.Habanero.DB.Migrations.Migrations
{
    public class _2016041657_CreateTable_Item : Migration
    {
        public override void Up()
        {
            Create.Table("Item")
                .WithColumn("ItemId").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("ItemName").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable();
        }

        public override void Down()
        {
        }
    }
}
