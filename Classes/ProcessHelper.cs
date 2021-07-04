using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Automation;

namespace sShell.Classes
{
    class ProcessHelper
    {
        MainWindow mw;

        public void setMW(MainWindow mw)
        {
            this.mw = mw;
        }

        public bool GetCurrentProcesses()
        {
            MessageBox.Show("getting proc!");
            string[] blacklist = { "TextInputHost", "sShell" };
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
              // MessageBox.Show("3");
                if (!String.IsNullOrEmpty(process.MainWindowTitle) && !blacklist.Contains(process.ProcessName))
                {
                    AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                    if (element != null && process.MainWindowHandle != IntPtr.Zero)
                    {
                        /* Let's recreate the following XAML in C#:
                        <Border Margin="0,0,3,0" Width="46" Style="{DynamicResource TBitemPanelBdr}">
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
                                Style = Application.Current.MainWindow.FindResource("TBitemPanelBdr") as Style,
                                ToolTip = process.MainWindowTitle + "\n" + process.ProcessName,
                                Tag = process.Id,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };
                            border.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(mw.FocusWin);
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
                            mw.AddTaskbarItem(border);
                        }
                        catch (Exception error)
                        {
                            Notification notif = new()
                            {
                                Title = "An error has occured!",
                                Description = string.Format("Failed to get program {0}: {1}", process.ProcessName, error.Message),
                                Type = NotificationType.Error
                            };
                            mw.notifHandler.ShowNotification(notif);
                        }
                    }
                }
            }
            return true;
        }
    }
}
