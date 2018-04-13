using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameClient
{
    class UpdateChecker //Klasa która odpowiada za sprawdzenie czy sa jakies pliki do zaaktualizowania, czyta tez ich sciezki
    {
        private readonly string fileName = "._files";

        private List<FilesToUpdate> files;

        public List<FilesToUpdate> GetFiles
        {
            get { return files; }
            set { files = value; }
        }


        public void CreatingListOfFiles()
        {
            try
            {
                files = new List<FilesToUpdate>();

                List<string> lines = File.ReadAllLines(fileName).ToList();

                foreach (var line in lines)
                {
                    string[] entries = line.Split(';');

                    FilesToUpdate fileToUpdate = new FilesToUpdate
                    {
                        FilePath = entries[0],
                        ShaHash = entries[1]
                    };

                    files.Add(fileToUpdate);
                }

                foreach (var file in files)
                {
                    MessageBox.Show(file.FilePath);
                    MessageBox.Show(file.ShaHash);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
