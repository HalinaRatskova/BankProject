using Bank.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    { BankDBContext context = new BankDBContext();
        private BankViewModel repo = new BankViewModel();
        private User user = new User();


        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login logWindow = new Login();
            logWindow.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = string.Empty;
            txtID.Text = string.Empty;
            txtBalance.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtAmount.IsEnabled = false;
            btnLogout.Visibility = Visibility.Hidden;
            btnLogin.Visibility = Visibility.Visible;
            btnLogin.IsEnabled = true;
            btnDeposit.IsEnabled = false;
            btnWithdraw.IsEnabled = false;


        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            user = repo.BalanceById(int.Parse(txtID.Text));
            decimal amountNumber;
            if (txtAmount.Text != string.Empty)
            {
                if (decimal.TryParse(txtAmount.Text, out amountNumber))
                {
                    repo.DepositBalance(user, decimal.Parse(txtAmount.Text));
                    txtBalance.Text = repo.GetBalance(user).ToString();
                    txtAmount.Text = string.Empty;
                    FileStream stream = new FileStream("logfile.txt", FileMode.Append, FileAccess.Write);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        DateTime timestamp = new DateTime();
                        timestamp = DateTime.Now;
                        string report = string.Format($"[{timestamp.Date.ToShortDateString()}|{timestamp.ToShortTimeString()}]" + " " +
                            " ID : " + txtID.Text + "Deposit: $  " + txtAmount.Text + " Balance: $ " + txtBalance.Text);
                        writer.WriteLine(report);
                    }

                    MessageBox.Show("Transaction is successful!");
                }
                else
                MessageBox.Show("Amount must be numeric.");
            }
            else 
                MessageBox.Show("Amount can't be empty.");
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
          
            user = repo.BalanceById(int.Parse(txtID.Text));
            decimal amount;
            if (!string.IsNullOrEmpty(txtAmount.Text) && (decimal.TryParse(txtAmount.Text, out amount)))
            {

                if (decimal.Parse(txtBalance.Text) != 0 && (decimal.Parse(txtBalance.Text.ToString()) > (decimal.Parse(txtAmount.Text))))
                {
                    repo.WithDrawBalance(user, decimal.Parse(txtAmount.Text));
                    txtBalance.Text = repo.GetBalance(user).ToString();
                    txtAmount.Text = string.Empty;
                    FileStream stream = new FileStream("logfile.txt", FileMode.Append, FileAccess.Write);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        DateTime timestamp = new DateTime();
                        timestamp = DateTime.Now;
                        string report = string.Format($"[{timestamp.Date.ToShortDateString()}|{timestamp.ToShortTimeString()}]" + "  " +
                            " ID : " + txtID.Text + " " + "Withdraw: $  " + txtAmount.Text + " Balance: $ " + txtBalance.Text);
                        writer.WriteLine(report);
                    }
                    MessageBox.Show("Transaction is successful!");
                }
                else
                {
                    MessageBox.Show("Not enought money on the balance.");
                    txtAmount.Text = string.Empty;
                }
            }else
                MessageBox.Show("Amount can't be empty. Amount must be numeric.");
        }


    }
}
