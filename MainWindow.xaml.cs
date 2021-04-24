using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Automation;
using System.Windows.Shapes;

namespace SShell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class IconHandler
    {
        public static ImageSource GetIcon(string path, bool smallIcon, bool isDirectory)
        {
            // SHGFI_USEFILEATTRIBUTES takes the file name and attributes into account if it doesn't exist
            uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;
            if (smallIcon)
                flags |= SHGFI_SMALLICON;

            uint attributes = FILE_ATTRIBUTE_NORMAL;
            if (isDirectory)
                attributes |= FILE_ATTRIBUTE_DIRECTORY;

            SHFILEINFO shfi;
            if (0 != SHGetFileInfo(
                        path,
                        attributes,
                        out shfi,
                        (uint)Marshal.SizeOf(typeof(SHFILEINFO)),
                        flags))
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                            shfi.hIcon,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
            }
            return null;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32")]
        private static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint flags);

        private const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
        private const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
        private const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        private const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
        private const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
        private const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        private const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
        private const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
        private const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
        private const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
        private const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
        private const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;
        private const uint FILE_ATTRIBUTE_VIRTUAL = 0x00010000;

        private const uint SHGFI_ICON = 0x000000100;     // get icon
        private const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
        private const uint SHGFI_TYPENAME = 0x000000400;     // get type name
        private const uint SHGFI_ATTRIBUTES = 0x000000800;     // get attributes
        private const uint SHGFI_ICONLOCATION = 0x000001000;     // get icon location
        private const uint SHGFI_EXETYPE = 0x000002000;     // return exe type
        private const uint SHGFI_SYSICONINDEX = 0x000004000;     // get system icon index
        private const uint SHGFI_LINKOVERLAY = 0x000008000;     // put a link overlay on icon
        private const uint SHGFI_SELECTED = 0x000010000;     // show icon in selected state
        private const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
        private const uint SHGFI_LARGEICON = 0x000000000;     // get large icon
        private const uint SHGFI_SMALLICON = 0x000000001;     // get small icon
        private const uint SHGFI_OPENICON = 0x000000002;     // get open icon
        private const uint SHGFI_SHELLICONSIZE = 0x000000004;     // get shell size icon
        private const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
        private const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;     // use passed dwFileAttribute

    }

    public partial class MainWindow : Window
    {
        private NotificationHandler notifHandler;
        public bool MenuOpen = false;
        public Process currproc;
        public MainWindow()
        {
            InitializeComponent();
            // TODO: MAKE WINDOW WORK IN NOTIFICATION CLAS :((
            notifHandler = new NotificationHandler();
            notifHandler.setMW(this);
            DispatcherTimer timer = new(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.dateText.Text = DateTime.Now.ToString("HH:mm:ss tt\nM/d/yyyy");
            }, Dispatcher);
            Window w = new Window(); // Create helper window
            w.Top = -100; // Location of new window is outside of visible part of screen
            w.Left = -100;
            w.Width = 1; // size of window is enough small to avoid its appearance at the beginning
            w.Height = 1;

            w.WindowStyle = WindowStyle.ToolWindow; // Set window style as ToolWindow to avoid its icon in AltTab 
            w.ShowInTaskbar = false;
            w.Show(); // We need to show window before set is as owner to our main window
            this.Owner = w; // Okey, this will result to disappear icon for main window.
            w.Hide(); // Hide helper window just in case
            notifHandler.ShowNotification(new Notification() {Title = "Welcome!", Type = NotificationType.Default, Description = "Welcome to SShell, a simple WPF shell created for fun.\nSome programs may ask for admin privledges."});
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // var newBrush = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\austi\Downloads\TranscodedWallpaper.jpg")));
            // this.Background = newBrush;
            // newBrush.Stretch = Stretch.UniformToFill;
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle) && process.ProcessName != "SShell")
                {
                    /* Let's recreate the following XAML in C#:
                    <Border Margin="0,0,3,0" Width="46" Style="{StaticResource TBitemPanelBdr}">
                        <iconPacks:PackIconMaterialDesign HorizontalAlignment="Center" Kind="Menu" Height="25" Width="25" Margin="2,0,2,0" VerticalAlignment="Center" Padding="5" Foreground="White" />
                    </Border>
                     */
                    try
                    {
                        string fullPath = process.MainModule.FileName;
                        Border border = new()
                        {
                            Margin = new Thickness(0, 0, 3, 0),
                            Width = 46,
                            Style = this.FindResource("TBitemPanelBdr") as Style,
                            ToolTip = process.MainWindowTitle + "\n" + process.ProcessName,
                            Tag = fullPath,
                            HorizontalAlignment = HorizontalAlignment.Left
                        };
                        border.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(FocusWin);
                        System.Windows.Controls.Image image = new()
                        {
                            Source = IconHandler.GetIcon(fullPath, false, false),
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(0),
                            Width = 25,
                            Height = 25,
                            Stretch = Stretch.Uniform
                        };
                        border.Child = image;
                        taskbar.Children.Add(border);
#if DEBUG
                        /*  MessageBox.Show(string.Format("Filename: {3}\nProcess: {0} \nID: {1} \nWindow title: {2}", process.ProcessName, process.Id, process.MainWindowTitle, fullPath)); */
#endif
                    }
                    catch (Exception error)
                    {
                        //Border border = new()
                        //{
                        //    Margin = new Thickness(0, 0, 3, 0),
                        //    Width = 46,
                        //    Style = this.FindResource("TBitemPanelBdr") as Style,
                        //    ToolTip = "Error",
                        //    HorizontalAlignment = HorizontalAlignment.Left
                        //};
                        //PackIconMaterialDesign packIcon = new()
                        //{
                        //    HorizontalAlignment = HorizontalAlignment.Center,
                        //    Kind = PackIconMaterialDesignKind.Error,
                        //    Foreground = FindResource("bgDanger") as System.Windows.Media.Brush,
                        //    Height = 25,
                        //    Width = 25
                        //};
                        //border.Child = packIcon;
                        //System.Windows.Controls.Image image = new()
                        //{
                        //    Source = new BitmapImage(new Uri(@"pack://application:,,,/assets/icon.png")),
                        //    VerticalAlignment = VerticalAlignment.Center,
                        //    HorizontalAlignment = HorizontalAlignment.Center,
                        //    Margin = new Thickness(0),
                        //    Width = 25,
                        //    Height = 25,
                        //    Stretch = Stretch.Uniform
                        //};
                        //border.Child = image;
                        //taskbar.Children.Add(border);
                        Notification notif = new()
                        {
                            Title = "An error occured...",
                            Description = string.Format("Failed to get program {0} because the following error occured:\n{1}", process.ProcessName, error.Message),
                            Type = NotificationType.Error
                        };
                        notifHandler.ShowNotification(notif);
#if DEBUG
                        // MessageBox.Show(string.Format("Failed to get program {0} because the following error occured:\n{1}", process.ProcessName, error.Message));
#endif
                    }
                }
            }
        }


        public void FocusWin(object sender, MouseEventArgs e)
        {
            Border bdr = sender as Border;
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                try
                {
                    if (!String.IsNullOrEmpty(process.MainWindowTitle) && bdr.Tag.ToString() == process.MainModule.FileName && process.ProcessName != "SShell")
                    {
                        AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                        if (element != null)
                        {
                            element.SetFocus();
                        }
                    }
                } catch
                {
#if DEBUG
                    // MessageBox.Show("oops");
#endif
                }
            }

        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Make entire window and everything in it "transparent" to the Mouse
            var windowHwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(windowHwnd);

            // Make the button "visible" to the Mouse
            var buttonHwndSource = (HwndSource)HwndSource.FromVisual(tb);
            var buttonHwnd = buttonHwndSource.Handle;
            WindowsServices.SetWindowExNotTransparent(buttonHwnd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // double width = SystemParameters.VirtualScreenWidth;
            // double height = SystemParameters.VirtualScreenHeight;
            // double top = SystemParameters.VirtualScreenTop;
            // double left = SystemParameters.VirtualScreenLeft;
            // MessageBox.Show(string.Format("Width x Height: {0} x {1} (Top: {2}, Left: {3})", width, height, top, left));
            Menu.Visibility = Visibility.Collapsed;
            //int windowPosX = Screen.PrimaryScreen.WorkingArea.Left;
            //int windowPosY = Screen.PrimaryScreen.WorkingArea.Top;

            //int windowWidth = Screen.PrimaryScreen.WorkingArea.Width;
            //int windowHeight = Screen.PrimaryScreen.WorkingArea.Height;

            //foreach (Screen s in Screen.AllScreens)
            //    if (!s.Primary)
            //        windowWidth += s.Bounds.Width;

            //// Set the window's left, top, width and height properties accordingly

        }

        private void ToggleMenu(object sender, MouseButtonEventArgs e)
        {
            if (MenuOpen)
            {
                Menu.Visibility = Visibility.Collapsed;
                MenuOpen = false;
            }
            else
            {
                Menu.Visibility = Visibility.Visible;
                MenuOpen = true;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // hide all taskbar icons stuff
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            MenuOpen = false;
        }

        private void whdlrs_openSettings(object sender, MouseButtonEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            MenuOpen = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public static class WindowsServices
    {
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void SetWindowExTransparent(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        public static void SetWindowExNotTransparent(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & ~WS_EX_TRANSPARENT);
        }
    }
}
