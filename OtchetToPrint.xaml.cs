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

namespace Ychet
{
    /// <summary>
    /// Логика взаимодействия для OtchetToPrint.xaml
    /// </summary>
    public partial class OtchetToPrint : Window
    {
        public OtchetToPrint()
        {
            InitializeComponent();
            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() == true)
            {
                p.PrintVisual(PrintInfo, "Отчет");
            }
        }
    }
}
