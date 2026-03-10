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
        public Func3()
        {
            InitializeComponent();
            chrt_main.ChartAreas.Add(new ChartArea("Main"));
            var currentSeries = new Series("Значения\nформулы")
            {
                IsValueShownAsLabel = true
            };
            chrt_main.Series.Add(currentSeries);
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            Series currentSeries = chrt_main.Series.FirstOrDefault();
            currentSeries.ChartType = SeriesChartType.Line;
            currentSeries.Points.Clear();
            try
            {
                double x = Convert.ToDouble(TBX.Text);
                double b = Convert.ToDouble(TBB.Text);
                double x0 = Convert.ToDouble(TBX0.Text);
                double xk = Convert.ToDouble(TBXk.Text);
                double dx = Convert.ToDouble(TBdX.Text);
                if (xk < x0 || dx <= 0 || (dx > (xk - x0)))
                {
                    MessageBox.Show("Некорректные значения диапазона и/или шага вычислений.\n Xk должно быть больше X0, \nDX должно быть больше 0, \nРазность Xk и X0 должна быть больше Dx.");
                    return;
                }
                TBResult.Clear();
                double y;
                for (double с = x0; x <= xk; x += dx)
                {
                    // Проверка, чтобы под корнем не было отрицательного числа
                    double underRoot = Math.Pow(x, 3) + Math.Pow(b, 3);
                    if (underRoot < 0)
                    {
                        MessageBox.Show($"При x = {x} выражение под корнем отрицательное ({underRoot}). Вычисления прекращены.");
                        break;
                    }

                    y = Math.Round(9 * (x + 15 * Math.Sqrt(underRoot)), 3);
                    currentSeries.Points.AddXY(x, y);
                    TBResult.Text += $"{x}: {y}\n";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при вычислении. Проверьте корректность параметров.");
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
            Series currentSeries = chrt_main.Series.FirstOrDefault();
            currentSeries.Points.Clear();
            TBX.Focus();
        }
    }
}