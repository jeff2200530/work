using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOutput.Extension_Methods
{
    public static class StringReplace
    {
        public static string ReplaceOne(this string str, string oldStr, string newStr) { 
            StringBuilder sb = new StringBuilder(str);
            int index = str.IndexOf(oldStr);
            if (index > -1)
                return str.Substring(0, index) + newStr + str.Substring(index + oldStr.Length);
            return str;
        }
    }
}
