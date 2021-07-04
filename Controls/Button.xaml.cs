using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sShell.Controls
{
    /// <summary>
    /// Interaction logic for Button.xaml
    /// </summary>
    [ContentProperty("AdditionalContent")]  
    public partial class Button : UserControl
    {
        public Button()
        {
            InitializeComponent();
            DataContext = this;
        }

        public object AdditionalContent
        {
            get { return (object)GetValue(AdditionalContentProperty); }
            set { SetValue(AdditionalContentProperty, value); }
        }
        public static readonly DependencyProperty AdditionalContentProperty =
            DependencyProperty.Register("AdditionalContent", typeof(object), typeof(Button),
              new PropertyMetadata(null));


        public Thickness CornerRadius
        {
            get { return (Thickness)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(Thickness), typeof(Button), new PropertyMetadata(new Thickness(300)));

        private void Enter (object sender, MouseEventArgs e)
        {
            // To="{DynamicResource clrHover}" From="{DynamicResource clrBGAlt2}" 
            BorderBtn.Background = Application.Current.MainWindow.FindResource("BGhover") as Brush;
        }
        private void Leave (object sender, MouseEventArgs e)
        {
            // To="{DynamicResource clrHoverClick}" From="{DynamicResource clrHover}" 
            BorderBtn.Background = Application.Current.MainWindow.FindResource("BGAlt2") as Brush;
        }
        private void LeftDown (object sender, MouseButtonEventArgs e)
        {
            // To="{DynamicResource clrHoverClick}" From="{DynamicResource clrHover}"
            BorderBtn.Background = Application.Current.MainWindow.FindResource("BGhoverClick") as Brush;
        }
        private void LeftUp (object sender, MouseButtonEventArgs e)
        {
            // To="{DynamicResource clrHover}" From="{DynamicResource clrHoverClick}" 
            BorderBtn.Background = Application.Current.MainWindow.FindResource("BGhover") as Brush;
        }
           //<EventTrigger RoutedEvent = "MouseEnter" >
           //     < BeginStoryboard >
           //         < Storyboard >
           //             < ColorAnimation Storyboard.TargetProperty="Background.Color" To="{DynamicResource clrHover}" From="{DynamicResource clrBGAlt2}" Duration="0:0:0"/>
           //         </Storyboard>
           //     </BeginStoryboard>
           // </EventTrigger>
           // <EventTrigger RoutedEvent = "PreviewMouseLeftButtonDown" >
           //     < BeginStoryboard >
           //         < Storyboard >
           //             < ColorAnimation Storyboard.TargetProperty="Background.Color" To="{DynamicResource clrHoverClick}" From="{DynamicResource clrHover}" Duration="0:0:0"/>
           //         </Storyboard>
           //     </BeginStoryboard>
           // </EventTrigger>
           // <EventTrigger RoutedEvent = "PreviewMouseLeftButtonUp" >
           //     < BeginStoryboard >
           //         < Storyboard >
           //             < ColorAnimation Storyboard.TargetProperty="Background.Color" To="{DynamicResource clrHover}" From="{DynamicResource clrHoverClick}" Duration="0:0:0"/>
           //         </Storyboard>
           //     </BeginStoryboard>
           // </EventTrigger>
           // <EventTrigger RoutedEvent = "MouseLeave" >
           //     < BeginStoryboard >
           //         < Storyboard >
           //             < ColorAnimation Storyboard.TargetProperty="Background.Color" To="{DynamicResource clrBGAlt2}" From="{DynamicResource clrHover}" Duration="0:0:0"/>
           //         </Storyboard>
           //     </BeginStoryboard>
           // </EventTrigger>
    }
}
