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

namespace AR
{
    /// <summary>
    /// Логика взаимодействия для CreateWin.xaml
    /// </summary>
    public partial class CreateWin : Window
    {
        public CreateWin()
        {
            InitializeComponent();
            
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            FileStream file = new FileStream("history.txt", FileMode.Append, FileAccess.Write);
            StreamWriter strem = new StreamWriter(file);
            strem.WriteLine(textBox.Text);
            strem.Close();
            file.Close();
            Editor editor = new Editor();
            editor.Show();
            this.Close();
            
        }
    }
}
