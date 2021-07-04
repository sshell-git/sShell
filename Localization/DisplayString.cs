#region Using Statements
using MahApps.Metro.IconPacks;
using System;
using System.IO;
using System.Windows.Threading;
using System.Threading;
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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Automation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

// yes i know i kind of stole this from cairo shell but there was no better way to do this

namespace sShell.Localization
{
    public class DisplayString
    {
        public static List<KeyValuePair<string, string>> Languages = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("English", "en_US"),
            new KeyValuePair<string, string>("Français", "fr_FR")
        };

        public DisplayString()
        {

        }

        private static string getString([CallerMemberName] string stringName = null)
        {
            Dictionary<string, string> lang;
            bool isDefault = false;
            string useLang = MainWindow.Settings.UseLang;

            if (useLang.StartsWith("fr_"))
            {
                lang = Language.fr_FR;
            }
            else
            {
                lang = Language.en_US;
                isDefault = true;
            }

            if (lang.ContainsKey(stringName))
                return lang[stringName];
            else
            {
                // default is en_US - return string from there if not found in set language
                if (!isDefault)
                {
                    lang = Language.en_US;
                    if (lang.ContainsKey(stringName))
                        return lang[stringName];
                }
            }

            return stringName;
        }

    }
}
