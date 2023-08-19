/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System.IO;

namespace CoxlinCore
{
    public class FileUtils
    {
        public static string[] FindAllFilesWithExtension(string folderPath, string extension)
        {
            return Directory.GetFiles(folderPath, "*." + extension);
        }
    }
}