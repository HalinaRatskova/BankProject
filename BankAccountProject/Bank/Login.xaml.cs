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

namespace Bank
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 
    public partial class Login : Window
    {
        User u = new User();
        BankDBContext context = new BankDBContext();
        MainWindow mainWindow = new MainWindow();
        AdministrationPanel adminWindow = new AdministrationPanel();
        private BankViewModel rep = new BankViewModel();
        private User user;
        public Login()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtPassword.Password.ToString()))
            {
                MessageBox.Show("ID and Pasword can't be empty.");
            }
            else
            {
                LoginUser();
            }

        }

        public void LoginUser()
        {
            int id;
            if (int.TryParse(txtID.Text.ToString(), out id))
            {
                user = rep.Login(id, txtPassword.Password.ToString());
                if ((user == null))
                {
                    MessageBox.Show("Invalid ID/Password.");
                }
                else if ((user.UserAccount.AccountNumber == 15) && (user.Password == "1"))
                {
                    MessageBox.Show("Welcome " + user.Name+"!");
                    this.Close();
                    AdministrationPanel adminWindow = new AdministrationPanel();
                    adminWindow.ShowDialog();
                }
                else if ((user != null))
                {
                    MessageBox.Show("Welcome " + user.Name + "!");
                    mainWindow.Show();
                    mainWindow.btnDeposit.IsEnabled = true;
                    mainWindow.btnWithdraw.IsEnabled = true;

                    mainWindow.txtAmount.IsEnabled = true;
                    mainWindow.txtName.Text = user.Name;
                    mainWindow.txtID.Text = user.ID.ToString();
                    mainWindow.txtBalance.Text = user.UserAccount.Balance.ToString();
                    mainWindow.btnLogin.IsEnabled = false;
                    mainWindow.btnLogin.Visibility = Visibility.Hidden;
                    mainWindow.btnLogout.IsEnabled = true;
                    mainWindow.btnLogout.Visibility = Visibility.Visible;
                    
                    this.Close();
                }
                else
                    return;

            }
            else
            {
                MessageBox.Show("ID must be numeric.");
            }


        }

    }  
}
