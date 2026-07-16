using LoadTestingSystem.Desktop.DTOs;
using LoadTestingSystem.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoadTestingSystem.Desktop.Views
{
    public partial class AddLoadTestResultWindow : Window
    {
        private bool _isEditMode;
        private int _currentId;
        private readonly LoadTestResultService _loadTestResultService = new();
        public AddLoadTestResultWindow()
        {
            InitializeComponent();
        }

        public AddLoadTestResultWindow(LoadTestResultDto result)
        {
            InitializeComponent();

            _currentId = result.Id;
            _isEditMode = true;

            NameTextBox.Text = result.TestName;
            RPSTextBox.Text = result.RequestsPerSecond.ToString();
            AvgTimeTextBox.Text = result.AverageResponseTime.ToString();
            ErrorsCountTextBox.Text = result.ErrorCount.ToString();  
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string testName = NameTextBox.Text;
            if (string.IsNullOrWhiteSpace(testName))
            {
                MessageBox.Show("Введіть назву тесту.");
                return;
            }
            string rpsText = RPSTextBox.Text;
            if (!int.TryParse(rpsText, out int rps))
            {
                MessageBox.Show("RPS повинен бути числом.");
                return;
            }
            string avgTimeText = AvgTimeTextBox.Text;
            if (!double.TryParse(avgTimeText, out double avg))
            {
                MessageBox.Show("Середній час повинен бути числом.");
                return;
            }
            string errorCountText = ErrorsCountTextBox.Text;
            if (!int.TryParse(errorCountText, out int er))
            {
                MessageBox.Show("Помилки повині бути числом.");
                return;
            }

            CreateLoadTestResult request = new()
            {
                TestName = testName,
                RequestsPerSecond = rps,
                AverageResponseTime = avg,
                ErrorCount = er
            };

            bool success;

            if (_isEditMode)
            {
                success = await _loadTestResultService.UpdateAsync(_currentId, request);
            }
            else
            {
                success = await _loadTestResultService.CreateAsync(request);
            }

            if (success)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Не вдалося додати запис.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
