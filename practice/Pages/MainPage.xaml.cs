using Microsoft.EntityFrameworkCore;
using practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        MainWindow mainWindow;
        public MainPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            using var db = new PracticeContext();
            try
            {
                DGridTickets.ItemsSource = db.Tickets.Include(t => t.Engineer).ToList();
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить таблицу с заявками", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditBttn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Content = new AddEditTicketPage(mainWindow, (sender as Button).DataContext as Models.Ticket);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using var db = new PracticeContext();
            if (SearchTextBox.Text.Length > 0)
            {
                try
                {
                    DGridTickets.ItemsSource = db.Tickets.Where(t => t.Id.ToString() == SearchTextBox.Text).Include(t => t.Engineer).ToList();
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить таблицу с заявками", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    DGridTickets.ItemsSource = db.Tickets.Include(t => t.Engineer).ToList();
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить таблицу с заявками", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddBttn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Content = new AddEditTicketPage(mainWindow);
        }

        private void DeleteBttn_Click(object sender, RoutedEventArgs e)
        {
            var tickets = DGridTickets.SelectedItems.Cast<Models.Ticket>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {tickets.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using var db = new PracticeContext();
                    db.RemoveRange(tickets);
                    db.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    mainWindow.MainFrame.Content = new MainPage(mainWindow);
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Device { get; set; }
        public string ProblemType { get; set; }
        public string Description { get; set; }
        public int EngineerId { get; set; }
        public string TicketStatus { get; set; }
        public string Client { get; set; }
        public Engineer Engineer { get; set; }
    }

    public class Engineer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Другие свойства
    }
}
