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
        public double Ri;
        //double[] RI = new double[] { };
        public double R0= 0.020;
        public double Ki=0;
        public double f;
        public int n;
        double H;
        public double A;
        //double[] Di = new double[] { 0.0240, 0.02574, 0.02750, 0.02925, 0.031, 0.03275, 0.0345, 0.03625, 0.038 };
        int[] KI = new int[] { 36, 36, 36, 36, 24, 32, 32, 32, 32 };
        double pr_sum = 0;
        double max;
    private void Start_click(object sender, RoutedEventArgs e)
        {
            
            Start_page start_page = new Start_page();
            start_page.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (k_t.Text == "" || k_s.Text == "" || v_r.Text == "" || p_p.Text == "" || o_n.Text == "" || Ri_t.Text == "")
            {
                MessageBox.Show("testerror");
            }
            else
            {

                f = Convert.ToDouble(k_t.Text);
                n = Convert.ToInt32(k_s.Text);
                R0 = Convert.ToDouble(v_r.Text);
                a = Convert.ToDouble(p_p.Text);
                q = Convert.ToDouble(o_n.Text);
                Ri = Convert.ToDouble(Ri_t.Text);
                H = Convert.ToDouble(H_t.Text);


                if (Convert.ToDouble(n2_4.Text) <= (Convert.ToDouble(n1_4.Text)) ||
                  (Convert.ToDouble(n3_4.Text) <= (Convert.ToDouble(n2_4.Text)) ||
                  (Convert.ToDouble(n4_4.Text) <= (Convert.ToDouble(n3_4.Text)) ||
                  (Convert.ToDouble(n5_4.Text) <= (Convert.ToDouble(n4_4.Text)) ||
                  (Convert.ToDouble(n6_4.Text) <= (Convert.ToDouble(n5_4.Text)) ||
                  (Convert.ToDouble(n7_4.Text) <= (Convert.ToDouble(n6_4.Text)) ||
                  (Convert.ToDouble(n8_4.Text) <= (Convert.ToDouble(n7_4.Text)) ||
                  (Convert.ToDouble(n9_4.Text) <= (Convert.ToDouble(n8_4.Text))))))))))
                {
                    MessageBox.Show("testerror");
                }
                else
                {
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
                        List<double> KI_list_2 = new List<double> { };
                        List<double> DI_list_2 = new List<double> { };
                        List<double> RI_list_2 = new List<double> { };
                        KI_list_2.Add(Convert.ToDouble(n1_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n2_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n3_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n4_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n5_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n6_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n7_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n8_2.Text));
                        KI_list_2.Add(Convert.ToDouble(n9_2.Text));
                        DI_list_2.Add(Convert.ToDouble(n1_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n2_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n3_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n4_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n5_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n6_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n7_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n8_3.Text));
                        DI_list_2.Add(Convert.ToDouble(n9_3.Text));
                        RI_list_2.Add(Convert.ToDouble(n1_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n2_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n3_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n4_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n5_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n6_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n7_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n8_4.Text));
                        RI_list_2.Add(Convert.ToDouble(n9_4.Text));
                        double[] KI_2 = KI_list_2.ToArray<double>();
                        double[] DI_2 = DI_list_2.ToArray<double>();
                        double[] RI_2 = RI_list_2.ToArray<double>();
                        List<double> RI_list = new List<double> { };
                        List<double> YI_list = new List<double> { };
                        List<double> VI_list = new List<double> { };
                        List<double> QI_list = new List<double> { };
                        List<double> AI_list = new List<double> { };
                        List<double> Sum_list = new List<double> { };
                        
                        RI_list.Add(Ri);
                        while (Ri < Convert.ToDouble(n9_4.Text))
                        {

                            Ri = Ri + H;
                            RI_list.Add(Ri);
                            //MessageBox.Show(Convert.ToString(Ri));

                        }
                        double[] RI = RI_list.ToArray<double>();
                        for (int i = 0; i <= RI.Length - 1; i++)
                        {
                            yi = 2 * a * (RI[i] - R0);
                            YI_list.Add(yi);
                            //MessageBox.Show(Convert.ToString(RI[i]));

                        }
                        double[] YI = YI_list.ToArray<double>();
                        for (int i = 0; i <= YI.Length - 1; i++)
                        {
                            Vi = Math.Pow((1 + Math.Pow(YI[i], 2)), -(1 / 2));
                            VI_list.Add(Vi);
                        }
                        double[] VI = VI_list.ToArray<double>();
                        for (int i = 0; i <= n-1; i++)
                        {
                            pr_sum = pr_sum + (KI[i] * di * Math.Pow(VI[i], 2));

                        }
                        for (int i = 0; i <= VI.Length - 1; i++)
                        {
                            qi = (di * VI[i] * q) / pr_sum;
                            QI_list.Add(qi);
                        }
                        double[] QI = QI_list.ToArray<double>();
                        for (int i = 0; i <= QI.Length - 1; i++)
                        {
                            A = A = 2 * 3.141593 * f * QI[i] * RI[i];
                            AI_list.Add(A);
                        }
                        double[] AI = AI_list.ToArray<double>();
                        max = AI.Max();
                        MessageBox.Show(Convert.ToString(max));

                    }
                    /*if (n_2.IsChecked == true)
                    {
                        List<double> KI_list = new List<double> { };
                        List<double> DI_list = new List<double> { };
                        List<double> RI_list = new List<double> { };
                        KI_list.Add(Convert.ToDouble(n1_2.Text));
                        KI_list.Add(Convert.ToDouble(n2_2.Text));
                        KI_list.Add(Convert.ToDouble(n3_2.Text));
                        KI_list.Add(Convert.ToDouble(n4_2.Text));
                        KI_list.Add(Convert.ToDouble(n5_2.Text));
                        KI_list.Add(Convert.ToDouble(n6_2.Text));
                        KI_list.Add(Convert.ToDouble(n7_2.Text));
                        KI_list.Add(Convert.ToDouble(n8_2.Text));
                        KI_list.Add(Convert.ToDouble(n9_2.Text));
                        DI_list.Add(Convert.ToDouble(n1_3.Text));
                        DI_list.Add(Convert.ToDouble(n2_3.Text));
                        DI_list.Add(Convert.ToDouble(n3_3.Text));
                        DI_list.Add(Convert.ToDouble(n4_3.Text));
                        DI_list.Add(Convert.ToDouble(n5_3.Text));
                        DI_list.Add(Convert.ToDouble(n6_3.Text));
                        DI_list.Add(Convert.ToDouble(n7_3.Text));
                        DI_list.Add(Convert.ToDouble(n8_3.Text));
                        DI_list.Add(Convert.ToDouble(n9_3.Text));
                        RI_list.Add(Convert.ToDouble(n1_4.Text));
                        RI_list.Add(Convert.ToDouble(n2_4.Text));
                        RI_list.Add(Convert.ToDouble(n3_4.Text));
                        RI_list.Add(Convert.ToDouble(n4_4.Text));
                        RI_list.Add(Convert.ToDouble(n5_4.Text));
                        RI_list.Add(Convert.ToDouble(n6_4.Text));
                        RI_list.Add(Convert.ToDouble(n7_4.Text));
                        RI_list.Add(Convert.ToDouble(n8_4.Text));
                        RI_list.Add(Convert.ToDouble(n9_4.Text));
                        double[] KI = KI_list.ToArray<double>();
                        double[] DI = DI_list.ToArray<double>();
                        double[] RI = RI_list.ToArray<double>();
                        for (int i = 0; i <= n - 1; i++)
                        {
                            yi = 2 * a * (RI[i] - R0);
                        }

                        Vi = Math.Pow((1 + Math.Pow(yi, 2)), -(1 / 2));
                        for (int i = 0; i <= n - 1; i++)
                        {
                            pr_sum = pr_sum + (KI[i] * DI[i] * Math.Pow(Vi, 2));
                        }
                        for (int i = 0; i <= n - 1; i++)
                        {
                            qi = (DI[i] * Vi * q) / pr_sum;
                        }
                        A = 2 * 3.141593 * f * qi * Ri;
                        MessageBox.Show(Convert.ToString(qi));
                        MessageBox.Show(Convert.ToString(A));
                    }*/
                }
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

        private void k_s_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(k_s.Text=="1")
            {
                n2_1.Visibility = Visibility.Hidden;
                n2_2.Visibility = Visibility.Hidden;
                n2_3.Visibility = Visibility.Hidden;
                n2_4.Visibility = Visibility.Hidden;
                n3_1.Visibility = Visibility.Hidden;
                n3_2.Visibility = Visibility.Hidden;
                n3_3.Visibility = Visibility.Hidden;
                n3_4.Visibility = Visibility.Hidden;
                n4_1.Visibility = Visibility.Hidden;
                n4_2.Visibility = Visibility.Hidden;
                n4_3.Visibility = Visibility.Hidden;
                n4_4.Visibility = Visibility.Hidden;
                n5_1.Visibility = Visibility.Hidden;
                n5_2.Visibility = Visibility.Hidden;
                n5_3.Visibility = Visibility.Hidden;
                n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;
                n6_2.Visibility = Visibility.Hidden;
                n6_3.Visibility = Visibility.Hidden;
                n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;
                n7_2.Visibility = Visibility.Hidden;
                n7_3.Visibility = Visibility.Hidden;
                n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="2")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Hidden;
                n3_2.Visibility = Visibility.Hidden;
                n3_3.Visibility = Visibility.Hidden;
                n3_4.Visibility = Visibility.Hidden;
                n4_1.Visibility = Visibility.Hidden;
                n4_2.Visibility = Visibility.Hidden;
                n4_3.Visibility = Visibility.Hidden;
                n4_4.Visibility = Visibility.Hidden;
                n5_1.Visibility = Visibility.Hidden;
                n5_2.Visibility = Visibility.Hidden;
                n5_3.Visibility = Visibility.Hidden;
                n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;
                n6_2.Visibility = Visibility.Hidden;
                n6_3.Visibility = Visibility.Hidden;
                n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;
                n7_2.Visibility = Visibility.Hidden;
                n7_3.Visibility = Visibility.Hidden;
                n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="3")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Hidden;
                n4_2.Visibility = Visibility.Hidden;
                n4_3.Visibility = Visibility.Hidden;
                n4_4.Visibility = Visibility.Hidden;
                n5_1.Visibility = Visibility.Hidden;
                n5_2.Visibility = Visibility.Hidden;
                n5_3.Visibility = Visibility.Hidden;
                n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;
                n6_2.Visibility = Visibility.Hidden;
                n6_3.Visibility = Visibility.Hidden;
                n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;
                n7_2.Visibility = Visibility.Hidden;
                n7_3.Visibility = Visibility.Hidden;
                n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="4")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;
                n4_2.Visibility = Visibility.Visible;
                n4_3.Visibility = Visibility.Visible;
                n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Hidden;
                n5_2.Visibility = Visibility.Hidden;
                n5_3.Visibility = Visibility.Hidden;
                n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;
                n6_2.Visibility = Visibility.Hidden;
                n6_3.Visibility = Visibility.Hidden;
                n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;
                n7_2.Visibility = Visibility.Hidden;
                n7_3.Visibility = Visibility.Hidden;
                n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="5")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;
                n4_2.Visibility = Visibility.Visible;
                n4_3.Visibility = Visibility.Visible;
                n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;
                n5_2.Visibility = Visibility.Visible;
                n5_3.Visibility = Visibility.Visible;
                n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Hidden;
                n6_2.Visibility = Visibility.Hidden;
                n6_3.Visibility = Visibility.Hidden;
                n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;
                n7_2.Visibility = Visibility.Hidden;
                n7_3.Visibility = Visibility.Hidden;
                n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="6")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;
                n4_2.Visibility = Visibility.Visible;
                n4_3.Visibility = Visibility.Visible;
                n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;
                n5_2.Visibility = Visibility.Visible;
                n5_3.Visibility = Visibility.Visible;
                n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;
                n6_2.Visibility = Visibility.Visible;
                n6_3.Visibility = Visibility.Visible;
                n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Hidden;
                n7_2.Visibility = Visibility.Hidden;
                n7_3.Visibility = Visibility.Hidden;
                n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="7")
            {   
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;
                n4_2.Visibility = Visibility.Visible;
                n4_3.Visibility = Visibility.Visible;
                n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;
                n5_2.Visibility = Visibility.Visible;
                n5_3.Visibility = Visibility.Visible;
                n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;
                n6_2.Visibility = Visibility.Visible;
                n6_3.Visibility = Visibility.Visible;
                n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Visible;
                n7_2.Visibility = Visibility.Visible;
                n7_3.Visibility = Visibility.Visible;
                n7_4.Visibility = Visibility.Visible;
                n8_1.Visibility = Visibility.Hidden;
                n8_2.Visibility = Visibility.Hidden;
                n8_3.Visibility = Visibility.Hidden;
                n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="8")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;
                n4_2.Visibility = Visibility.Visible;
                n4_3.Visibility = Visibility.Visible;
                n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;
                n5_2.Visibility = Visibility.Visible;
                n5_3.Visibility = Visibility.Visible;
                n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;
                n6_2.Visibility = Visibility.Visible;
                n6_3.Visibility = Visibility.Visible;
                n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Visible;
                n7_2.Visibility = Visibility.Visible;
                n7_3.Visibility = Visibility.Visible;
                n7_4.Visibility = Visibility.Visible;
                n8_1.Visibility = Visibility.Visible;
                n8_2.Visibility = Visibility.Visible;
                n8_3.Visibility = Visibility.Visible;
                n8_4.Visibility = Visibility.Visible;
                n9_1.Visibility = Visibility.Hidden;
                n9_2.Visibility = Visibility.Hidden;
                n9_3.Visibility = Visibility.Hidden;
                n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="9")
            {
                n2_1.Visibility = Visibility.Visible;
                n2_2.Visibility = Visibility.Visible;
                n2_3.Visibility = Visibility.Visible;
                n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;
                n3_2.Visibility = Visibility.Visible;
                n3_3.Visibility = Visibility.Visible;
                n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;
                n4_2.Visibility = Visibility.Visible;
                n4_3.Visibility = Visibility.Visible;
                n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;
                n5_2.Visibility = Visibility.Visible;
                n5_3.Visibility = Visibility.Visible;
                n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;
                n6_2.Visibility = Visibility.Visible;
                n6_3.Visibility = Visibility.Visible;
                n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Visible;
                n7_2.Visibility = Visibility.Visible;
                n7_3.Visibility = Visibility.Visible;
                n7_4.Visibility = Visibility.Visible;
                n8_1.Visibility = Visibility.Visible;
                n8_2.Visibility = Visibility.Visible;
                n8_3.Visibility = Visibility.Visible;
                n8_4.Visibility = Visibility.Visible;
                n9_1.Visibility = Visibility.Visible;
                n9_2.Visibility = Visibility.Visible;
                n9_3.Visibility = Visibility.Visible;
                n9_4.Visibility = Visibility.Visible;
            }
        }

        private void n_2_Checked(object sender, RoutedEventArgs e)
        {
           
            Rn_t.Visibility = Visibility.Hidden;
            H_t.Visibility = Visibility.Hidden;
        }

        private void n_1_Checked(object sender, RoutedEventArgs e)
        {
            Ri_t.Visibility = Visibility.Visible;
            Rn_t.Visibility = Visibility.Visible;
            H_t.Visibility = Visibility.Visible;
        }

        private void Ri_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            n1_4.Text = Ri_t.Text;
        }

        private void Rn_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            n9_4.Text = Rn_t.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {   
            using (Table_context db = new Table_context())
            {
                Table_base n1 = new Table_base { t1_1 = Convert.ToDouble(n1_1.Text) };
                
                db.table.Add(n1);
                db.SaveChanges();

                var Table = db.table;
                foreach (Table_base u in Table)
                {
                    MessageBox.Show(Convert.ToString(u.t1_1));
                    db.table.Remove(n1);
                }
            }
        }
    }
}
