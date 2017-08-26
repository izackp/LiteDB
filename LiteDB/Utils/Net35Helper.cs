using System;
using System.Text.RegularExpressions;

namespace LiteDB {
    public class Net35Helper {
#if NET35
        public static RegexOptions RegexOptionCompiled = RegexOptions.None;
#else
        public static RegexOptions RegexOptionCompiled = RegexOptions.Compiled;
#endif
    }
}
