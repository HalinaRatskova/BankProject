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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private BankViewModel repo = new BankViewModel();
        BankDBContext context = new BankDBContext();
        AdministrationPanel adminWindow = new AdministrationPanel();
        User u = new User();

    public AddUser()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {User u = new User();

            if (txtNameAdd.Text == "" || txtPasswordAdd.Password == "")
                MessageBox.Show("Please, enter Name and Password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            else
            {

                User user = new User();
                Account account = new Account();
                user.Name = txtNameAdd.Text;
                user.Password = txtPasswordAdd.Password;
                user.UserAccount = account;
                repo.AddUser(user);
                adminWindow.dgUsers.ItemsSource = repo.GetAllUsersData();
                this.Close();

           

            }
 
        }

    }
}
