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

        private string infoFile = "._files";
        private List<FilesToUpdate> filesToUpdate;
        private string placeToSave;
        private bool result = false;
        private ProgressBar progressBar;

        private string baseUriString = "http://80.211.222.8/patch/files/";
        private string checkerString = "http://80.211.222.8/patch/files/._files";

        public Downloader()
        {

        }

        public Downloader(ProgressBar progressBar)
        {
            this.progressBar = progressBar;
        }


        public Downloader(List<FilesToUpdate> filesToUpdate, ProgressBar progressBar)
        {
            this.filesToUpdate = filesToUpdate;
            this.progressBar = progressBar;
        }

        public void DownloadingAllFiles()
        {
            foreach (FilesToUpdate file in filesToUpdate)
            {
                DownloadingFile(file);
            }
        }

        public async void DownloadingFile(FilesToUpdate file)
        {
            string uriString = baseUriString + file.FilePath;

            if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    using (webClient = new WebClient())
                    {
                        var uri = new Uri(uriString);

                        webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                        webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                        MessageBox.Show("Pobieram plik" + file.FilePath);

                        //webClient.DownloadFile(uri, file.FilePath);
                        await webClient.DownloadFileTaskAsync(uri, file.FilePath);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                MessageBox.Show("Problem z połączeniem internetowym.");
            }
        }

        public void DownloadingInfoFile()
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

                        webClient.DownloadFile(uri, infoFile);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("EXCEPTION OVER 4K");
                    MessageBox.Show(ex.ToString());
                }
            }

            else
            {
                MessageBox.Show("Problem z połączeniem internetowym.");
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
