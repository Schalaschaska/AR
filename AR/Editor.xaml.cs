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
using System.IO;
using System.Text.RegularExpressions;
using NetOffice.WordApi.Enums;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using System.Globalization;
using System.Threading;



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
        public double a;
        public double q;
        public double qi = 0;
        public double Ri= 0.020;
        public double R0;
        public double Ki=0;
        public double f;
        public int n;
        public double A;
        double[] Di = new double[] { 0.0240, 0.02574, 0.02750, 0.02925, 0.031, 0.03275, 0.0345, 0.03625, 0.038 };
        int[] KI = new int[] { 36, 36, 36, 36, 24, 32, 32, 32, 32 };
        double pr_sum = 0;
        double zn;
    private void Start_click(object sender, RoutedEventArgs e)
        {
            
            Start_page start_page = new Start_page();
            start_page.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            f = Convert.ToDouble(k_t.Text);
            n = Convert.ToInt32(k_s.Text);
            R0 = Convert.ToDouble(v_r.Text);
            a = Convert.ToDouble(p_p.Text);
            q = Convert.ToDouble(o_n.Text);

            /*NetOffice.WordApi.Application word = new NetOffice.WordApi.Application();
            word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            NetOffice.WordApi.Document newdoc = word.Documents.Add();
            word.Selection.TypeText("Test text "+textBox.Text);//пока только так(
            

            word.Selection.HomeKey(WdUnits.wdLine, WdMovementType.wdExtend);
            word.Selection.Font.Color = WdColor.wdColorAqua;
            word.Selection.Font.Bold = 1;
            word.Selection.Font.Size = 18;
            string fileExtension = GetDefaultExtension(word);
            object documentFile = string.Format("{0}\\Test{1}", Directory.GetCurrentDirectory(), fileExtension);
            newdoc.SaveAs(documentFile);
            word.Quit();
            word.Dispose();*/
            if (n_1.IsChecked == true)
            {
                yi = 2 * a * (Ri - R0);
                Vi = Math.Pow((1 + Math.Pow(yi, 2)), -(1 / 2)); 
                for(int i=0;i<=n-1;i++)
                {
                    pr_sum = pr_sum + (KI[i] * di * Math.Pow(Vi, 2));
                }
                
                qi = (di * Vi * q) / pr_sum;
                A = 2 * 3.141593 * f * qi * Ri;
                MessageBox.Show(Convert.ToString(qi));
                MessageBox.Show(Convert.ToString(A));
            }
           if(n_2.IsChecked==true)
            {

            }
        }
        #region Helder

        private static string GetDefaultExtension(NetOffice.WordApi.Application application)
        {
            double version = Convert.ToDouble(application.Version, CultureInfo.InvariantCulture);
            if (version >= 12.00)
                return ".docx";
            else
                return ".doc";
        }

        #endregion
    }
}
