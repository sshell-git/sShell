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

namespace SShell.Windows
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
            #region Init
            Window w = new()
            {
                Top = -100, // Location of new window is outside of visible part of screen
                Left = -100,
                Width = 1, // size of window is enough small to avoid its appearance at the beginning
                Height = 1,

                WindowStyle = WindowStyle.ToolWindow, // Set window style as ToolWindow to avoid its icon in AltTab 
                ShowInTaskbar = false
            }; // Create helper window
            w.Show(); // We need to show window before set is as owner to our main window
            this.Owner = w; // Okay, this will result to disappear icon for main window.
            w.Hide(); // Hide helper window just in case
            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBrush brush = new()
            {
                ImageSource = new BitmapImage(new Uri(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper"))
            };
            brush.Stretch = Stretch.UniformToFill;
            desktopGrid.Background = brush;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Activate();
        }
    }
}
