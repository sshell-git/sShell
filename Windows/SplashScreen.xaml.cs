using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SShell.Windows
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void ForceLoad(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new();
            mw.Show();
            Close();
        }

        public void Load()
        {
            MainWindow mw = new();
            mw.Show();
            Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                CacheInfo cache = new();
                // comment below line for error testing
                cache.CachedItems = new List<CachedAsset>();
                var Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SShell\Cache";
                if (Directory.Exists(Path))
                {
#if DEBUG
                    //MessageBox.Show(Path + " exists!");
#endif
                }
                else
                {
#if DEBUG
                    //MessageBox.Show(Path + " doesn't exist, creating it.");
#endif
                    Directory.CreateDirectory(Path);
                }
                if (File.Exists(Path + @"\cache.json"))
                {
                    string cacheSettings = File.ReadAllText(Path + @"\cache.json");
                    JsonConvert.DeserializeObject(cacheSettings);
                    LoadingText.Text = "Cache successfully found!\nLoading main window..";
                    Load();
                }
                else
                {
                    cache.Date = JsonConvert.SerializeObject(DateTime.Now);
                    cache.Version = "sc1.0";
                    LoadingText.Text = "Caching images, please wait.\nThis shouldn't take long.";
                    LoadingBar.IsIndeterminate = false;
                    LoadingBar.Value = 0;

                    string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs", "*", SearchOption.AllDirectories);
                    LoadingBar.Maximum = files.Length;

                    foreach (string file in files)
                    {
                        /*
                         <Border Style="{DynamicResource TBitemPanelBdr}">
                            <WrapPanel Style="{DynamicResource TBitemPanel}">
                                <Image Style="{DynamicResource TBitemIcon}" Source="https://cdn.discordapp.com/attachments/767080494269333504/835050490936557568/about-logo2x.png"  />
                                <TextBlock Style="{DynamicResource TBitemText}" Text="Firefox" />
                            </WrapPanel>
                         </Border> 
                        */

                        CachedAsset asset = new();

                        byte[] imageBytes = new UTF8Encoding().GetBytes(file);
                        byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(imageBytes);
                        string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                        PngBitmapEncoder encoder = new();
                        ImageSource FileImg = IconHandler.GetIcon(file, false, false);
                        asset.AssetPath = Path + @"\" + encoded;
                        asset.OriginalPath = file;
                        asset.NameHash = encoded;
                        File.WriteAllBytes(Path + @"\" + encoded, ImageSourceToBytes(encoder, FileImg));
                        cache.CachedItems.Add(asset);
                        LoadingBar.Value += 1;
                        //MessageBox.Show(LoadingBar.Value.ToString());
                    }

                    LoadingText.Text = "Writing cache to file...";
                    File.WriteAllText(Path + @"\cache.json", JsonConvert.SerializeObject(cache));

                    LoadingText.Text = "Loading main window..";
                    LoadingBar.IsIndeterminate = true;
                    LoadingBar.Value = 0;
                    Load();
                }
            }
            catch (Exception err)
            {
                MessageBoxResult result = MessageBox.Show(
                    string.Format("Something went wrong!\n{0}\n{1}\nPress YES if you would like to send this error to the developers and help us improve SShell, otherwise press NO.", err.Message, err.StackTrace),
                    "SShell — Error",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    string x = "https://github.com/datkat21/sshell/issues/new?title=Error%20%22" + Uri.EscapeDataString(err.Message) + "%20when%20loading&body=%23%23%23%20Add%20any%20comments%20or%20information%20about%20what%20happened%20during%20the%20error%3A%0A%0AError%20%22%60" + Uri.EscapeDataString(err.GetType().ToString()) + Uri.EscapeDataString(": ") + Uri.EscapeDataString(err.Message) + "%60%22%20during%20loading.%0A%0A%0A%0AStack%20trace%3A%0A%60%60%60cs%0A" + Uri.EscapeDataString(err.StackTrace) + "%0A%60%60%60";
                    // MessageBox.Show(x);
                    var psi = new ProcessStartInfo
                    {
                        FileName = x,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                LoadingText.Text = "An error occured, aborting.";
                LoadingBar.IsIndeterminate = true;
                LoadingBar.Value = 0;
                Load();
            }
        }


        public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;

            if (imageSource is BitmapSource bitmapSource)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using var stream = new MemoryStream();
                encoder.Save(stream);
                bytes = stream.ToArray();
            }

            return bytes;
        }

    }

        public class CacheInfo
        {
            [JsonProperty("version")]
            public string Version { get; set; }
            [JsonProperty("date")]
            public string Date { get; set; }
            [JsonProperty("items")]
            public List<CachedAsset> CachedItems { get; set; }
        }

        public class CachedAsset
        {
            [JsonProperty("originalPath")]
            public string OriginalPath { get; set; }
            [JsonProperty("hash")]
            public string NameHash { get; set; }
            [JsonProperty("cachedPath")]
            public string AssetPath { get; set; }
        }
}
