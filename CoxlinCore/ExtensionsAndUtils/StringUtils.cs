/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System;

namespace CoxlinCore
{
    public static class StringUtils
    {
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string LowercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToLowerInvariant(a[0]);
            return new string(a);
        }

        public static bool Contains(this string s, string check)
        {
            return s.IndexOf(check, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}