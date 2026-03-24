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
        /// <summary>
        /// Метод, вызываемый при нажатии на кнопку "Вычислить"
        /// </summary>
        /// <param name="sender">Базовый параметр</param>
        /// <param name="e">Базовый параметр</param>
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

                Functions2.FunctionType selectedType;
                if (RadioSinh.IsChecked == true)
                    selectedType = Functions2.FunctionType.Sinh;
                else if (RadioSquare.IsChecked == true)
                    selectedType = Functions2.FunctionType.Square;
                else if (RadioExp.IsChecked == true)
                    selectedType = Functions2.FunctionType.Exp;
                else
                {
                    MessageBox.Show("Выберите функцию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double result;
                string funcName;
                if (Functions2.F2Calculate(x, b, selectedType, out result, out funcName))
                {
                    TBResult.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Ошибка вычисления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

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
        /// <summary>
        /// Статический класс для отдельных функций.
        /// </summary>
        public static class Functions2
        {
            /// <summary>
            /// Перечисление для выбора функции.
            /// </summary>
            public enum FunctionType { Sinh, Square, Exp }
            /// <summary>
            /// Метод расчета заданной функции.
            /// </summary>
            /// <param name="x">Переменная x</param>
            /// <param name="b">Переменная b</param>
            /// <param name="funcType">Переменная, передающая варианты функций из перечисления</param>
            /// <param name="result">Результат вычислений</param>
            /// <param name="selectedFunction">Параметр, хранящий выбранную функцию для расчета</param>
            /// <returns></returns>
            public static bool F2Calculate(double x, double b, FunctionType funcType, out double result, out string selectedFunction)
            {
                result = 0;
                selectedFunction = null;

                try
                {
                    // 1. Вычисляем f(x) в зависимости от типа функции
                    double fx;
                    if (funcType == FunctionType.Sinh)
                    {
                        fx = Math.Sinh(x);
                        selectedFunction = "sh(x)";
                    }
                    else if (funcType == FunctionType.Square)
                    {
                        fx = x * x;
                        selectedFunction = "x^2";
                    }
                    else if (funcType == FunctionType.Exp)
                    {
                        fx = Math.Exp(x);
                        selectedFunction = "e^x";
                    }
                    else
                    {
                        return false; // Неизвестная функция
                    }

                    // 2. Вычисляем произведение
                    double xb = x * b;

                    // 3. Выбираем формулу результата
                    if (xb > 1 && xb < 10)
                    {
                        result = Math.Exp(fx);           // s = e^f
                    }
                    else if (xb > 12 && xb < 40)
                    {
                        result = Math.Sqrt(Math.Abs(fx + 4 * b)); // s = √|f+4b|
                    }
                    else
                    {
                        result = b * fx * fx;            // s = b*f²
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Метод для очистки текстовых полей.
        /// </summary>
        /// <param name="sender">Базовый параметр</param>
        /// <param name="e">Базовый параметр</param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TBX.Clear();
            TBB.Clear();
            TBResult.Clear();
            TBX.Focus();
        }
    }
}