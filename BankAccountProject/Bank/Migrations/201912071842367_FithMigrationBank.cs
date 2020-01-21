namespace Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FithMigrationBank : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "User_ID", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "User_ID" });
            AddColumn("dbo.Users", "UserAccount_AccountNumber", c => c.Int());
            CreateIndex("dbo.Users", "UserAccount_AccountNumber");
            AddForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts", "AccountNumber");
            DropColumn("dbo.Accounts", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "User_ID", c => c.Int());
            DropForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "UserAccount_AccountNumber" });
            DropColumn("dbo.Users", "UserAccount_AccountNumber");
            CreateIndex("dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Accounts", "User_ID", "dbo.Users", "ID");
        }
    }
}
