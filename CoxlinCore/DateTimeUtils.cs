/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System;

namespace CoxlinCore
{
    public class DateTimeUtils
    {
        public static long ConvertToUnixTimestamp(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }
    }
}