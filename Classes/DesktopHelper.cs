using System.Windows.Forms;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic.Devices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sShell.Classes
{
    class DesktopHelper
    {
        public void ShowDesktop()
        {
            int index = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                // System.Windows.MessageBox.Show(string.Format("Monitor {0}: {1} x {2}", index, screen.Bounds.X, screen.Bounds.Y));
                Windows.Desktop win = new()
                {
                    Left = screen.Bounds.X,
                    Top = screen.Bounds.Y,
                    Width = screen.Bounds.Width,
                    Height = screen.Bounds.Height
                };
                win.Show();
                if (index == 0)
                {
                    TextBlock text = new()
                    {
                        Foreground = Brushes.White,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        TextAlignment = TextAlignment.Right,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                        FontSize = 12,
                        Margin = new Thickness(0, 0, 3, 40),
                        Text = "sShell v1.0 beta running on" + new ComputerInfo().OSVersion
                    };
                    System.Windows.MessageBox.Show("sShell v1.0 beta running on" + new ComputerInfo().OSVersion);
                    win.desktopGrid.Children.Add(text);
                    // <TextBlock Text = "" Foreground = "White" VerticalAlignment = "Bottom" TextAlignment = "Right" HorizontalAlignment = "Right" FontSize = "12" Margin = "0,0,3,40" />
                }
                win.ContentRendered += new EventHandler(MainWindow.SendWpfWindowBack);
                win.Activated += new EventHandler(MainWindow.SendWpfWindowBack);
                win.Deactivated += new EventHandler(MainWindow.SendWpfWindowBack);
                win.Tag = "desktopWin" + index;
                index++;
            }
            System.Windows.Application.Current.MainWindow.Activate();
        }
    }
}
