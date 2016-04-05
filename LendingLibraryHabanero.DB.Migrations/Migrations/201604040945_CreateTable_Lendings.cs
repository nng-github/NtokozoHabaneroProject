using FluentMigrator;

namespace LendingLibrary.Habanero.DB.Migrations.Migrations
{
    [Migration(201604040945)]
    public class _201604040945_CreateTable_Lendings : Migration
    {
        public override void Up()
        {
            Create.Table("Lending")
                .WithColumn("LendingId").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("PersonId").AsGuid().NotNullable().ForeignKey("FK_Person_Lending", "Lending", "LendingId")
                .WithColumn("ItemId").AsGuid().NotNullable().ForeignKey("FK_Item_Lending", "Item", "LendingId")
                .WithColumn("RequestDate").AsDateTime().Nullable()
                .WithColumn("LoanDate").AsDateTime()
                .WithColumn("ReturnDate").AsDateTime();
        }

        public override void Down()
        {}
    }
}
