using LoadTestingSystem.Desktop.DTOs;
using LoadTestingSystem.Desktop.Services;
using LoadTestingSystem.Desktop.Views;
using System.Windows;

namespace LoadTestingSystem.Desktop
{
    public partial class MainWindow : Window
    {
        private int _currentPage = 1;
        private const int PageSize = 10;
        private int _totalPages;

        private readonly LoadTestResultService _loadTestResultService = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task LoadDataAsync()
        {
            var result = await _loadTestResultService.GetAllAsync(_currentPage, PageSize);

            ResultsDataGrid.ItemsSource = result.Items;
            _totalPages = result.TotalPages;

            PageInfoTextBlock.Text = _totalPages == 0 ? "0 / 0" : $"{_currentPage} / {_totalPages}";

            PreviousButton.IsEnabled = _currentPage > 1;
            NextButton.IsEnabled = _currentPage < _totalPages;
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

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTestResultDto? selected = ResultsDataGrid.SelectedItem as LoadTestResultDto;

            if (selected == null)
            {
                MessageBox.Show("Оберіть запис для редагування.");
                return;
            }

            AddLoadTestResultWindow window = new(selected);
            bool? result = window.ShowDialog();

            if (result == true)
            {
                await LoadDataAsync();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTestResultDto? selected = ResultsDataGrid.SelectedItem as LoadTestResultDto;

            if (selected == null)
            {
                MessageBox.Show("Оберіть запис для видалення.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Ви дійсно хочете видалити цей запис?","Підтвердження",MessageBoxButton.YesNo,MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            bool success = await _loadTestResultService.DeleteAsync(selected.Id);

            if (success)
            {
                await LoadDataAsync();
            }
            else
            {
                MessageBox.Show("Не вдалося видалити запис.");
            }
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;

            await LoadDataAsync();
        }

        private async void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;

            await LoadDataAsync();
        }
    }
}