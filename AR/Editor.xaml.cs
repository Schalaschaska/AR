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
using System.Data.Entity;
using System.ComponentModel;
using System.Data;
using NetOffice;


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
        public double R0= 0.020;
        public double Ki=0;
        public double f;//k трегия
        public int n;//слой
        public int pr_n = 0;
        public double pr_sum = 0;//сумма_1
        public double H;//нагрузка
        public double A;
        public int kol_e;
        public double[] RI_rez;
        int[] NI = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };//массив слоёв
        double pr_sum_2 = 0;//сумма_2
        double max;
        public bool save_flag;
        int[] myArr = new int[] {  };
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(k_t.Text) || string.IsNullOrWhiteSpace(k_s.Text) || string.IsNullOrWhiteSpace(o_n.Text) || string.IsNullOrWhiteSpace(p_p.Text) ||
                   string.IsNullOrWhiteSpace(v_r.Text) || string.IsNullOrWhiteSpace(Ri_t.Text) || string.IsNullOrWhiteSpace(Rn_t.Text) || string.IsNullOrWhiteSpace(H_t.Text) ||
                   string.IsNullOrWhiteSpace(n1_1.Text) || string.IsNullOrWhiteSpace(n1_2.Text) || string.IsNullOrWhiteSpace(n1_3.Text) || string.IsNullOrWhiteSpace(n1_4.Text) ||
                   string.IsNullOrWhiteSpace(n2_1.Text) || string.IsNullOrWhiteSpace(n2_2.Text) || string.IsNullOrWhiteSpace(n2_3.Text) || string.IsNullOrWhiteSpace(n2_4.Text) ||
                   string.IsNullOrWhiteSpace(n3_1.Text) || string.IsNullOrWhiteSpace(n3_2.Text) || string.IsNullOrWhiteSpace(n3_3.Text) || string.IsNullOrWhiteSpace(n3_4.Text) ||
                   string.IsNullOrWhiteSpace(n4_1.Text) || string.IsNullOrWhiteSpace(n4_2.Text) || string.IsNullOrWhiteSpace(n4_3.Text) || string.IsNullOrWhiteSpace(n4_4.Text) ||
                   string.IsNullOrWhiteSpace(n5_1.Text) || string.IsNullOrWhiteSpace(n5_2.Text) || string.IsNullOrWhiteSpace(n5_3.Text) || string.IsNullOrWhiteSpace(n5_4.Text) ||
                   string.IsNullOrWhiteSpace(n6_1.Text) || string.IsNullOrWhiteSpace(n6_2.Text) || string.IsNullOrWhiteSpace(n6_3.Text) || string.IsNullOrWhiteSpace(n6_4.Text) ||
                   string.IsNullOrWhiteSpace(n7_1.Text) || string.IsNullOrWhiteSpace(n7_2.Text) || string.IsNullOrWhiteSpace(n7_3.Text) || string.IsNullOrWhiteSpace(n7_4.Text) ||
                   string.IsNullOrWhiteSpace(n8_1.Text) || string.IsNullOrWhiteSpace(n8_2.Text) || string.IsNullOrWhiteSpace(n8_3.Text) || string.IsNullOrWhiteSpace(n8_4.Text) ||
                   string.IsNullOrWhiteSpace(n9_1.Text) || string.IsNullOrWhiteSpace(n9_2.Text) || string.IsNullOrWhiteSpace(n9_3.Text) || string.IsNullOrWhiteSpace(n9_4.Text)
                   )
            { MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information); }
            else
            {
                Regex X = new Regex(@"^\d*(\,\d+)?$");

                if (X.IsMatch(n1_1.Text) && (X.IsMatch(n1_2.Text)) && (X.IsMatch(n1_3.Text)) && (X.IsMatch(n1_4.Text)) && (X.IsMatch(n2_1.Text)) && (X.IsMatch(n2_2.Text)) &&
                    (X.IsMatch(n2_3.Text)) && (X.IsMatch(n2_4.Text)) && (X.IsMatch(n3_1.Text)) && (X.IsMatch(n3_2.Text)) && (X.IsMatch(n3_3.Text)) && (X.IsMatch(n3_4.Text)) &&
                    (X.IsMatch(n4_1.Text)) && (X.IsMatch(n4_2.Text)) && (X.IsMatch(n4_3.Text)) && (X.IsMatch(n4_4.Text)) && (X.IsMatch(n5_1.Text)) && (X.IsMatch(n5_2.Text)) &&
                    (X.IsMatch(n5_3.Text)) && (X.IsMatch(n5_4.Text)) && (X.IsMatch(n6_1.Text)) && (X.IsMatch(n6_2.Text)) && (X.IsMatch(n6_3.Text)) && (X.IsMatch(n6_4.Text)) &&
                    (X.IsMatch(n7_1.Text)) && (X.IsMatch(n7_2.Text)) && (X.IsMatch(n7_3.Text)) && (X.IsMatch(n7_4.Text)) && (X.IsMatch(n8_1.Text)) && (X.IsMatch(n8_2.Text)) &&
                    (X.IsMatch(n8_3.Text)) && (X.IsMatch(n8_4.Text)) && (X.IsMatch(n9_1.Text)) && (X.IsMatch(n9_2.Text)) && (X.IsMatch(n9_3.Text)) && (X.IsMatch(n9_4.Text)) &&
                    (X.IsMatch(k_t.Text)) && (X.IsMatch(k_s.Text)) && (X.IsMatch(o_n.Text)) && (X.IsMatch(p_p.Text)) && (X.IsMatch(v_r.Text)) && (X.IsMatch(Ri_t.Text)) && (X.IsMatch(Rn_t.Text)) && (X.IsMatch(H_t.Text)))
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
                        MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
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
                        RI_list.Add(Convert.ToDouble(n1_4.Text));
                        while (Ri <= Convert.ToDouble(n9_4.Text))
                        {
                            Ri = Ri + H;
                            RI_list.Add(Ri);
                            kol_e++;

                        }
                        double[] RI = RI_list.ToArray<double>();

                        for (int i = 0; i <= RI.Length - 1; i++)
                        {
                            yi = 2 * a * (RI[i] - R0);
                            YI_list.Add(yi);

                        }
                        double[] YI = YI_list.ToArray<double>();
                        for (int i = 0; i <= RI.Length - 1; i++)
                        {
                            Vi = 1 / (Math.Sqrt(1 + YI[i] * YI[i]));
                            VI_list.Add(Vi);
                        }
                        double[] VI = VI_list.ToArray<double>();


                        for (int i = 0; i <= n - 1; i++)
                        {
                            pr_sum = pr_sum + (KI_2[i] * DI_2[i] * Math.Pow(VI[i], 2));
                            pr_n++;
                        }

                        pr_sum_2 = DI_2[pr_n - 1] / pr_sum;
                        for (int i = 0; i <= RI.Length - 1; i++)
                        {
                            qi = pr_sum_2 * q * VI[i];
                            QI_list.Add(qi);
                        }
                        double[] QI = QI_list.ToArray<double>();

                        for (int i = 0; i <= RI.Length - 1; i++)
                        {
                            A = 2 * 3.141592653589793238462643383279 * f * QI[i] * RI[i];
                            AI_list.Add(A);
                        }
                        double[] AI = AI_list.ToArray<double>();
                        max = AI.Max();
                        string date_time = DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss");
                        string date_time_2 = DateTime.Now.ToString("dd MMMM yyyy HH-mm-ss");
                        NetOffice.WordApi.Application word = new NetOffice.WordApi.Application();
                        word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                        NetOffice.WordApi.Document newdoc = word.Documents.Add();
                        word.Selection.TypeText(date_time);//пока только так(
                        word.Selection.TypeParagraph();
                        word.Selection.TypeText("Исходные данные");
                        word.Selection.TypeParagraph();
                        NetOffice.WordApi.Table table = newdoc.Tables.Add(word.Selection.Range, n, 4);
                        table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 1; j <= 1; j++)
                            {
                                table.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(NI[i]));

                            }
                        }
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 2; j <= 2; j++)
                            {
                                table.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(KI_2[i]));

                            }
                        }
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 3; j <= 3; j++)
                            {
                                table.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(DI_2[i]));

                            }
                        }
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 4; j <= 4; j++)
                            {
                                table.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(RI_2[i]));

                            }
                        }
                        word.Selection.EndKey(6);
                        word.Selection.TypeParagraph();
                        word.Selection.TypeParagraph();
                        word.Selection.TypeText("Таблица результатов");
                        NetOffice.WordApi.Table table_2 = newdoc.Tables.Add(word.Selection.Range, kol_e, 3);
                        table_2.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        table_2.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        for (int i = 0; i < kol_e; i++)
                        {
                            for (int j = 1; j <= 1; j++)
                            {
                                table_2.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(RI[i]));
                            }
                        }
                        for (int i = 0; i < kol_e; i++)
                        {
                            for (int j = 2; j <= 2; j++)
                            {
                                table_2.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(QI[i]));

                            }
                        }
                        for (int i = 0; i < kol_e; i++)
                        {
                            for (int j = 3; j <= 3; j++)
                            {
                                table_2.Cell(i + 1, j).Select();
                                word.Selection.TypeText(Convert.ToString(AI[i]));

                            }
                        }
                        int indexMax = Array.IndexOf(AI, max);
                        word.Selection.EndKey(6);
                        word.Selection.TypeParagraph();
                        word.Selection.TypeText("Максимальное значение А = "+Convert.ToString(max)+" при значении "+RI[indexMax]+" RI");
                        string fileExtension = GetDefaultExtension(word);//проверка версии
                        object documentFile = string.Format("{0}\\" + date_time_2 + "{1}", Directory.GetCurrentDirectory(), fileExtension);
                        newdoc.SaveAs(documentFile);
                        word.Quit();
                        word.Dispose();
                        Array.Clear(RI, 0, RI.Length);
                        Array.Clear(YI, 0, YI.Length);
                        Array.Clear(VI, 0, VI.Length);
                        Array.Clear(QI, 0, QI.Length);
                        Array.Clear(AI, 0, AI.Length);
                        Array.Clear(KI_2, 0, KI_2.Length);
                        Array.Clear(RI_2, 0, RI_2.Length);
                        pr_n = 0;
                        pr_sum = 0;
                        pr_sum_2 = 0;
                        kol_e = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }//расчёт
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
                n2_1.Visibility = Visibility.Hidden;n2_2.Visibility = Visibility.Hidden;n2_3.Visibility = Visibility.Hidden;n2_4.Visibility = Visibility.Hidden;
                n3_1.Visibility = Visibility.Hidden;n3_2.Visibility = Visibility.Hidden;n3_3.Visibility = Visibility.Hidden;n3_4.Visibility = Visibility.Hidden;
                n4_1.Visibility = Visibility.Hidden;n4_2.Visibility = Visibility.Hidden;n4_3.Visibility = Visibility.Hidden;n4_4.Visibility = Visibility.Hidden;
                n5_1.Visibility = Visibility.Hidden;n5_2.Visibility = Visibility.Hidden;n5_3.Visibility = Visibility.Hidden;n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;n6_2.Visibility = Visibility.Hidden;n6_3.Visibility = Visibility.Hidden;n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;n7_2.Visibility = Visibility.Hidden;n7_3.Visibility = Visibility.Hidden;n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="2")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Hidden;n3_2.Visibility = Visibility.Hidden;n3_3.Visibility = Visibility.Hidden;n3_4.Visibility = Visibility.Hidden;
                n4_1.Visibility = Visibility.Hidden;n4_2.Visibility = Visibility.Hidden;n4_3.Visibility = Visibility.Hidden;n4_4.Visibility = Visibility.Hidden;
                n5_1.Visibility = Visibility.Hidden;n5_2.Visibility = Visibility.Hidden;n5_3.Visibility = Visibility.Hidden;n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;n6_2.Visibility = Visibility.Hidden;n6_3.Visibility = Visibility.Hidden;n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;n7_2.Visibility = Visibility.Hidden;n7_3.Visibility = Visibility.Hidden;n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="3")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Hidden;n4_2.Visibility = Visibility.Hidden;n4_3.Visibility = Visibility.Hidden;n4_4.Visibility = Visibility.Hidden;
                n5_1.Visibility = Visibility.Hidden;n5_2.Visibility = Visibility.Hidden;n5_3.Visibility = Visibility.Hidden;n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;n6_2.Visibility = Visibility.Hidden;n6_3.Visibility = Visibility.Hidden;n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;n7_2.Visibility = Visibility.Hidden;n7_3.Visibility = Visibility.Hidden;n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="4")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;n4_2.Visibility = Visibility.Visible;n4_3.Visibility = Visibility.Visible;n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Hidden;n5_2.Visibility = Visibility.Hidden;n5_3.Visibility = Visibility.Hidden;n5_4.Visibility = Visibility.Hidden;
                n6_1.Visibility = Visibility.Hidden;n6_2.Visibility = Visibility.Hidden;n6_3.Visibility = Visibility.Hidden;n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;n7_2.Visibility = Visibility.Hidden;n7_3.Visibility = Visibility.Hidden;n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="5")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;n4_2.Visibility = Visibility.Visible;n4_3.Visibility = Visibility.Visible;n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;n5_2.Visibility = Visibility.Visible;n5_3.Visibility = Visibility.Visible;n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Hidden;n6_2.Visibility = Visibility.Hidden;n6_3.Visibility = Visibility.Hidden;n6_4.Visibility = Visibility.Hidden;
                n7_1.Visibility = Visibility.Hidden;n7_2.Visibility = Visibility.Hidden;n7_3.Visibility = Visibility.Hidden;n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="6")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;n4_2.Visibility = Visibility.Visible;n4_3.Visibility = Visibility.Visible;n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;n5_2.Visibility = Visibility.Visible;n5_3.Visibility = Visibility.Visible;n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;n6_2.Visibility = Visibility.Visible;n6_3.Visibility = Visibility.Visible;n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Hidden;n7_2.Visibility = Visibility.Hidden;n7_3.Visibility = Visibility.Hidden;n7_4.Visibility = Visibility.Hidden;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="7")
            {   
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;n4_2.Visibility = Visibility.Visible;n4_3.Visibility = Visibility.Visible;n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;n5_2.Visibility = Visibility.Visible;n5_3.Visibility = Visibility.Visible;n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;n6_2.Visibility = Visibility.Visible;n6_3.Visibility = Visibility.Visible;n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Visible;n7_2.Visibility = Visibility.Visible;n7_3.Visibility = Visibility.Visible;n7_4.Visibility = Visibility.Visible;
                n8_1.Visibility = Visibility.Hidden;n8_2.Visibility = Visibility.Hidden;n8_3.Visibility = Visibility.Hidden;n8_4.Visibility = Visibility.Hidden;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="8")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;n4_2.Visibility = Visibility.Visible;n4_3.Visibility = Visibility.Visible;n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;n5_2.Visibility = Visibility.Visible;n5_3.Visibility = Visibility.Visible;n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;n6_2.Visibility = Visibility.Visible;n6_3.Visibility = Visibility.Visible;n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Visible;n7_2.Visibility = Visibility.Visible;n7_3.Visibility = Visibility.Visible;n7_4.Visibility = Visibility.Visible;
                n8_1.Visibility = Visibility.Visible;n8_2.Visibility = Visibility.Visible;n8_3.Visibility = Visibility.Visible;n8_4.Visibility = Visibility.Visible;
                n9_1.Visibility = Visibility.Hidden;n9_2.Visibility = Visibility.Hidden;n9_3.Visibility = Visibility.Hidden;n9_4.Visibility = Visibility.Hidden;
            }
            if(k_s.Text=="9")
            {
                n2_1.Visibility = Visibility.Visible;n2_2.Visibility = Visibility.Visible;n2_3.Visibility = Visibility.Visible;n2_4.Visibility = Visibility.Visible;
                n3_1.Visibility = Visibility.Visible;n3_2.Visibility = Visibility.Visible;n3_3.Visibility = Visibility.Visible;n3_4.Visibility = Visibility.Visible;
                n4_1.Visibility = Visibility.Visible;n4_2.Visibility = Visibility.Visible;n4_3.Visibility = Visibility.Visible;n4_4.Visibility = Visibility.Visible;
                n5_1.Visibility = Visibility.Visible;n5_2.Visibility = Visibility.Visible;n5_3.Visibility = Visibility.Visible;n5_4.Visibility = Visibility.Visible;
                n6_1.Visibility = Visibility.Visible;n6_2.Visibility = Visibility.Visible;n6_3.Visibility = Visibility.Visible;n6_4.Visibility = Visibility.Visible;
                n7_1.Visibility = Visibility.Visible;n7_2.Visibility = Visibility.Visible;n7_3.Visibility = Visibility.Visible;n7_4.Visibility = Visibility.Visible;
                n8_1.Visibility = Visibility.Visible;n8_2.Visibility = Visibility.Visible;n8_3.Visibility = Visibility.Visible;n8_4.Visibility = Visibility.Visible;
                n9_1.Visibility = Visibility.Visible;n9_2.Visibility = Visibility.Visible;n9_3.Visibility = Visibility.Visible;n9_4.Visibility = Visibility.Visible;
            }
        }
        private void Ri_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            n1_4.Text = Ri_t.Text;
        }
        private void Rn_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            n9_4.Text = Rn_t.Text;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//сохраниение
        {
            Regex X = new Regex(@"^\d*(\,\d+)?$");

            if (X.IsMatch(n1_1.Text) && (X.IsMatch(n1_2.Text)) && (X.IsMatch(n1_3.Text)) && (X.IsMatch(n1_4.Text)) && (X.IsMatch(n2_1.Text)) && (X.IsMatch(n2_2.Text)) &&
                  (X.IsMatch(n2_3.Text)) && (X.IsMatch(n2_4.Text)) && (X.IsMatch(n3_1.Text)) && (X.IsMatch(n3_2.Text)) && (X.IsMatch(n3_3.Text)) && (X.IsMatch(n3_4.Text)) &&
                  (X.IsMatch(n4_1.Text)) && (X.IsMatch(n4_2.Text)) && (X.IsMatch(n4_3.Text)) && (X.IsMatch(n4_4.Text)) && (X.IsMatch(n5_1.Text)) && (X.IsMatch(n5_2.Text)) &&
                  (X.IsMatch(n5_3.Text)) && (X.IsMatch(n5_4.Text)) && (X.IsMatch(n6_1.Text)) && (X.IsMatch(n6_2.Text)) && (X.IsMatch(n6_3.Text)) && (X.IsMatch(n6_4.Text)) &&
                  (X.IsMatch(n7_1.Text)) && (X.IsMatch(n7_2.Text)) && (X.IsMatch(n7_3.Text)) && (X.IsMatch(n7_4.Text)) && (X.IsMatch(n8_1.Text)) && (X.IsMatch(n8_2.Text)) &&
                  (X.IsMatch(n8_3.Text)) && (X.IsMatch(n8_4.Text)) && (X.IsMatch(n9_1.Text)) && (X.IsMatch(n9_2.Text)) && (X.IsMatch(n9_3.Text)) && (X.IsMatch(n9_4.Text)) &&
                  (X.IsMatch(k_t.Text)) && (X.IsMatch(k_s.Text)) && (X.IsMatch(o_n.Text)) && (X.IsMatch(p_p.Text)) && (X.IsMatch(v_r.Text)) && (X.IsMatch(Ri_t.Text)) && (X.IsMatch(Rn_t.Text)) && (X.IsMatch(H_t.Text)))
            {
                if (string.IsNullOrWhiteSpace(k_t.Text) || string.IsNullOrWhiteSpace(k_s.Text) || string.IsNullOrWhiteSpace(o_n.Text) || string.IsNullOrWhiteSpace(p_p.Text) ||
                    string.IsNullOrWhiteSpace(v_r.Text) || string.IsNullOrWhiteSpace(Ri_t.Text) || string.IsNullOrWhiteSpace(Rn_t.Text) || string.IsNullOrWhiteSpace(H_t.Text) ||
                    string.IsNullOrWhiteSpace(n1_1.Text) || string.IsNullOrWhiteSpace(n1_2.Text) || string.IsNullOrWhiteSpace(n1_3.Text) || string.IsNullOrWhiteSpace(n1_4.Text) ||
                    string.IsNullOrWhiteSpace(n2_1.Text) || string.IsNullOrWhiteSpace(n2_2.Text) || string.IsNullOrWhiteSpace(n2_3.Text) || string.IsNullOrWhiteSpace(n2_4.Text) ||
                    string.IsNullOrWhiteSpace(n3_1.Text) || string.IsNullOrWhiteSpace(n3_2.Text) || string.IsNullOrWhiteSpace(n3_3.Text) || string.IsNullOrWhiteSpace(n3_4.Text) ||
                    string.IsNullOrWhiteSpace(n4_1.Text) || string.IsNullOrWhiteSpace(n4_2.Text) || string.IsNullOrWhiteSpace(n4_3.Text) || string.IsNullOrWhiteSpace(n4_4.Text) ||
                    string.IsNullOrWhiteSpace(n5_1.Text) || string.IsNullOrWhiteSpace(n5_2.Text) || string.IsNullOrWhiteSpace(n5_3.Text) || string.IsNullOrWhiteSpace(n5_4.Text) ||
                    string.IsNullOrWhiteSpace(n6_1.Text) || string.IsNullOrWhiteSpace(n6_2.Text) || string.IsNullOrWhiteSpace(n6_3.Text) || string.IsNullOrWhiteSpace(n6_4.Text) ||
                    string.IsNullOrWhiteSpace(n7_1.Text) || string.IsNullOrWhiteSpace(n7_2.Text) || string.IsNullOrWhiteSpace(n7_3.Text) || string.IsNullOrWhiteSpace(n7_4.Text) ||
                    string.IsNullOrWhiteSpace(n8_1.Text) || string.IsNullOrWhiteSpace(n8_2.Text) || string.IsNullOrWhiteSpace(n8_3.Text) || string.IsNullOrWhiteSpace(n8_4.Text) ||
                    string.IsNullOrWhiteSpace(n9_1.Text) || string.IsNullOrWhiteSpace(n9_2.Text) || string.IsNullOrWhiteSpace(n9_3.Text) || string.IsNullOrWhiteSpace(n9_4.Text)

                    )
                {
                    MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    using (Table_context db = new Table_context())
                    {
                        Table_base n1 = new Table_base
                        {
                            t1_1 = Convert.ToDouble(n1_1.Text),t1_2 = Convert.ToDouble(n1_2.Text),t1_3 = Convert.ToDouble(n1_3.Text),t1_4 = Convert.ToDouble(n1_4.Text),
                            t2_1 = Convert.ToDouble(n2_1.Text),t2_2 = Convert.ToDouble(n2_2.Text),t2_3 = Convert.ToDouble(n2_3.Text),t2_4 = Convert.ToDouble(n2_4.Text),
                            t3_1 = Convert.ToDouble(n3_1.Text),t3_2 = Convert.ToDouble(n3_2.Text),t3_3 = Convert.ToDouble(n3_3.Text),t3_4 = Convert.ToDouble(n3_4.Text),
                            t4_1 = Convert.ToDouble(n4_1.Text),t4_2 = Convert.ToDouble(n4_2.Text),t4_3 = Convert.ToDouble(n4_3.Text),t4_4 = Convert.ToDouble(n4_4.Text),
                            t5_1 = Convert.ToDouble(n5_1.Text),t5_2 = Convert.ToDouble(n5_2.Text),t5_3 = Convert.ToDouble(n5_3.Text),t5_4 = Convert.ToDouble(n5_4.Text),
                            t6_1 = Convert.ToDouble(n6_1.Text),t6_2 = Convert.ToDouble(n6_2.Text),t6_3 = Convert.ToDouble(n6_3.Text),t6_4 = Convert.ToDouble(n6_4.Text),
                            t7_1 = Convert.ToDouble(n7_1.Text),t7_2 = Convert.ToDouble(n7_2.Text),t7_3 = Convert.ToDouble(n7_3.Text),t7_4 = Convert.ToDouble(n7_4.Text),
                            t8_1 = Convert.ToDouble(n8_1.Text),t8_2 = Convert.ToDouble(n8_2.Text),t8_3 = Convert.ToDouble(n8_3.Text),t8_4 = Convert.ToDouble(n8_4.Text),
                            t9_1 = Convert.ToDouble(n9_1.Text),t9_2 = Convert.ToDouble(n9_2.Text),t9_3 = Convert.ToDouble(n9_3.Text),t9_4 = Convert.ToDouble(n9_4.Text),
                            k_t_base = Convert.ToDouble(k_t.Text),
                            k_s_base = Convert.ToDouble(k_s.Text),
                            o_s_base = Convert.ToDouble(o_n.Text),
                            p_p_base = Convert.ToDouble(p_p.Text),
                            v_r_base = Convert.ToDouble(v_r.Text),
                            ri_base = Convert.ToDouble(Ri_t.Text),
                            rn_base = Convert.ToDouble(Rn_t.Text),
                            h_base = Convert.ToDouble(H_t.Text)
                        };

                        db.table.Add(n1);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        private void Button_Click_4(object sender, RoutedEventArgs e)//загрузка
        {
            using (Table_context db = new Table_context())
            {
                
                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@Id", "%"+box.SelectedItem+"");
                var table = db.Database.SqlQuery<Table_base>("SELECT * FROM Table_base WHERE Id LIKE @Id",param);
                foreach (var table_base in table)
                {
                    k_t.Text = Convert.ToString(table_base.k_t_base);
                    k_s.Text = Convert.ToString(table_base.k_s_base);
                    o_n.Text = Convert.ToString(table_base.o_s_base);
                    p_p.Text = Convert.ToString(table_base.p_p_base);
                    v_r.Text = Convert.ToString(table_base.v_r_base);
                    n1_1.Text = Convert.ToString(table_base.t1_1);n1_2.Text = Convert.ToString(table_base.t1_2);n1_3.Text = Convert.ToString(table_base.t1_3);n1_4.Text = Convert.ToString(table_base.t1_4);
                    n2_1.Text = Convert.ToString(table_base.t2_1); n2_2.Text = Convert.ToString(table_base.t2_2); n2_3.Text = Convert.ToString(table_base.t2_3); n2_4.Text = Convert.ToString(table_base.t2_4);
                    n3_1.Text = Convert.ToString(table_base.t3_1); n3_2.Text = Convert.ToString(table_base.t3_2); n3_3.Text = Convert.ToString(table_base.t3_3); n3_4.Text = Convert.ToString(table_base.t3_4);
                    n4_1.Text = Convert.ToString(table_base.t4_1); n4_2.Text = Convert.ToString(table_base.t4_2); n4_3.Text = Convert.ToString(table_base.t4_3); n4_4.Text = Convert.ToString(table_base.t4_4);
                    n5_1.Text = Convert.ToString(table_base.t5_1); n5_2.Text = Convert.ToString(table_base.t5_2); n5_3.Text = Convert.ToString(table_base.t5_3); n5_4.Text = Convert.ToString(table_base.t5_4);
                    n6_1.Text = Convert.ToString(table_base.t6_1); n6_2.Text = Convert.ToString(table_base.t6_2); n6_3.Text = Convert.ToString(table_base.t6_3); n6_4.Text = Convert.ToString(table_base.t6_4);
                    n7_1.Text = Convert.ToString(table_base.t7_1); n7_2.Text = Convert.ToString(table_base.t7_2); n7_3.Text = Convert.ToString(table_base.t7_3); n7_4.Text = Convert.ToString(table_base.t7_4);
                    n8_1.Text = Convert.ToString(table_base.t8_1); n8_2.Text = Convert.ToString(table_base.t8_2); n8_3.Text = Convert.ToString(table_base.t8_3); n8_4.Text = Convert.ToString(table_base.t8_4);
                    n9_1.Text = Convert.ToString(table_base.t9_1); n9_2.Text = Convert.ToString(table_base.t9_2); n9_3.Text = Convert.ToString(table_base.t9_3); n9_4.Text = Convert.ToString(table_base.t9_4);
                    Ri_t.Text = Convert.ToString(table_base.ri_base);Rn_t.Text = Convert.ToString(table_base.rn_base);H_t.Text = Convert.ToString(table_base.h_base);
                }
            }

        }
        private void ListBox_Loaded(object sender, RoutedEventArgs e)//заполнение listbox
        {
            using (Table_context db = new Table_context())
            {
                var table = db.Database.SqlQuery<Table_base>("SELECT Id FROM Table_base");
                
                int[] countries = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
                foreach (var s in countries)
                box.Items.Add(s);
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)//обновление
        {
            Regex X = new Regex(@"^\d*(\,\d+)?$");

            if (X.IsMatch(n1_1.Text) && (X.IsMatch(n1_2.Text)) && (X.IsMatch(n1_3.Text)) && (X.IsMatch(n1_4.Text)) && (X.IsMatch(n2_1.Text)) && (X.IsMatch(n2_2.Text)) &&
                  (X.IsMatch(n2_3.Text)) && (X.IsMatch(n2_4.Text)) && (X.IsMatch(n3_1.Text)) && (X.IsMatch(n3_2.Text)) && (X.IsMatch(n3_3.Text)) && (X.IsMatch(n3_4.Text)) &&
                  (X.IsMatch(n4_1.Text)) && (X.IsMatch(n4_2.Text)) && (X.IsMatch(n4_3.Text)) && (X.IsMatch(n4_4.Text)) && (X.IsMatch(n5_1.Text)) && (X.IsMatch(n5_2.Text)) &&
                  (X.IsMatch(n5_3.Text)) && (X.IsMatch(n5_4.Text)) && (X.IsMatch(n6_1.Text)) && (X.IsMatch(n6_2.Text)) && (X.IsMatch(n6_3.Text)) && (X.IsMatch(n6_4.Text)) &&
                  (X.IsMatch(n7_1.Text)) && (X.IsMatch(n7_2.Text)) && (X.IsMatch(n7_3.Text)) && (X.IsMatch(n7_4.Text)) && (X.IsMatch(n8_1.Text)) && (X.IsMatch(n8_2.Text)) &&
                  (X.IsMatch(n8_3.Text)) && (X.IsMatch(n8_4.Text)) && (X.IsMatch(n9_1.Text)) && (X.IsMatch(n9_2.Text)) && (X.IsMatch(n9_3.Text)) && (X.IsMatch(n9_4.Text)) &&
                  (X.IsMatch(k_t.Text)) && (X.IsMatch(k_s.Text)) && (X.IsMatch(o_n.Text)) && (X.IsMatch(p_p.Text)) && (X.IsMatch(v_r.Text)) && (X.IsMatch(Ri_t.Text)) && (X.IsMatch(Rn_t.Text)) && (X.IsMatch(H_t.Text)))
            {

                using (Table_context db = new Table_context())
                {
                    if (string.IsNullOrWhiteSpace(k_t.Text) || string.IsNullOrWhiteSpace(k_s.Text) || string.IsNullOrWhiteSpace(o_n.Text) || string.IsNullOrWhiteSpace(p_p.Text) ||
                         string.IsNullOrWhiteSpace(v_r.Text) || string.IsNullOrWhiteSpace(Ri_t.Text) || string.IsNullOrWhiteSpace(Rn_t.Text) || string.IsNullOrWhiteSpace(H_t.Text) ||
                         string.IsNullOrWhiteSpace(n1_1.Text) || string.IsNullOrWhiteSpace(n1_2.Text) || string.IsNullOrWhiteSpace(n1_3.Text) || string.IsNullOrWhiteSpace(n1_4.Text) ||
                         string.IsNullOrWhiteSpace(n2_1.Text) || string.IsNullOrWhiteSpace(n2_2.Text) || string.IsNullOrWhiteSpace(n2_3.Text) || string.IsNullOrWhiteSpace(n2_4.Text) ||
                         string.IsNullOrWhiteSpace(n3_1.Text) || string.IsNullOrWhiteSpace(n3_2.Text) || string.IsNullOrWhiteSpace(n3_3.Text) || string.IsNullOrWhiteSpace(n3_4.Text) ||
                         string.IsNullOrWhiteSpace(n4_1.Text) || string.IsNullOrWhiteSpace(n4_2.Text) || string.IsNullOrWhiteSpace(n4_3.Text) || string.IsNullOrWhiteSpace(n4_4.Text) ||
                         string.IsNullOrWhiteSpace(n5_1.Text) || string.IsNullOrWhiteSpace(n5_2.Text) || string.IsNullOrWhiteSpace(n5_3.Text) || string.IsNullOrWhiteSpace(n5_4.Text) ||
                         string.IsNullOrWhiteSpace(n6_1.Text) || string.IsNullOrWhiteSpace(n6_2.Text) || string.IsNullOrWhiteSpace(n6_3.Text) || string.IsNullOrWhiteSpace(n6_4.Text) ||
                         string.IsNullOrWhiteSpace(n7_1.Text) || string.IsNullOrWhiteSpace(n7_2.Text) || string.IsNullOrWhiteSpace(n7_3.Text) || string.IsNullOrWhiteSpace(n7_4.Text) ||
                         string.IsNullOrWhiteSpace(n8_1.Text) || string.IsNullOrWhiteSpace(n8_2.Text) || string.IsNullOrWhiteSpace(n8_3.Text) || string.IsNullOrWhiteSpace(n8_4.Text) ||
                         string.IsNullOrWhiteSpace(n9_1.Text) || string.IsNullOrWhiteSpace(n9_2.Text) || string.IsNullOrWhiteSpace(n9_3.Text) || string.IsNullOrWhiteSpace(n9_4.Text)

                         )
                    {
                        MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        int zh = Convert.ToInt32(box.SelectedItem);
                        var a = db.table.Where(c => c.Id == zh).FirstOrDefault();
                        a.k_t_base = Convert.ToDouble(k_t.Text);
                        a.k_s_base = Convert.ToDouble(k_s.Text);
                        a.o_s_base = Convert.ToDouble(o_n.Text);
                        a.p_p_base = Convert.ToDouble(p_p.Text);
                        a.v_r_base = Convert.ToDouble(v_r.Text);
                        a.ri_base = Convert.ToDouble(Ri_t.Text);
                        a.rn_base = Convert.ToDouble(Rn_t.Text);
                        a.h_base = Convert.ToDouble(H_t.Text);
                        a.t1_1 = Convert.ToDouble(n1_1.Text); a.t1_2 = Convert.ToDouble(n1_2.Text); a.t1_3 = Convert.ToDouble(n1_3.Text); a.t1_4 = Convert.ToDouble(n1_4.Text);
                        a.t2_1 = Convert.ToDouble(n2_1.Text); a.t2_2 = Convert.ToDouble(n2_2.Text); a.t2_3 = Convert.ToDouble(n2_3.Text); a.t2_4 = Convert.ToDouble(n2_4.Text);
                        a.t3_1 = Convert.ToDouble(n3_1.Text); a.t3_2 = Convert.ToDouble(n3_2.Text); a.t3_3 = Convert.ToDouble(n3_3.Text); a.t3_4 = Convert.ToDouble(n3_4.Text);
                        a.t4_1 = Convert.ToDouble(n4_1.Text); a.t4_2 = Convert.ToDouble(n4_2.Text); a.t4_3 = Convert.ToDouble(n4_3.Text); a.t4_4 = Convert.ToDouble(n4_4.Text);
                        a.t5_1 = Convert.ToDouble(n5_1.Text); a.t5_2 = Convert.ToDouble(n5_2.Text); a.t5_3 = Convert.ToDouble(n5_3.Text); a.t5_4 = Convert.ToDouble(n5_4.Text);
                        a.t6_1 = Convert.ToDouble(n6_1.Text); a.t6_2 = Convert.ToDouble(n6_2.Text); a.t6_3 = Convert.ToDouble(n6_3.Text); a.t6_4 = Convert.ToDouble(n6_4.Text);
                        a.t7_1 = Convert.ToDouble(n7_1.Text); a.t7_2 = Convert.ToDouble(n7_2.Text); a.t7_3 = Convert.ToDouble(n7_3.Text); a.t7_4 = Convert.ToDouble(n7_4.Text);
                        a.t8_1 = Convert.ToDouble(n8_1.Text); a.t8_2 = Convert.ToDouble(n8_2.Text); a.t8_3 = Convert.ToDouble(n8_3.Text); a.t8_4 = Convert.ToDouble(n8_4.Text);
                        a.t9_1 = Convert.ToDouble(n9_1.Text); a.t9_2 = Convert.ToDouble(n9_2.Text); a.t9_3 = Convert.ToDouble(n9_3.Text); a.t9_4 = Convert.ToDouble(n9_4.Text);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Закрыть расчёт?", "Запрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            { }

            else e.Cancel = true;
        }
    }

    }

