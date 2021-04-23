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
    class NotificationHandler
    {
        public enum NotificationType
        {
            Toast = 0,
            Popup = 1,
            Top = 2,
            MessageBox = 3
        }

        internal struct Notification
        {
            public NotificationType Type;
            public string Title;
            public string Description;
            public BitmapImage Icon;
        }

        public bool ShowNotification(Notification notif)
        {
            switch (notif.Type)
            {
                case NotificationType.Top:
                    MessageBox.Show("This is currently not finished. :(");
                    break;
                case NotificationType.Toast:
                    MessageBox.Show("This is currently not finished. :(");
                    break;
                case NotificationType.Popup:
                    MessageBox.Show("This is currently not finished. :(");
                    break;
                case NotificationType.MessageBox:
                    MessageBox.Show(notif.Title, notif.Description);
                    break;
            }
            return true;
        }
    }
}
