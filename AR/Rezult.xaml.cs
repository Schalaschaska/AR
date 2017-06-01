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
    /// Логика взаимодействия для Rezult.xaml
    /// </summary>
    public partial class Rezult : Window
    {
        public Rezult()
        {
            InitializeComponent();


        }
        public double kol;
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (Table_context db = new Table_context())
            {
                var Table_R = db.table;
                foreach (Table_base u in Table_R)
                {


                    MessageBox.Show(Convert.ToString(u.kol));
                    List<Table_content> result = new List<Table_content>(Convert.ToInt32(kol));
                    for (int i = 0; i <= u.kol; i++)
                    {
                        result.Add(new Table_content(i, 0, 0, 0));

                    }
                    DG.ItemsSource = result;
                }
            } 
        }
    }
}

