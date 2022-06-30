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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET_INIS4_PR2._2_z2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dane dane = new Dane();
        public MainWindow()
        {
            DataContext = dane;
            InitializeComponent();
        }

        private void DopiszZnak(object sender, RoutedEventArgs e)
        {
            dane.Number((string)((Button)sender).Content);
        }

        private void Resetuj(object sender, RoutedEventArgs e)
        {
            dane.Restart();
        }

        private void RównaSię(object sender, RoutedEventArgs e)
        {
            dane.ShowResult();
        }

        private void Działanie(object sender, RoutedEventArgs e)
        {
            dane.WriteSign((string)((Button)sender).Content);
        }
    }
}
