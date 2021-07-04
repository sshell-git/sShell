using System;
using System.IO;
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

namespace sShell.Windows
{
    /// <summary>
    /// Interaction logic for Explorer.xaml
    /// </summary>
    public partial class Explorer : Window
    {
        MainWindow mw;

        public void setMW(MainWindow mw)
        {
            this.mw = mw;
        }

        public Explorer()
        {
            InitializeComponent();
        }

        private void whdlrs_DragWin(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeView treeView = new TreeView()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = mw.FindResource("BGAlt2") as System.Windows.Media.Brush,
                BorderBrush = null
            };
            foreach (var drive in DriveInfo.GetDrives())
            {
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                Image image = new Image() { Margin = new Thickness(0, 0, 5, 0), Width = 16, Height = 16, Source = IconHandler.GetIcon(drive.RootDirectory.ToString(), false, true) };
                TextBlock textBox = new TextBlock() { Text = drive.RootDirectory.ToString() };
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBox);
                TreeViewItem driveItm = new TreeViewItem() { Header = stackPanel };
                treeView.Items.Add(driveItm);
                foreach (var folder in Directory.GetDirectories(drive.RootDirectory.ToString()))
                {
                    StackPanel stackPanel1 = new StackPanel() { Orientation = Orientation.Horizontal };
                    Image image1 = new Image() { Margin = new Thickness(0, 0, 5, 0), Width = 16, Height = 16, Source = IconHandler.GetIcon(folder, false, true) };
                    TextBlock textBox1 = new TextBlock() { Text = folder };
                    stackPanel1.Children.Add(image1);
                    stackPanel1.Children.Add(textBox1);
                    TreeViewItem folderItm = new TreeViewItem() { Header = stackPanel1 };
                    driveItm.Items.Add(folderItm);
                }
                foreach (var file in Directory.GetFiles(drive.RootDirectory.ToString()))
                {
                    StackPanel stackPanel2 = new StackPanel() { Orientation = Orientation.Horizontal };
                    Image image2 = new Image() { Margin = new Thickness(0, 0, 5, 0), Width = 16, Height = 16, Source = IconHandler.GetIcon(file, false, false) };
                    TextBlock textBox2 = new TextBlock() { Text = file };
                    stackPanel2.Children.Add(image2);
                    stackPanel2.Children.Add(textBox2);
                    TreeViewItem fileItm = new TreeViewItem() { Header = stackPanel2 };
                    driveItm.Items.Add(fileItm);
                }
            }
            LeftPane.Children.Add(treeView);
            /* StackPanel stackPanel1 = new StackPanel() { Orientation = Orientation.Horizontal };
             Image image1 = new Image() { Margin = new Thickness(0, 0, 5, 0), Width = 16, Height = 16, Source = IconHandler.GetIcon(@"C:\Users\austi\Documents\", false, true) };
             TextBlock textBox1 = new TextBlock() { Text = "folder_close" };
             stackPanel1.Children.Add(image1);
             stackPanel1.Children.Add(textBox1);
             TreeViewItem folder_close = new TreeViewItem() { Header = stackPanel1 }; */

        }
    }
}
