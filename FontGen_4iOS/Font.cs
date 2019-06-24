using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FontGen_iOS
{
    class Font
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string profileUUID { get; set; }
        public string fontPayloadUUID { get; set; }
        public string profilePayloadUUID { get; set; }
        public string fixedFileName { get; }

        public override string ToString()
        {
            return filePath;
        }

        public Font(string path) {
            filePath = path;
            string[] dirSeperators = path.Split(System.IO.Path.DirectorySeparatorChar);
            fileName = dirSeperators.Last();
            fixedFileName = fileName;
            profileUUID = System.Guid.NewGuid().ToString();
            fontPayloadUUID = System.Guid.NewGuid().ToString();
            profilePayloadUUID = System.Guid.NewGuid().ToString();
        }
    }
}
