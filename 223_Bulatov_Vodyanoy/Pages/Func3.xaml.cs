using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;

namespace _223_Bulatov_Vodyanoy.Pages
{
    public partial class Func3 : Page
    {
        private List<(double X, double Y)> _chartPoints = new List<(double, double)>();

        public Func3()
        {
            InitializeComponent();
            InitializeChartHost();
        }

        private void InitializeChartHost()
        {
            var chart = new Chart
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                BackColor = System.Drawing.Color.White,
                BorderlineColor = System.Drawing.Color.Black,
                BorderlineWidth = 1
            };

            var chartArea = new ChartArea("MainArea")
            {
                AxisX = { Title = "X" },
                AxisY = { Title = "Y" }
            };
            chart.ChartAreas.Add(chartArea);

            var series = new Series("Function")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.SteelBlue,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6,
                MarkerColor = System.Drawing.Color.DarkBlue
            };
            chart.Series.Add(series);
            chart.Legends.Add(new Legend());

            // Привязка к элементу с именем chartHost
            chartHost.Child = chart;
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Парсинг входных данных
                double x = double.Parse(TBX.Text.Trim(), CultureInfo.InvariantCulture);
                double b = double.Parse(TBB.Text.Trim(), CultureInfo.InvariantCulture);
                double x0 = double.Parse(TBX0.Text.Trim(), CultureInfo.InvariantCulture);
                double xk = double.Parse(TBXk.Text.Trim(), CultureInfo.InvariantCulture);
                double dx = double.Parse(TBdX.Text.Trim(), CultureInfo.InvariantCulture);

                // Валидация
                if (x0 >= xk)
                {
                    MessageBox.Show("Левая граница x0 должна быть меньше правой xk!", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (dx <= 0)
                {
                    MessageBox.Show("Шаг dx должен быть положительным числом!", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _chartPoints.Clear();
                var results = new List<string>();
                int pointsCount = 0;
                const int maxPoints = 5; // Требуется 5 значений

                for (double currentX = x0; currentX <= xk && pointsCount < maxPoints; currentX += dx)
                {
                    double y = CalculateFunction(currentX, b);

                    // Добавляем точку для графика
                    _chartPoints.Add((currentX, y));

                    // Добавляем строку для поля Ответ (только значение Y)
                    results.Add(y.ToString("F4"));

                    pointsCount++;
                }

                // Вывод всех 5 значений через Enter
                TBResult.Text = string.Join(Environment.NewLine, results);

                UpdateChart();
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте правильность ввода числовых значений (используйте точку для дробей)!",
                    "Ошибка формата", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка вычисления",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateFunction(double xVal, double bParam)
        {
            // Формула: y = 9(x + 15√(x³ + b³))
            double insideSqrt = Math.Pow(xVal, 3) + Math.Pow(bParam, 3);

            if (insideSqrt < 0)
            {
                throw new ArgumentException($"Отрицательное число под корнем: {insideSqrt:F2}. Проверьте значения x и b.");
            }

            double y = 9 * (xVal + 15 * Math.Sqrt(insideSqrt));
            return y;
        }

        private void UpdateChart()
        {
            if (chartHost.Child is Chart chart && chart.Series.Count > 0)
            {
                var series = chart.Series[0];
                series.Points.Clear();

                foreach (var point in _chartPoints)
                {
                    series.Points.AddXY(point.X, point.Y);
                }

                if (_chartPoints.Any())
                {
                    chart.ChartAreas[0].AxisX.Minimum = _chartPoints.Min(p => p.X) - 1;
                    chart.ChartAreas[0].AxisX.Maximum = _chartPoints.Max(p => p.X) + 1;

                    var minY = _chartPoints.Min(p => p.Y);
                    var maxY = _chartPoints.Max(p => p.Y);
                    var padding = (maxY - minY) * 0.2;
                    if (padding == 0) padding = 10;
                    chart.ChartAreas[0].AxisY.Minimum = minY - padding;
                    chart.ChartAreas[0].AxisY.Maximum = maxY + padding;
                }

                chart.Invalidate();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TBX.Clear();
            TBB.Clear();
            TBX0.Clear();
            TBXk.Clear();
            TBdX.Clear();
            TBResult.Clear();

            _chartPoints.Clear();
            UpdateChart();

            TBX.Focus();
        }
    }
}