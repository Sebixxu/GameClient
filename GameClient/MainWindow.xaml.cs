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

namespace GameClient 
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //Klasa ktora odpowiada za UI
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            Launcher.UpdateFiles(progressBar);
        }

        private void mainSiteBtn_Click(object sender, RoutedEventArgs e)
        {
            Launcher.MainSite("https://pl.metin2.gameforge.com/");
        }

        private void optionBtn_Click(object sender, RoutedEventArgs e)
        {
            Launcher.Options("config.exe");
        }
    }
}
