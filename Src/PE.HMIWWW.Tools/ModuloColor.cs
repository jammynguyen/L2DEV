using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.HMIWWW.Core
{
    public static class ModuloColor
    {
        public static string[] Colors = new string[] { "f2a900", "6ba4b8", "00b5e2", "cedc00", "97999b", "c6aa76", "5c462b", "7a9a01", "00587c", "8c4799", "500778", "ffcd00", "ce0037", "971b2f", "425563", "e87722" };
        public static string GetColor(long index)
        {
            return Colors[index % 16];
        }
    }
}
