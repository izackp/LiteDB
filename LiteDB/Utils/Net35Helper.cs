using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
