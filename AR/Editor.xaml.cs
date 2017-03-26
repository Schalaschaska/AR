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
using System.Windows.Shapes;


namespace AR
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor()
        {
            InitializeComponent();
            

        }
        public double yi=0;//Уравнения профиля коронки
        public double Vi=0;
        public double di= 0.0011;
        public double ai=0;
        public double a=40;
        public double q = 16000.0;
        public double qi = 0;
        public double Ri= 0.020;
        public double R0= 0.020;
        public double Ki=0;
        public double f=0.30;
        public int n=1;
        private void Start_click(object sender, RoutedEventArgs e)
        {
            
            Start_page start_page = new Start_page();
            start_page.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            yi = 2*a * (Ri - R0);
            Vi = Math.Pow((1 + Math.Pow(yi, 2)), -(1 / 2));
            qi = (di * Vi * q) / (36 * di * Math.Pow(Vi, 2));
            a = 2 * 3.141593 * f * qi * Ri;
            MessageBox.Show(Convert.ToString(qi));
            MessageBox.Show(Convert.ToString(a));
        }
    }
}
