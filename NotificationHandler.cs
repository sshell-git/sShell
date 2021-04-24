using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public enum NotificationType : byte
    {
        /// <summary>
        /// Display a default notification.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Display a warning notification.
        /// </summary>
        Warning = 1,
        /// <summary>
        /// Display an error notification.
        /// </summary>
        Error = 2,
        /// <summary>
        /// Display an information notification.
        /// </summary>
        Information = 3
    }

    public enum NotificationButtons : byte
    {
        NoButtons = 0,
        OneButton = 1,
        TwoButtons = 2
    }

    internal struct Notification
    {
        public NotificationType Type;
        public string Title;
        public string Description;
        public NotificationButtons Buttons;
    }

    class NotificationHandler
    {
        MainWindow mw;

        public void setMW(MainWindow mw)
        {
            this.mw = mw;
        }

        public bool ShowNotification(Notification notif)
        {
            // Check notification count and display it in the corner
            int count = mw.notifBox.Children.Count + 1; // +1 because this is the notification we are adding 
            if (count > 8)
            {
                mw.NotifAmountText.Text = "9+";
                mw.NotifAmount.Visibility = Visibility.Visible;
            }
            else if (count == 0)
            {

                mw.NotifAmount.Visibility = Visibility.Hidden;
            }
            else
            {
                mw.NotifAmountText.Text = count.ToString();
                mw.NotifAmount.Visibility = Visibility.Visible;
            }

            // Display the notification in the Quick Actions menu
            Border qaNotif = new()
            {
                Style = mw.FindResource("TBitemPanelBdr") as Style,
                Margin = new Thickness(0, 3, 0, 0),
                Padding = new Thickness(20),
                CornerRadius = new CornerRadius(3)
            };
            DockPanel qaPanel = new();
            PackIconMaterialDesign qaIcon = new();
            switch (notif.Type)
            {
                case NotificationType.Default:
                    qaIcon = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconMaterialDesignKind.Notifications,
                        Foreground = mw.FindResource("bgPrimary") as System.Windows.Media.Brush,
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    break;
                case NotificationType.Error:
                    qaIcon = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconMaterialDesignKind.Error,
                        Foreground = mw.FindResource("bgDanger") as System.Windows.Media.Brush,
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    break;
                case NotificationType.Warning:
                    qaIcon = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconMaterialDesignKind.Warning,
                        Foreground = mw.FindResource("bgWarning") as System.Windows.Media.Brush,
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    break;
                case NotificationType.Information:
                    qaIcon = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconMaterialDesignKind.Warning,
                        Foreground = mw.FindResource("bgInfo") as System.Windows.Media.Brush,
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    break;
            }
            /*
               <Border Style="{DynamicResource TBitemPanelBdr}" Margin="0,3,0,0" Width="275" Padding="20" CornerRadius="3">
                   <DockPanel>
                       <iconPacks:PackIconMaterialDesign 
                       VerticalAlignment="Center" Kind="Notifications" Foreground="{DynamicResource bgPrimary}" Width="30" Height="30" HorizontalAlignment="Left" />
                       <StackPanel Margin="10,0,0,0">
                           <TextBlock Foreground="{DynamicResource FG}" HorizontalAlignment="Left" Text="Notification" FontSize="18" />
                           <TextBlock Foreground="{DynamicResource FG}" HorizontalAlignment="Left" Text="Hello" FontSize="13" />
                       </StackPanel>
                   </DockPanel>
               </Border>
            */
            StackPanel qaStack = new()
            {
                Margin = new Thickness(10, 0, 0, 0)
            };
            TextBlock qaTitle = new()
            {
                Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                Text = notif.Title,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 18
            };
            TextBlock qaDesc = new()
            {
                Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                Text = notif.Description,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 13
            };
            qaNotif.Child = qaPanel;
            qaPanel.Children.Add(qaIcon);
            qaStack.Children.Add(qaTitle);
            qaStack.Children.Add(qaDesc);
            qaPanel.Children.Add(qaStack);
            mw.qaStackParent.Children.Add(qaNotif);
            // Display the notification in the bottom right corner
            switch (notif.Type)
            {
                case NotificationType.Default:
                    /*
                    <Border Style="{DynamicResource Notification}" Margin="0,3,0,0" Width="275" Background="{DynamicResource bgPrimary}" Padding="20" CornerRadius="3">
                        <DockPanel>
                            <iconPacks:PackIconMaterialDesign VerticalAlignment="Center" Kind="Notifications" Foreground="White" Width="30" Height="30" HorizontalAlignment="Left" />
                            <StackPanel Margin="10,0,0,0">
                                <TextBlock Foreground="{DynamicResource FG}" HorizontalAlignment="Left" Text="Notification" FontSize="18" />
                                <TextBlock Foreground="{DynamicResource FG}" HorizontalAlignment="Left" Text="Hello" FontSize="13" />
                            </StackPanel>
                        </DockPanel>
                    </Border>
                    */
                    Border Defbdr = new()
                    {
                        Margin = new Thickness(0, 3, 0, 0),
                        Background = mw.FindResource("bgPrimary") as System.Windows.Media.Brush,
                        Padding = new Thickness(20),
                        CornerRadius = new CornerRadius(3),
                        Style = mw.FindResource("Notification") as Style,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        MinWidth = 400,
                        MinHeight = 100
                    };
                    DockPanel Defpanel = new();
                    PackIconMaterialDesign Deficon = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconMaterialDesignKind.Notifications,
                        Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    StackPanel Defsp = new()
                    {
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    TextBlock Deftitle = new()
                    {
                        Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Text = notif.Title,
                        FontSize = 18
                    };
                    TextBlock Defdesc = new()
                    {
                        Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Text = notif.Description,
                        FontSize = 13
                    };
                    Defbdr.Child = Defpanel;
                    Defpanel.Children.Add(Deficon);
                    Defsp.Children.Add(Deftitle);
                    Defsp.Children.Add(Defdesc);
                    Defpanel.Children.Add(Defsp);
                    mw.notifBox.Children.Add(Defbdr);
                    break;
                case NotificationType.Error:
                    Border Errbdr = new()
                    {
                        Margin = new Thickness(0, 3, 0, 0),
                        Background = mw.FindResource("bgDanger") as System.Windows.Media.Brush,
                        Padding = new Thickness(20),
                        CornerRadius = new CornerRadius(3),
                        Style = mw.FindResource("Notification") as Style,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        MinWidth = 400,
                        MinHeight = 100
                    };
                    DockPanel Errpanel = new();
                    PackIconMaterialDesign Erricon = new()
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconMaterialDesignKind.Error,
                        Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                        Width = 30,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    StackPanel Errsp = new()
                    {
                        Margin = new Thickness(10, 0, 0, 0),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    TextBlock Errtitle = new()
                    {
                        Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Text = notif.Title,
                        FontSize = 18
                    };
                    TextBlock Errdesc = new()
                    {
                        Foreground = mw.FindResource("FG") as System.Windows.Media.Brush,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Text = notif.Description,
                        FontSize = 13
                    };
                    /*
                 <Border Margin="0,3,0,0" Width="275" Background="{DynamicResource bgDanger}" Padding="20" CornerRadius="3">
                    <DockPanel>
                        <iconPacks:PackIconMaterialDesign VerticalAlignment="Center" Kind="Error" Foreground="White" Width="30" Height="30" HorizontalAlignment="Left" />
                        <StackPanel Margin="10,0,0,0">
                            <TextBlock Foreground="{DynamicResource FG}" HorizontalAlignment="Left" Text="Test Notification" FontSize="18" />
                            <TextBlock Foreground="{DynamicResource FG}" HorizontalAlignment="Left" Text="Hello" FontSize="13" />
                        </StackPanel>
                    </DockPanel>
                 </Border>
                 */
                    Errbdr.Child = Errpanel;
                    Errpanel.Children.Add(Erricon);
                    Errsp.Children.Add(Errtitle);
                    Errsp.Children.Add(Errdesc);
                    Errpanel.Children.Add(Errsp);
                    mw.notifBox.Children.Add(Errbdr);
                    break;
            }
            return true;
        }
    }
}
