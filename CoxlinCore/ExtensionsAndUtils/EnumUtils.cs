/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System;
using System.Collections.Generic;

namespace CoxlinCore
{
    public static class EnumUtils
    {
        public static IEnumerable<T> GetValues<T>()
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"{enumType.Name} is not an enum type.");
            }
            var enumValues = Enum.GetValues(enumType);
            int enumCount = enumValues.Length;
            var result = new T[enumCount];
            for (int i = 0; i < enumCount; i++)
            {
                result[i] = (T)enumValues.GetValue(i);
            }
            return result;
        }
    }
}