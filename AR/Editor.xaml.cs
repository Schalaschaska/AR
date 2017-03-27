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
            
            NetOffice.WordApi.Application word = new NetOffice.WordApi.Application();
            word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            NetOffice.WordApi.Document newdoc = word.Documents.Add();
            word.Selection.TypeText("Test text "+textBox.Text);//пока тольео так(
            

            word.Selection.HomeKey(WdUnits.wdLine, WdMovementType.wdExtend);
            word.Selection.Font.Color = WdColor.wdColorAqua;
            word.Selection.Font.Bold = 1;
            word.Selection.Font.Size = 18;
            string fileExtension = GetDefaultExtension(word);
            object documentFile = string.Format("{0}\\Test{1}", Directory.GetCurrentDirectory(), fileExtension);
            newdoc.SaveAs(documentFile);
            word.Quit();
            word.Dispose();
            yi = 2*a * (Ri - R0);
            Vi = Math.Pow((1 + Math.Pow(yi, 2)), -(1 / 2));
            qi = (di * Vi * q) / (36 * di * Math.Pow(Vi, 2));
            a = 2 * 3.141593 * f * qi * Ri;
            MessageBox.Show(Convert.ToString(qi));
            MessageBox.Show(Convert.ToString(a));
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
