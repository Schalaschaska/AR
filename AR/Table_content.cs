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


namespace AR
{
    class Table_content
    {
        public int Id { get; set; }
        public double Ki { get; set; }
        public double Di { get; set; }
        public double Ri { get; set; }
        public Table_content(int Id,double Ki,double Di,double Ri)
        {
            this.Id = Id;
            this.Ki = Ki;
            this.Di = Di;
            this.Ri = Ri;

        }
    }
}
