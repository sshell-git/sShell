using System.Windows.Forms;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SShell.Classes
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
                win.ContentRendered += new EventHandler(MainWindow.SendWpfWindowBack);
                win.Tag = "desktopWin" + index;
                index++;
            }
        }
    }
}
