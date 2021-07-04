using System;
using System.Text.RegularExpressions;
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
using System.Windows.Shapes;

namespace sShell.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>

    public class SettingItem
    {
        public string NormalPath;
        public string FixedPath;
        public bool IsDir;
    }

    public partial class SettingsWin : Window
    {
        public SettingsWin()
        {
            InitializeComponent();
        }

        private void SelectItem(object sender, RoutedEventArgs e)
        {
            TreeViewItem itm = sender as TreeViewItem;
            MainFrame.Source = new Uri(string.Format("Settings.Views/{0}.xaml", itm.Tag), UriKind.Relative);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            List<SettingItem> list = new();
            MainFrame.Source = new Uri("Settings.Views/General.xaml", UriKind.Relative);

            // TODO: dynamic TreeView based on folder paths — actually I'll just make it myself since doing it dynamically is really hard
            // The below code was causing issues, if anyone can fix it let me know by pull requesting

            /*
            string next;
            string next2;
            bool isDir;
            int IndexOf;
            string toBeCapitalized;
            string capitalized = "Unknown Item";

            foreach (string str in Classes.MiscClasses.GetResourceNames())
            {
                // if page is in views
                if (str.Contains(@"windows/settings.views/"))
                {

                    next = str.Replace("windows/settings.views/", "");
                    // if it is a directory
                    if (next.Contains("/") && next.IndexOf("/") != -1)
                    {
                        // New directory found!
                        SettingItem settingItem = new()
                        {
                            NormalPath = str,
                            FixedPath = next,
                            IsDir = true
                        };
                        list.Add(settingItem);
                        MessageBox.Show(next.Substring(0, next.IndexOf("/")));
                    }
                    else
                    {
                        SettingItem settingItem = new()
                        {
                            NormalPath = str,
                            FixedPath = next,
                            IsDir = false
                        };
                        list.Add(settingItem);
                    }

                }
            }
            foreach (SettingItem itm in list)
            {
                bool dirExists = false;
                next2 = char.ToUpper(itm.FixedPath.First()) + itm.FixedPath.Substring(1).ToLower();
                if (itm.IsDir)
                {
                    toBeCapitalized = itm.FixedPath.Substring(0, itm.FixedPath.IndexOf("/"));
                    capitalized = char.ToUpper(toBeCapitalized.First()) + toBeCapitalized.Substring(1).ToLower();
                }
                // for each item
                MessageBox.Show("for each item");
                foreach (TreeViewItem treeItem in TreeView.Items)
                {
                    // check if item already exists
                    MessageBox.Show(string.Format("check if item already exists\ncomparing {0} to {1}", treeItem.Header.ToString(), next2.Substring(0, next2.IndexOf("/"))));
                    if (treeItem.Header.ToString() == next2.Substring(0, next2.IndexOf("/")))
                    {
                        dirExists = true;
                        treeItem.Items.Add(new TreeViewItem() { Header = next2.Substring(0, next2.IndexOf("/")) });
                    }
                    else dirExists = false;
                }
                // double check the item is a directory
                MessageBox.Show("double check the item is a directory");
                if (itm.IsDir && itm.FixedPath.IndexOf("/") != -1)
                {
                    MessageBox.Show("it is a dir");
                    // if not then create the item
                    MessageBox.Show("if not then create the item");
                    if (!dirExists)
                    {
                        TreeViewItem dirItem = new()
                        {
                            Header = capitalized
                        };
                        TreeView.Items.Add(dirItem);
                    }
                }

                if (!dirExists)
                {
                    MessageBox.Show("Creating Next2 (" + next2 + ")");
                    TreeViewItem treeViewItem = new()
                    {
                        Header = next2
                    };
                    MessageBox.Show("Adding to TreeView");
                    TreeView.Items.Add(next2);
                }
            }
            */
        }
    }
}
