using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Base.Operation
{
    public class CheckMethod
    {
        public static bool checkDoubleCanConvert(object o)
        {
            return !(o is DBNull || !(o is System.IConvertible));
        }
    }
}
