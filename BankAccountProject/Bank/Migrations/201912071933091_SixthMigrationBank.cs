namespace Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthMigrationBank : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "UserAccount_AccountNumber" });
            AlterColumn("dbo.Users", "UserAccount_AccountNumber", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "UserAccount_AccountNumber");
            AddForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts", "AccountNumber", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "UserAccount_AccountNumber" });
            AlterColumn("dbo.Users", "UserAccount_AccountNumber", c => c.Int());
            CreateIndex("dbo.Users", "UserAccount_AccountNumber");
            AddForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts", "AccountNumber");
        }
    }
}
