using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameClient
{
    class Launcher //Klasa Launchera odpowaida za dzialnie przyciskow
    {
        private static string uriString = "http://80.211.222.8/patch/files/";
        private static string checkerString = "http://80.211.222.8/patch/files/._files";
        private static string files = "._files";
        private static object baton = new object();

        public static void PlayGame(string launcherName)
        {
            Process.Start(launcherName);
        }

        private static void Test(ProgressBar progressBar)
        {
            Updater.AnyChanges();
            //Thread.Sleep(5000);
            //return true;
        }

        public static void CheckUpdates(ProgressBar progressBar)
        {
            File.Delete(files);

            Downloader download = new Downloader(progressBar);
            download.AnyChanges();
        }

        public static void Options(string optionName)
        {
            Process.Start(optionName);
        }

        public static void MainSite(string siteURL)
        {
            Process.Start(siteURL);
        }
    }
}
