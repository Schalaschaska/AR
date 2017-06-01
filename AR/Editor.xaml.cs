﻿using System;
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
using System.Data.Entity;
using System.ComponentModel;
using System.Data;

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
        public double di=0;
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
        public double H;
        public double A;
        public int kol_e;
        public double[] RI_rez;
        //double[] Di = new double[] { 0.0240, 0.02574, 0.02750, 0.02925, 0.031, 0.03275, 0.0345, 0.03625, 0.038 };
        int[] KI = new int[] { 36, 36, 36, 36, 24, 32, 32, 32, 32 };
        double pr_sum = 0;
        double max;
        public bool save_flag;
        int[] myArr = new int[] {  };
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


                if(Convert.ToDouble(n2_4.Text) <= (Convert.ToDouble(n1_4.Text)) ||
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
                        
                        RI_list.Add(Convert.ToDouble(n1_4.Text));
                        while (Ri <= Convert.ToDouble(n9_4.Text))
                        {

                            Ri = Ri + H;
                            RI_list.Add(Ri);
                        
                            //kol_e++;
                            //MessageBox.Show(Convert.ToString(Ri));

                        }
                        double[] RI = RI_list.ToArray<double>();
                        
                        for (int i = 0; i <= n - 1; i++)
                        {
                            yi = 2 * a * (RI_2[i] - R0);
                            YI_list.Add(yi);

                        }
                        double[] YI = YI_list.ToArray<double>();

                  


                    for (int i = 0; i <= n - 1; i++)
                        {
                        Vi = Math.Pow((1 + Math.Pow(YI[i], 2)), -(1 / 2));
                        VI_list.Add(Vi);
                        //MessageBox.Show(Convert.ToString(YI[i]));
                        }
                        double[] VI = VI_list.ToArray<double>();
                   

                    for (int i = 0; i <= n-1; i++)
                        {
                            pr_sum = pr_sum + (KI_2[i] * DI_2[i] * Math.Pow(VI[i], 2));

                        }
                    //MessageBox.Show(Convert.ToString(pr_sum));
                        for (int i = 0; i <= VI.Length - 1; i++)
                        {
                            qi = (DI_2[i] * VI[i] * q) / pr_sum;
                            QI_list.Add(qi);
                            
                        }
                        double[] QI = QI_list.ToArray<double>();
                        for (int i = 0; i <= RI.Length - 1; i++)
                        {
                     
                            A = A = 2 * 3.141593 * f * QI[0] * RI[i];
                            AI_list.Add(A);
                           // MessageBox.Show(Convert.ToString(QI[i]));
                        }
                        double[] AI = AI_list.ToArray<double>();
                       max = AI.Max();
                    for(int i=0;i<=RI.Length-1;i++)
                    {
                        MessageBox.Show(Convert.ToString(AI[i]));
                    }
                    max = AI.Max();
                    MessageBox.Show(Convert.ToString(max));
                    using (Table_context db = new Table_context())
                    {

                    }
                        //MessageBox.Show(Convert.ToString(kol_e));
                        /*Array.Copy(RI, RI_rez = new double[RI.Length - 1], RI.Length - 1);*/
                    /*Rezult w = new Rezult();
                    w.ShowDialog();*/

                    
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
                    Table_base n1 = new Table_base { t1_1 = Convert.ToDouble(n1_1.Text), t1_2 = Convert.ToDouble(n1_2.Text), t1_3 = Convert.ToDouble(n1_3.Text), t1_4 = Convert.ToDouble(n1_4.Text),
                    t2_1 = Convert.ToDouble(n2_1.Text), t2_2 = Convert.ToDouble(n2_2.Text), t2_3 = Convert.ToDouble(n2_3.Text), t2_4 = Convert.ToDouble(n2_4.Text),
                    t3_1 = Convert.ToDouble(n3_1.Text), t3_2 = Convert.ToDouble(n3_2.Text), t3_3 = Convert.ToDouble(n3_3.Text), t3_4 = Convert.ToDouble(n3_4.Text),
                    t4_1 = Convert.ToDouble(n4_1.Text), t4_2 = Convert.ToDouble(n4_2.Text), t4_3 = Convert.ToDouble(n4_3.Text), t4_4 = Convert.ToDouble(n4_4.Text),
                    t5_1 = Convert.ToDouble(n5_1.Text), t5_2 = Convert.ToDouble(n5_2.Text), t5_3 = Convert.ToDouble(n5_3.Text), t5_4 = Convert.ToDouble(n5_4.Text),
                    t6_1 = Convert.ToDouble(n6_1.Text), t6_2 = Convert.ToDouble(n6_2.Text), t6_3 = Convert.ToDouble(n6_3.Text), t6_4 = Convert.ToDouble(n6_4.Text),
                    t7_1 = Convert.ToDouble(n7_1.Text), t7_2 = Convert.ToDouble(n7_2.Text), t7_3 = Convert.ToDouble(n7_3.Text), t7_4 = Convert.ToDouble(n7_4.Text),
                    t8_1 = Convert.ToDouble(n8_1.Text), t8_2 = Convert.ToDouble(n8_2.Text), t8_3 = Convert.ToDouble(n8_3.Text), t8_4 = Convert.ToDouble(n8_4.Text),
                    t9_1 = Convert.ToDouble(n9_1.Text), t9_2 = Convert.ToDouble(n9_2.Text), t9_3 = Convert.ToDouble(n9_3.Text), t9_4 = Convert.ToDouble(n9_4.Text),
                    k_t_base = Convert.ToDouble(k_t.Text), k_s_base = Convert.ToDouble(k_s.Text), o_s_base = Convert.ToDouble(o_n.Text), p_p_base = Convert.ToDouble(p_p.Text), v_r_base = Convert.ToDouble(v_r.Text),
                    ri_base = Convert.ToDouble(Ri_t.Text), rn_base = Convert.ToDouble(Rn_t.Text), h_base = Convert.ToDouble(H_t.Text)};
                db.table.Add(n1);
                db.SaveChanges();
                if (save_flag == true)
                {
                    Table_base p1 = db.table.FirstOrDefault();
                    
                    
                    if (p1 != null)
                    {
                        db.table.Remove(p1);
                        db.SaveChanges();
                    }
                }
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            using (Table_context db = new Table_context())
            {
                
                var Table = db.table;
                foreach (Table_base u in Table)
                {

                    n1_1.Text = Convert.ToString(u.t1_1);n1_2.Text = Convert.ToString(u.t1_2);n1_3.Text = Convert.ToString(u.t1_3);n1_4.Text = Convert.ToString(u.t1_4);
                    n2_1.Text = Convert.ToString(u.t2_1);n2_2.Text = Convert.ToString(u.t2_2);n2_3.Text = Convert.ToString(u.t2_3);n2_4.Text = Convert.ToString(u.t2_4);
                    n3_1.Text = Convert.ToString(u.t3_1);n3_2.Text = Convert.ToString(u.t3_2);n3_3.Text = Convert.ToString(u.t3_3);n3_4.Text = Convert.ToString(u.t3_4);
                    n4_1.Text = Convert.ToString(u.t4_1); n4_2.Text = Convert.ToString(u.t4_2); n4_3.Text = Convert.ToString(u.t4_3); n4_4.Text = Convert.ToString(u.t4_4);
                    n5_1.Text = Convert.ToString(u.t5_1); n5_2.Text = Convert.ToString(u.t5_2); n5_3.Text = Convert.ToString(u.t5_3); n5_4.Text = Convert.ToString(u.t5_4);
                    n6_1.Text = Convert.ToString(u.t6_1); n6_2.Text = Convert.ToString(u.t6_2); n6_3.Text = Convert.ToString(u.t6_3); n6_4.Text = Convert.ToString(u.t6_4);
                    n7_1.Text = Convert.ToString(u.t7_1); n7_2.Text = Convert.ToString(u.t7_2); n7_3.Text = Convert.ToString(u.t7_3); n7_4.Text = Convert.ToString(u.t7_4);
                    n8_1.Text = Convert.ToString(u.t8_1); n8_2.Text = Convert.ToString(u.t8_2); n8_3.Text = Convert.ToString(u.t8_3); n8_4.Text = Convert.ToString(u.t8_4);
                    n9_1.Text = Convert.ToString(u.t9_1); n9_2.Text = Convert.ToString(u.t9_2); n9_3.Text = Convert.ToString(u.t9_3); n9_4.Text = Convert.ToString(u.t9_4);
                    Ri_t.Text = Convert.ToString(u.ri_base);Rn_t.Text = Convert.ToString(u.rn_base);H_t.Text = Convert.ToString(u.h_base);k_t.Text = Convert.ToString(u.k_t_base);
                    k_s.Text = Convert.ToString(u.k_s_base);o_n.Text = Convert.ToString(u.o_s_base);p_p.Text = Convert.ToString(u.p_p_base);v_r.Text=Convert.ToString(u.v_r_base) ;

                }
                save_flag = true;
              

            }
                
            
        }

      
        /*
private void grid_Loaded(object sender, RoutedEventArgs e)
{
   List<Table_content> result = new List<Table_content>(8);

   result.Add(new Table_content(1, 36, 0.0011, 0.0240));
   result.Add(new Table_content(2, 36, 0.0011, 0.02574));
   result.Add(new Table_content(3, 36, 0.0011, 0.2750));
   result.Add(new Table_content(4, 36, 0.0011, 0.02925));
   result.Add(new Table_content(5, 24, 0.0011, 0.031));
   result.Add(new Table_content(6, 32, 0.0011, 0.03275));
   result.Add(new Table_content(7, 32, 0.0011, 0.0345));
   result.Add(new Table_content(8, 32, 0.0011, 0.03625));
   result.Add(new Table_content(9, 32, 0.0011, 0.038));
   grid.ItemsSource = result;
   int parse = grid.SelectedIndex;
   DataRowView rowView = grid.SelectedValue as DataRowView;


}

private void Button_Click_3(object sender, RoutedEventArgs e)
{
   Table_cell cell = new Table_cell();
   string a;

   a =Convert.ToString(cell.FindCell(0, 3, grid));
   a = a.Substring(a.IndexOf(':') + 1);
   MessageBox.Show(a);



}*/

    }
}
