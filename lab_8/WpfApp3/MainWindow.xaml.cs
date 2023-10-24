using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for WindowList.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int createCount;
        public MainWindow()
        {
            InitializeComponent();

            // Определение заголовка окна
            this.Title = "Окно № " + (createCount++).ToString();
        }

        private void NewWindowClicked(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }

        private void ListOpenWindows(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Window openWindow in Application.Current.Windows)
                sb.AppendLine(openWindow.Title);

            MessageBox.Show(sb.ToString(), "Открытые окна приложения");
        }
        private void AllCloseWindows(object sender, RoutedEventArgs e)
        {
            //Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Application.Current.Shutdown();// Закрывает при любом режиме
        }
    }
}