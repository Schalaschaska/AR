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



namespace AR
{
    /// <summary>
    /// Логика взаимодействия для CreateWin.xaml
    /// </summary>
    /// 

    public partial class CreateWin : Window
    {
        public CreateWin()
        {
            InitializeComponent();

            
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            Regex Reg_z = new Regex(@"^[a-zA-Z0-9]+$");
            if (Reg_z.IsMatch(textBox.Text))
            {
                FileStream file = new FileStream("history.txt", FileMode.Append, FileAccess.Write);
                StreamWriter strem = new StreamWriter(file);
                strem.WriteLine(textBox.Text);
                strem.Close();
                file.Close();
                NetOffice.WordApi.Application word = new NetOffice.WordApi.Application();
                word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                NetOffice.WordApi.Document newdoc = word.Documents.Add();
                word.Selection.TypeText("Test text");
                word.Selection.HomeKey(WdUnits.wdLine, WdMovementType.wdExtend);
                word.Selection.Font.Color = WdColor.wdColorAqua;
                word.Selection.Font.Bold = 1;
                word.Selection.Font.Size = 18;
                


                Editor editor = new Editor();
                editor.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
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
