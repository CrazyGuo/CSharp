using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCodes.UnitTest
{
    public class MyStringClass
    {
        public string Reverse(string content)
        {
            return new string(content.Reverse().ToArray());
        }
    }
}
