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

namespace _223_Bulatov_Vodyanoy
{
    /// <summary>
    /// Логика взаимодействия для Func2.xaml
    /// </summary>
    public partial class Func2 : Page
    {
        public Func2()
        {
            InitializeComponent();
        }

        private double CalculateF(double x)
        {
            if (RadioSinh.IsChecked == true)
                return Math.Sinh(x);  // sh(x)
            else if (RadioSquare.IsChecked == true)
                return x * x;          // x^2
            else if (RadioExp.IsChecked == true)
                return Math.Exp(x);    // e^x
            else
                throw new InvalidOperationException("Выберите функцию f(x)");
        }

        private double CalculateS(double x, double b)
        {
            // Вычисляем f(x)
            double fx = CalculateF(x);

            // Вычисляем xb (произведение x и b)
            double xb = x * b;

            double s;

            // Проверяем условия
            if (xb > 1 && xb < 10)
            {
                // s = e^f
                s = Math.Exp(fx);
            }
            else if (xb > 12 && xb < 40)
            {
                // s = √|f(x) + 4*b|
                s = Math.Sqrt(Math.Abs(fx + 4 * b));
            }
            else
            {
                // s = b*f(x)²
                s = b * fx * fx;
            }

            return s;
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            TBResult.Clear();

            try
            {
                if (string.IsNullOrWhiteSpace(TBX.Text))
                {
                    throw new ArgumentException("Поле X не заполнено!");
                }

                if (string.IsNullOrWhiteSpace(TBB.Text))
                {
                    throw new ArgumentException("Поле B не заполнено!");
                }

                if (!RadioSinh.IsChecked.Value &&
                    !RadioSquare.IsChecked.Value &&
                    !RadioExp.IsChecked.Value)
                {
                    throw new ArgumentException("Выберите функцию f(x)!");
                }

                string xText = TBX.Text.Trim().Replace(".", ",");
                string bText = TBB.Text.Trim().Replace(".", ",");

                if (!double.TryParse(xText, out double x))
                {
                    throw new FormatException("Поле X должно содержать число!");
                }

                if (!double.TryParse(bText, out double b))
                {
                    throw new FormatException("Поле B должно содержать число!");
                }

                double s = CalculateS(x, b);

                double xb = x * b;
                string funcName = RadioSinh.IsChecked.Value ? "sh(x)" :
                                 RadioSquare.IsChecked.Value ? "x^2" : "e^x";

                TBResult.Text = s.ToString();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TBX.Clear();
            TBB.Clear();
            TBResult.Clear();
            TBX.Focus();
        }
    }
}