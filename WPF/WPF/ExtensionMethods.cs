using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public static class ExtensionMethods
    {
        public static void ExpandBytes(this List<string> source)
        {
            int maxLength = source.Select(str => str.Length).Max();

            for (int i = 0; i < source.Count; i++)
            {
                if (string.IsNullOrEmpty(source[i]))
                    continue;
                int lengthBefore = source[i].Length;
                for (int j = 0; j < maxLength - lengthBefore; j++)
                {
                    source[i] = '0' + source[i];
                }
            }
        }
    }
}
