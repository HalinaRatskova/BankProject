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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bank
{
    /// <summary>
    /// Interaction logic for AdministrationPanel.xaml
    /// </summary>
    public partial class AdministrationPanel : Window
    {BankDBContext context = new BankDBContext();
        private BankViewModel repo = new BankViewModel();
        User user = new User();


        public AdministrationPanel()
        {
            InitializeComponent();
          dgUsers.ItemsSource = repo.GetAllUsersData(); 

            dgUsers.ItemsSource = (from e in context.Users.Include("UserAccount")
                                       select e).ToList();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //Delete User and Account
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null && MessageBox.Show("Are you sure you want to delete?\n" +
              "This will delete the account records as well", "Confirm Delete."
              , MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
            {
                object row = dgUsers.SelectedItem;
                int selectedID = Convert.ToInt32((dgUsers.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
                if (selectedID != 1)
                {
                    repo.DeleteUserRecord(selectedID);
                    dgUsers.ItemsSource = repo.GetAllUsersData();
                    dgUsers.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Select user to delete.");
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
                AddUser addUserWindow = new AddUser();
                    addUserWindow.Show();
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem == null)
                MessageBox.Show("Please,select an user.");
            else
            {
                
                 object row = dgUsers.SelectedItem;
                int selectedID = Convert.ToInt32((dgUsers.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
               UpdateUser updateUserWindow = new UpdateUser(selectedID);
                updateUserWindow.Show();
                
                updateUserWindow.txtID.Text = string.Format("{0}", (dgUsers.SelectedItem as User).ID);
                updateUserWindow.txtAccountNumber.Text = string.Format("{0}", (dgUsers.SelectedItem as User).UserAccount.AccountNumber);
               
            }

        }
    }
}
