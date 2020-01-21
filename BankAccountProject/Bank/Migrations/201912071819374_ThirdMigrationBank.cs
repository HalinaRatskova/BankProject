namespace Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigrationBank : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "UserAccount_AccountNumber" });
            AddColumn("dbo.Accounts", "User_ID", c => c.Int());
            CreateIndex("dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Accounts", "User_ID", "dbo.Users", "ID");
            DropColumn("dbo.Users", "UserAccount_AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserAccount_AccountNumber", c => c.Int());
            DropForeignKey("dbo.Accounts", "User_ID", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "User_ID" });
            DropColumn("dbo.Accounts", "User_ID");
            CreateIndex("dbo.Users", "UserAccount_AccountNumber");
            AddForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts", "AccountNumber");
        }
    }
}
