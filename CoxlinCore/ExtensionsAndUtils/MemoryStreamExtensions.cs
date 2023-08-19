/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System.IO;

namespace CoxlinCore
{
    public static class MemoryStreamExtensions
    {
        public static MemoryStream ToMemoryStream(this byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static MemoryStream ToMemoryStream(this byte[] bytes, int position)
        {
            var memoryStream = new MemoryStream(bytes);
            memoryStream.Position = position;
            return memoryStream;
        }
    }
}