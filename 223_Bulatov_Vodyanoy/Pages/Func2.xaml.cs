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


        private bool F2Calculate(double x, double b, out double result, out string selectedFunction)
        {
            result = 0;
            selectedFunction = null;

            try
            {
                // Вычисляем f(x)
                double fx;

                if (RadioSinh?.IsChecked == true)
                {
                    fx = Math.Sinh(x);
                    selectedFunction = "sh(x)";
                }
                else if (RadioSquare?.IsChecked == true)
                {
                    fx = x * x;
                    selectedFunction = "x^2";
                }
                else if (RadioExp?.IsChecked == true)
                {
                    fx = Math.Exp(x);
                    selectedFunction = "e^x";
                }
                else
                {
                    return false; // Функция не выбрана
                }

                // Вычисляем xb (произведение x и b)
                double xb = x * b;

                // Проверяем условия
                if (xb > 1 && xb < 10)
                {
                    // s = e^f
                    result = Math.Exp(fx);
                }
                else if (xb > 12 && xb < 40)
                {
                    // s = √|f(x) + 4*b|
                    double underRoot = Math.Abs(fx + 4 * b);
                    result = Math.Sqrt(underRoot);
                }
                else
                {
                    // s = b*f(x)²
                    result = b * fx * fx;
                }

                return true;
            }
            catch
            {
                // Любое исключение при вычислении = ошибка
                return false;
            }
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

                if (!F2Calculate(x, b, out double s, out string funcName))
                {
                    throw new InvalidOperationException(
                        string.IsNullOrEmpty(funcName)
                            ? "Выберите функцию f(x)!"
                            : $"Ошибка вычисления для {funcName}");
                }

                double xb = x * b;
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