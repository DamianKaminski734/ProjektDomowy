using ProjektDomowy.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ProjektDomowy
{
    public partial class MainWindow : Window
    {
        private MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            vm.AddBook();
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteBook();
        }

        private void MarkAsRead_Click(object sender, RoutedEventArgs e)
        {
            vm.MarkAsRead();
        }

        private void YearTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}