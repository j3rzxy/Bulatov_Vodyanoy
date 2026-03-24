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

                for (double currentX = x0; currentX <= xk; currentX += dx)
                {
                    if (Functions3.F3Calculate(currentX, b, out double y, out string error))
                    {
                        currentSeries.Points.AddXY(currentX, y);
                        TBResult.Text += $"{currentX}: {y}\n";
                    }
                    else
                    {
                        MessageBox.Show($"{error}. Вычисления прекращены.");
                        break;
                    }
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

    public static class Functions3
    {
        public static bool F3Calculate(double x, double b, out double result, out string error)
        {
            result = 0;
            error = null;

            double underRoot = Math.Pow(x, 3) + Math.Pow(b, 3);

            if (underRoot < 0)
            {
                error = $"При x = {x} выражение под корнем отрицательное ({underRoot})";
                return false;
            }

            result = Math.Round(9 * (x + 15 * Math.Sqrt(underRoot)), 3);
            return true;
        }
    }
}