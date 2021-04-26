using System;
using System.IO;
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

namespace SShell.Windows.Settings.Views.Misc
{
    /// <summary>
    /// Interaction logic for Theme.xaml
    /// </summary>
    public partial class Theme : Page
    {
        public Theme()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                ComboBoxItem cbi = cb.SelectedItem as ComboBoxItem;
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri(string.Format("pack://application:,,,/Styles/Themes/{0}.xaml", cbi.Tag), UriKind.RelativeOrAbsolute);
            }
            catch (Exception error)
            {
                MessageBox.Show(string.Format("error\n{0}", error.Message));
            }
        }
    }
}
