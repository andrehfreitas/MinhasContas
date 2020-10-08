using MinhasContas.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MinhasContas.Droid
{
    class FileHelper: IFileHelper
    {
        public String GetLocalFilePath(String FileName)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, FileName);
        }
    }
}