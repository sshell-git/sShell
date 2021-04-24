using System.Windows.Forms;
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

namespace SShell.windows
{
    /// <summary>
    /// Interaction logic for Desktop.xaml
    /// </summary>
    /// 
    public class WindowHandler
    {
        public List<Window> Windows { get; set; }

    }
    public partial class Desktop : Window
    {
        public Desktop()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBrush brush = new()
            {
                ImageSource = new BitmapImage(new Uri(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper"))
            };
            desktopGrid.Background = brush;
            MainWindow.SendWpfWindowBack(this);
        }
    }
}
