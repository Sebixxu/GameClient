using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameClient
{
    class Downloader
    {
        private static WebClient webClient;

        private string files = "._files";
        private string fileName;
        private string placeToSave;
        private bool result = false;
        private ProgressBar progressBar;

        private string uriString = "http://80.211.222.8/patch/files/";
        private string checkerString = "http://80.211.222.8/patch/files/._files";

        public Downloader(ProgressBar progressBar)
        {
            this.progressBar = progressBar;
        }


        public Downloader(string fileName, ProgressBar progressBar)
        {
            this.fileName = fileName;
            this.progressBar = progressBar;
        }

        public void AnyChanges()
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

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            result = !e.Cancelled;

            if (!result)
            {
                MessageBox.Show(e.Error.ToString());
            }

            MessageBox.Show(Environment.NewLine + "Download completed!");
        }

    }
}
