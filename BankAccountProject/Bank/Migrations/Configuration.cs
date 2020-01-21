namespace Bank.Migrations
{
    using Bank.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bank.Models.BankDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bank.Models.BankDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.RemoveRange(context.Users);
            context.Accounts.RemoveRange(context.Accounts);
            //create object employee
            List<User> users = new List<User>();
            //add information 
            users.Add(new User()
            {
                Name = "Michael",
                Password = "111",
              UserAccount = new Account()
             {
                  
                       Balance=1000
                    },
   
            });
            users.Add(new User()
            {
                Name = "Barry Allen",
                Password = "222",
                UserAccount = new Account()
                {

                    Balance = 1010
                },

            });
            users.Add(new User()
            {
                Name = "admin",
                Password = "1",
                UserAccount = new Account()
                {

                    Balance = 0
                },

            });
            users.Add(new User()
            {
                Name = "Bruce Banner",
                Password = "333",
                UserAccount = new Account()
                {

                    Balance = 500
                },

            });
            users.Add(new User()
            {
                Name = "Barr Allen",
                Password = "222",
                UserAccount = new Account()
                {

                    Balance = 0
                },

            });

            context.Users.AddRange(users);
            base.Seed(context);
        }
    }
}
