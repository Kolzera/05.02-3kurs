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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void LifeEvents(object sender, RoutedEventArgs e)
        {
            Window1 wnd1 = new Window1();
            wnd1.ShowInTaskbar = false; // Не показывать в панели задач
            wnd1.ShowDialog();          // Показать в модальном режиме
        }
    }
    

}
