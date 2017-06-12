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
                Directory.CreateDirectory(textBox.Text);
                Directory.SetCurrentDirectory(textBox.Text);
                Editor editor = new Editor();
                editor.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        
    }
}
