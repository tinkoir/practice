using practice.Models;
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

namespace practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public MainWindow mainWindow;
        public LoginPage(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
            InitializeComponent();
        }

        private void EnterBttn_Click(object sender, RoutedEventArgs e)
        {
            using var db = new PracticeContext();
            User? user = db.Users.FirstOrDefault(u => u.Password == PasswordTextBox.Text && u.Login == LoginTextBox.Text);
            if (user == null)
            {
                MessageBox.Show("Пользователь с веденными данными не найден", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Вход выполнен успешно", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
            mainWindow.SetUser(user);
            mainWindow.MainFrame.Content = new MainPage(mainWindow);
        }
    }
}
