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
    /// Логика взаимодействия для AddEditTicketPage.xaml
    /// </summary>
    public partial class AddEditTicketPage : UserControl
    {
        Models.Ticket? ticket;
        MainWindow mainWindow;
        public AddEditTicketPage(MainWindow _mainWindow, Models.Ticket _ticket = null)
        {
            InitializeComponent();
            ticket = _ticket ?? null;
            mainWindow = _mainWindow;
            using var db = new PracticeContext();
            EnginerListBox.ItemsSource = db.Users.Select(u => u.Login).ToList();
            StatusListBox.ItemsSource = new List<string> { "в ожидаении", "в работе", "выполнено" };
            if (ticket != null )
            {
                IdTextBox.Text = ticket.Id.ToString();
                CreationDateTextBox.Text = ticket.CreationDate.ToString("yyyy-MM-dd");
                ClosingDateTextBox.Text = ticket.ClosingDate.ToString("yyyy-MM-dd");
                DeviceTextBox.Text = ticket.Device.ToString();
                ProblemTypeTextBox.Text = ticket.ProblemType.ToString();
                DescriptionTextBox.Text = ticket.Description.ToString();
                EnginerListBox.SelectedValue = ticket.Engineer.Login.ToString();
                StatusListBox.SelectedValue = ticket.TicketStatus.ToString();
                ClientTextBox.Text = ticket.Client.ToString();
            }
        }

        private void GoBack()
        {
            mainWindow.MainFrame.Content = new MainPage(mainWindow);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using var db = new PracticeContext();
            try
            {
                if (ticket != null)
                {
                    Models.Ticket new_ticket = db.Tickets.FirstOrDefault(t => t.Id == ticket.Id);
                    DateTime date = DateTime.ParseExact(CreationDateTextBox.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    new_ticket.CreationDate = DateOnly.FromDateTime(date);
                    date = DateTime.ParseExact(ClosingDateTextBox.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    new_ticket.ClosingDate = DateOnly.FromDateTime(date);
                    new_ticket.Device = DeviceTextBox.Text;
                    new_ticket.ProblemType = ProblemTypeTextBox.Text;
                    new_ticket.Description = DescriptionTextBox.Text;
                    new_ticket.EngineerId = db.Users.FirstOrDefault(u => u.Login == EnginerListBox.SelectedValue.ToString()).Id;
                    new_ticket.TicketStatus = StatusListBox.SelectedValue.ToString();
                    new_ticket.Client = ClientTextBox.Text;
                    db.Tickets.Update(new_ticket);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                    GoBack();
                }
                else
                {
                    Models.Ticket new_ticker = new Models.Ticket();
                    DateTime date = DateTime.ParseExact(CreationDateTextBox.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    new_ticker.CreationDate = DateOnly.FromDateTime(date);
                    date = DateTime.ParseExact(ClosingDateTextBox.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    new_ticker.ClosingDate = DateOnly.FromDateTime(date);
                    new_ticker.Device = DeviceTextBox.Text;
                    new_ticker.ProblemType = ProblemTypeTextBox.Text;
                    new_ticker.Description = DescriptionTextBox.Text;
                    new_ticker.EngineerId = db.Users.FirstOrDefault(u => u.Login == EnginerListBox.SelectedValue).Id;
                    new_ticker.TicketStatus = StatusListBox.SelectedValue.ToString();
                    new_ticker.Client = ClientTextBox.Text;
                    db.Tickets.Update(new_ticker);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                    GoBack();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при внесении/изменении данных, проверьте правильность вводимых данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }
    }
}
