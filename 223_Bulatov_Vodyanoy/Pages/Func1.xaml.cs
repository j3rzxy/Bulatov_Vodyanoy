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

namespace _223_Bulatov_Vodyanoy.Pages
{
    /// <summary>
    /// Логика взаимодействия для Func1.xaml
    /// </summary>
    public partial class Func1 : Page
    {
        public Func1()
        {
            InitializeComponent();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TBX.Clear();
            TBY.Clear();
            TBZ.Clear();
            TBResult.Clear();
            TBX.Focus();
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            double x, y, z;

            try
            {
                TBResult.Clear();

                // === Валидация X ===
                if (string.IsNullOrWhiteSpace(TBX.Text))
                {
                    throw new ArgumentException("Заполните поле X!");
                }
                string xText = TBX.Text.Replace('.', ',');
                if (!double.TryParse(xText, out x))
                {
                    throw new FormatException("Поле X должно содержать число!");
                }

                // === Валидация Y ===
                if (string.IsNullOrWhiteSpace(TBY.Text))
                {
                    throw new ArgumentException("Заполните поле Y!");
                }
                string yText = TBY.Text.Replace('.', ','); 
                if (!double.TryParse(yText, out y))
                {
                    throw new FormatException("Поле Y должно содержать число!");
                }

                // === Валидация Z ===
                if (string.IsNullOrWhiteSpace(TBZ.Text))
                {
                    throw new ArgumentException("Заполните поле Z!");
                }
                string zText = TBZ.Text.Replace('.', ','); 
                if (!double.TryParse(zText, out z))
                {
                    throw new FormatException("Поле Z должно содержать число!");
                }

                // === Вычисление ===
                double result;
                if (Functions.F1Calculate(x, y, z, out result))
                {
                    TBResult.Text = result.ToString(); 
                }
                else
                {
                    MessageBox.Show("Ошибка вычисления функции. Проверьте входные данные.",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка формата", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка: {ex.Message}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    public static class Functions
    {
        public static bool F1Calculate(double x, double y, double z, out double result)
        {

            try
            {
                if (x < -1 || x > 1)
                {
                    throw new ArgumentException("Согласно области значений вашей функции X должен быть в диапазоне [-1, 1]");
                }

                double absXY = Math.Abs(x - y);
                double numerator = x + 3 * absXY + x * x;
                double denominator = absXY * z + x * x;

                if (Math.Abs(denominator) < 1e-10)
                {
                    throw new ArgumentException("Знаменатель не может быть равен нулю");
                }

                result = 5 * Math.Atan(x) - 0.25 * Math.Acos(x) * (numerator / denominator);
                return true;
            }
            catch
            {
                result = 0;
                return false;
            }
        }
    }
}
