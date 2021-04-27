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
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SShell.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SShell.Controls;assembly=SShell.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:BetterPopupMenu/>
    ///
    /// </summary>
    [ContentProperty("TheContent")]
    public class BetterPopupMenu : Control
    {
        public static readonly DependencyProperty TheContentProperty = DependencyProperty.Register("TheContent", typeof(object), typeof(BetterPopupMenu), new PropertyMetadata(null));
        public object TheContent
        {
            get { return (object)GetValue(TheContentProperty); }
            set { SetValue(TheContentProperty, value); }
        }

        static BetterPopupMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BetterPopupMenu), new FrameworkPropertyMetadata(typeof(BetterPopupMenu)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //Apply bindings and events
            ContentPresenter TheContentContent = GetTemplateChild("TheContentContent") as ContentPresenter;
            TheContentContent.SetBinding(ContentPresenter.ContentProperty, new Binding("TheContent") { Source = this });
        }
    }
}
