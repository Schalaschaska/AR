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
        public double yi;//Уравнения профиля коронки
        public double Vi=0;
        public double di= 0.0011;
        public double ai=0;
        public double a=40.1;
        public double q;
        public double qi = 0;
        public double Ri= 0.0200;
        //double[] RI = new double[] { };
        public double R0= 0.020;
        public double Ki=0;
        public double f;
        public int n;
        double H = 0.0002;
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
            /*f = Convert.ToDouble(k_t.Text);
            n = Convert.ToInt32(k_s.Text);
            R0 = Convert.ToDouble(v_r.Text);
            a = Convert.ToDouble(p_p.Text);
            q = Convert.ToDouble(o_n.Text);*/

            /*NetOffice.WordApi.Application word = new NetOffice.WordApi.Application();
            word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            NetOffice.WordApi.Document newdoc = word.Documents.Add();
            word.Selection.TypeText("Test text "+textBox.Text);//пока только так(
            

            word.Selection.HomeKey(WdUnits.wdLine, WdMovementType.wdExtend);
            word.Selection.Font.Color = WdColor.wdColorAqua;//тистим цвет
            word.Selection.Font.Bold = 1;
            word.Selection.Font.Size = 18;
            string fileExtension = GetDefaultExtension(word);//проверка версии
            object documentFile = string.Format("{0}\\Test{1}", Directory.GetCurrentDirectory(), fileExtension);
            newdoc.SaveAs(documentFile);
            word.Quit();
            word.Dispose();*/
            if (n_1.IsChecked == true)
            {
                List<double> RI_list = new List<double> { };
                List<double> YI_list = new List<double> { };
                List<double> VI_list = new List<double> { };
                List<double> QI_list = new List<double> { };
                List<double> AI_list = new List<double> { };
                List<double> Sum_list = new List<double> { };
                RI_list.Add(Ri);
                while (Ri < 0.038)
                {

                    Ri = Ri + H;
                    RI_list.Add(Ri);
                    //MessageBox.Show(Convert.ToString(Ri));
                    

                }
                double[] RI = RI_list.ToArray<double>();
                for (int i = 0; i <= RI.Length-1; i++)
                {
                    yi = 2 * a * (RI[i] - R0);
                    YI_list.Add(yi);
                    //MessageBox.Show(Convert.ToString(RI[i]));
                    
                }
                double[] YI = YI_list.ToArray<double>();
                for(int i=0;i<=YI.Length-1;i++)
                {
                    Vi= Math.Pow((1 + Math.Pow(YI[i], 2)), -(1 / 2));
                    VI_list.Add(Vi);
                }
                double[] VI = VI_list.ToArray<double>();
                for(int i=0;i<=VI.Length-1;i++)
                {
                    pr_sum = pr_sum + (KI[i] * di * Math.Pow(VI[i], 2));
                   
                }
                for(int i=0;i<=VI.Length-1;i++)
                {
                    qi = (di * VI[i] * q) / pr_sum;
                    QI_list.Add(qi);
                }
                double[] QI = QI_list.ToArray<double>();
                for(int i=0;i<=QI.Length-1;i++)
                {
                    A= A = 2 * 3.141593 * f * QI[i] * RI[i];
                    AI_list.Add(A);
                }
                double[] AI = AI_list.ToArray<double>();

            }
           if(n_2.IsChecked==true)
            {
                yi = 2 * a * (Ri - R0);
                Vi = Math.Pow((1 + Math.Pow(yi, 2)), -(1 / 2));
                for (int i = 0; i <= n - 1; i++)
                {
                    pr_sum = pr_sum + (KI[i] * di * Math.Pow(Vi, 2));
                }

                qi = (di * Vi * q) / pr_sum;
                A = 2 * 3.141593 * f * qi * Ri;
                MessageBox.Show(Convert.ToString(qi));
                MessageBox.Show(Convert.ToString(A));
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
