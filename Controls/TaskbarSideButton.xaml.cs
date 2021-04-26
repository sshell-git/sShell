using MahApps.Metro.IconPacks;
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

namespace SShell.Controls
{
    /// <summary>
    /// Interaction logic for TaskbarSideButton.xaml
    /// </summary>
    public partial class TaskbarSideButton : UserControl
    {
        public TaskbarSideButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string IconKind { get; set; }
        public bool isOpen { get; set; }

        private void Enter(object sender, MouseEventArgs e)
        {

        }

        private void Leave(object sender, MouseEventArgs e)
        {
            Icon.Foreground = Application.Current.MainWindow.FindResource("FG") as Brush;
        }

        private void Down(object sender, MouseButtonEventArgs e)
        {
            Icon.Foreground = Application.Current.MainWindow.FindResource("bgPrimary") as Brush;
        }

        private void Up(object sender, MouseButtonEventArgs e)
        {
            Icon.Foreground = Application.Current.MainWindow.FindResource("FG") as Brush;
        }

        public bool MyProperty
        {
            get { return (bool)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(bool), typeof(TaskbarSideButton), new PropertyMetadata(false));
    
    }
}
