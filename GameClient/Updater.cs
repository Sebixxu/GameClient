using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Threading;
using System.Windows.Controls;

namespace GameClient
{
    class Updater //Klasa odpowiada za pobieranie pliku zawieracjacego info i kolejnych.
    {
        private static WebClient webClient;

        private static string files = "._files";
        private string fileName;
        private string placeToSave;
        private static bool result = false;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(0);
        private static ProgressBar progressBar;

        private static string uriString = "http://80.211.222.8/patch/files/";
        private static string checkerString = "http://80.211.222.8/patch/files/._files";

        public static bool GetStateOfResult()
        {
            return result;
        }


        public Updater()
        {

        }

        public Updater(ProgressBar progressBar)
        {
            //this.progressBar = progressBar;
        }

        public Updater(string fileName, string placeToSave)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("Filename was empty.");
            if (string.IsNullOrEmpty(placeToSave)) throw new ArgumentNullException("Placetosave was empty.");

            this.fileName = fileName;
            this.placeToSave = placeToSave;
        }

        public bool DownloadingFile()
        {
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(placeToSave));

            using (webClient = new WebClient())
            {
                var uri = new Uri(uriString);

                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;

                MessageBox.Show("Zaczynam pobierać!");

            }

            return false;
        }

        public static void AnyChanges()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    using (webClient = new WebClient())
                    {
                        var uri = new Uri(checkerString);

                        webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                        webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                        MessageBox.Show("Pobieram plik do sprawdzenia akutalizajci");

                        webClient.DownloadFileAsync(uri, files);

                        result = true;
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("EXCEPTION OVER 4K");
                    MessageBox.Show(e.ToString());
                }
            }

            else
            {
                result = false;
            }
        }

        private static void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private static void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            result = !e.Cancelled;

            if (!result)
            {
                MessageBox.Show(e.Error.ToString());
            }

            MessageBox.Show(Environment.NewLine + "Download completed!");
        }

        //public static Updater DownloadTempFile(int timeout, ProgressBar progressBar)
        //{
        //    return new Updater(progressBar).AnyChanges(timeout);
        //}
    }
}
