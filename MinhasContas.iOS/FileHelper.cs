using MinhasContas.iOS;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MinhasContas.iOS
{
    class FileHelper: IFileHelper
    {
        public String GetLocalFilePath(string FileName)
        {
            String docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String libFolder = Path.Combine(docFolder, "..", "Library", "Database");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, FileName);
        }
        
    }
}