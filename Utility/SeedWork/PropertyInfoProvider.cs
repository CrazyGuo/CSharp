using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace SeedWork
{
    public class PropertyInfoProvider
    {
        public static string Get(Type containerType, string propertyName)
        {
            string value = string.Empty;
            string keyForDisplayValue;

            keyForDisplayValue = string.Format("{0}_{1}", containerType.Name, propertyName);
            try
            {
                value = HttpContext.GetGlobalResourceObject("WebDisplayName", keyForDisplayValue).ToString();
            }
            catch(Exception e)
            {
                value = null ;
            }
            return value;
        }
    }
}
