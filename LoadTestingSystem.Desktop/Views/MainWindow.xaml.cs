using LoadTestingSystem.Desktop.Services;
using LoadTestingSystem.Desktop.Views;
using System.Windows;

namespace LoadTestingSystem.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly LoadTestResultService _loadTestResultService = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task LoadDataAsync()
        {
            var result = await _loadTestResultService.GetAllAsync();

            ResultsDataGrid.ItemsSource = result.Items;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddLoadTestResultWindow window = new();

            bool? result = window.ShowDialog();

            if (result == true)
            {
                await LoadDataAsync();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}