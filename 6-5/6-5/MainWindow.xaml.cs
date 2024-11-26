using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CityLibrary;

namespace CityApp
{
    public partial class MainWindow : Window
    {
        private City _currentCity;
        private Tourist _tourist;
        private List<double> _expenses; 

        public MainWindow()
        {
            InitializeComponent();
            _expenses = new List<double>();
        }

        private void NewCityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Считывание данных о городе
                string cityName = CityNameInput.Text;
                int population = int.Parse(CityPopulationInput.Text);
                double area = double.Parse(CityAreaInput.Text);
                int landmarks = int.Parse(CityLandmarksInput.Text);
                double dailyCost = double.Parse(CityDailyCostInput.Text);

                _currentCity = new City(cityName, population, area, landmarks, dailyCost);

                // Считывание данных о туристе
                string touristName = TouristNameInput.Text;
                double money = double.Parse(TouristMoneyInput.Text);
                int landmarksPerDay = int.Parse(TouristLandmarksInput.Text);

                _tourist = new Tourist(touristName, money, landmarksPerDay);

                // Обновление данных
                UpdateOutput();
                VisitButton.IsEnabled = _tourist.Money >= GetVisitCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VisitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double visitCost = GetVisitCost();
                _tourist.SpendMoney(visitCost);
                _expenses.Add(visitCost);

                UpdateOutput();
                VisitButton.IsEnabled = _tourist.Money >= GetVisitCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_expenses.Count == 0)
            {
                MessageBox.Show("Турист пока ничего не потратил.", "Статистика", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            double averageExpense = _expenses.Average();
            MessageBox.Show($"Средняя сумма потраченных денег: {averageExpense:F2} руб.", "Статистика", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private double GetVisitCost()
        {
            int days = _currentCity.GetDaysToVisit(_tourist.LandmarksPerDay);
            return days * _currentCity.DailyCost;
        }

        private void UpdateOutput()
        {
            OutputBlock.Text = $"Город: {_currentCity.Name}\n" +
                               $"Плотность населения: {_currentCity.GetPopulationDensity():F2} чел/км²\n" +
                               $"Дней на посещение: {_currentCity.GetDaysToVisit(_tourist.LandmarksPerDay)}\n\n" +
                               $"Турист: {_tourist.Name}\n" +
                               $"Осталось денег: {_tourist.Money:F2} руб.";
        }
    }
}
