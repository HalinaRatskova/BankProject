using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;


namespace Bank
{
    /// <summary>
    /// Interaction logic for UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Window
    {
        BankDBContext context = new BankDBContext();
        private BankViewModel repo = new BankViewModel();
        AdministrationPanel adminWindow = new AdministrationPanel();
        User user = new User();
        int userId;

        
        public UpdateUser(int userid)
        {
            InitializeComponent();
            this.userId = userid;
            user = repo.GetUserWithID(userId);
            txtID.Text = user.ID.ToString();
            txtAccountNumber.Text = user.UserAccount.AccountNumber.ToString();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewName.Text !="" && txtNewPassword.Text != "")
            {
                user.Name = txtNewName.Text;
                user.Password = txtNewPassword.Text;
                repo.UpdateUser(user);
                
                this.Close(); 

            }
            else
            {
                MessageBox.Show("Enter New Name and New Password!!");
            }
            
        }
    }
}
