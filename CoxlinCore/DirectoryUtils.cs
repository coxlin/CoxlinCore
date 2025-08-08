using System.Collections.Generic;
using System.IO;

namespace CoxlinCore
{
    public static class DirectoryUtils
    {
        public static List<string> GetAllFileNamesInFolder(string path, string extension)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles(extension);
            List<string> fileList = new List<string>();
            foreach (FileInfo file in Files)
            {
                fileList.Add(file.Name);
            }
            return fileList;
        }

        public static void CleanDirectory(string directoryPath)
        {
            DirectoryInfo di = new DirectoryInfo(directoryPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public static void CreateDirectoryIfItDoesntExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}