/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System.IO;

namespace CoxlinCore
{
    public class DirectoryUtils
    {
        public static string[] BuildFileNamesInFolder(string path, string extension)
        {
            var d = new DirectoryInfo(path);
            var files = d.GetFiles(extension);
            var fileNames = new string[files.Length];
            for (var i = 0; i < files.Length; ++i)
            {
                var file = files[i];
                fileNames[i] = file.Name;
            }

            return fileNames;
        }

        public static void CleanDirectory(string directoryPath)
        {
            DirectoryInfo di = new DirectoryInfo(directoryPath);

            var files = di.GetFiles();
            for (var i = 0; i < files.Length; ++i)
            {
                var file = files[i];
                file.Delete();
            }

            var dirs = di.GetDirectories();
            for (var i = 0; i < dirs.Length; ++i)
            {
                var dir = dirs[i];
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