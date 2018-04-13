using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class FilesToUpdate
    {

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private string shaHash;

        public string ShaHash
        {
            get { return shaHash; }
            set { shaHash = value; }
        }



    }
}
