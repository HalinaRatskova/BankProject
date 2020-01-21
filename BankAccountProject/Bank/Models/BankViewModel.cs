using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bank.Models
{
    public class BankViewModel
    {
        //access  data via DBContext object
        BankDBContext context = new BankDBContext();
        User u = new User();

       

        public List<User> GetAllUsersData()
        {
            return context.Users.Include("UserAccount").ToList();
        }

        public Account GetAccountPerUser(int ID)
        {
            return (from u in context.Users
                    where u.ID == ID
                    select u.UserAccount).FirstOrDefault();

        }

        //public Account GetUserAcountByID(int userid)
        //{
        //    return (from u in context.Users
        //            where u.ID == userid
        //            select u.UserAccount).SingleOrDefault();

        //}

        //Delete User and Acoount
        public void DeleteUserRecord(int ID)
        {
            User toBeDeleted = (from usr in context.Users
                                where usr.ID == ID
                                select usr).SingleOrDefault();
            Account AccountToBeDelete = GetAccountPerUser(ID);
            context.Accounts.Remove(AccountToBeDelete);
            context.Users.Remove(toBeDeleted);
            context.SaveChanges();
        }



        //Update
        public void UpdateUser(User updated)
        {
            int userID = updated.ID;
            User current = (from usr in context.Users
                            where usr.ID == userID
                            select usr).SingleOrDefault();
            current.Name = updated.Name;
            current.Password = updated.Password;
            context.SaveChanges();

        }

        public User GetUserWithID(int ID)
        {
            return (from u in context.Users.Include("UserAccount")
                    where u.ID == ID
                    select u).SingleOrDefault();
        }


        //Add
        public void AddUser(User usr)
        {
            context.Users.Add(usr);
            context.SaveChanges();
          
        }


        public User Login(int logId, string logPassword)
        {

            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == logId && u.Password.Contains(logPassword)
                         select u).SingleOrDefault();
            return user;

        }


       //Depoit and Withdraw 

        public void DepositBalance(User newUser, decimal deposit)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == newUser.ID
                         select u).SingleOrDefault();
            user.UserAccount.Balance += deposit;

            context.SaveChanges();
        }
        public void WithDrawBalance(User newUser, decimal withdraw)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == newUser.ID
                         select u).SingleOrDefault();
            user.UserAccount.Balance -= withdraw;


            context.SaveChanges();
        }

        public decimal GetBalance(User user)
        {
            return (from u in context.Users.Include("UserAccount")
                    where u.ID == user.ID
                    select u.UserAccount.Balance).SingleOrDefault();
        }

        public User BalanceById(int logId)
        {
            User user = (from u in context.Users.Include("UserAccount")
                         where u.ID == logId
                         select u).SingleOrDefault();
            return user;
        }

        }
    }


