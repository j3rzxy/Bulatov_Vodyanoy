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

                if (string.IsNullOrWhiteSpace(TBX.Text))
                {
                    throw new ArgumentException("Заполните поле X!");
                }

                TBX.Text = TBX.Text.Replace('.', ',');

                if (!double.TryParse(TBX.Text, out x))
                {
                    throw new FormatException("Поле X должно содержать число!");
                }
                if (string.IsNullOrWhiteSpace(TBY.Text))
                {
                    throw new ArgumentException("Заполните поле Y!");
                }

                TBX.Text = TBX.Text.Replace('.', ',');


                if (!double.TryParse(TBY.Text, out y))
                {
                    throw new FormatException("Поле Y должно содержать число!");
                }
                if (string.IsNullOrWhiteSpace(TBZ.Text))
                {
                    throw new ArgumentException("Заполните поле Z!");
                }

                TBX.Text = TBX.Text.Replace('.', ',');

                if (!double.TryParse(TBZ.Text, out z))
                {
                    throw new FormatException("Поле Z должно содержать число!");
                }

                x = double.Parse(TBX.Text);
                y = double.Parse(TBY.Text);
                z = double.Parse(TBZ.Text);

                if (x < -1 || x > 1)
                {
                    throw new ArgumentException("Согласно области значений вашей функции X должен быть в диапозоне [-1, 1]");
                }


                double absXY = Math.Abs(x - y);

                double numerator = x + 3 * absXY + x * x;

                double denominator = absXY * z + x * x;

                if (Math.Abs(denominator) < 1e-10)
                {
                    throw new ArgumentException("Знаменатель не может быть равен нулю");
                }

                double gamma = 5 * Math.Atan(x) - 0.25 * Math.Acos(x) * (numerator / denominator);

                TBResult.Text = gamma.ToString();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
