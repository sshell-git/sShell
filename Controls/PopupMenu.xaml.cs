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

namespace SShell.Controls
{
    /// <summary>
    /// Interaction logic for PopupMenu.xaml
    /// </summary>
    [ContentProperty("AdditionalContent")]  
    public partial class PopupMenu : UserControl
    {
        public PopupMenu()
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
            DependencyProperty.Register("AdditionalContent", typeof(object), typeof(PopupMenu),
              new PropertyMetadata(null));
    }

}
