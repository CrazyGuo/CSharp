using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotnetPark.Commons.NDigester;

namespace Study.MyBatis
{
    public class XmlConvertClass
    {
        public static ItemList GetItemList(string databaseSelect)
        {            
            string rule = databaseSelect + @"\DatabaseRule.xml";
            string data = databaseSelect + @"\DatabaseSelectorConfig.xml";

            Digester dig = new Digester();
            try
            {
                dig.Configure(rule);
            }
            catch (Exception e)
            {
                throw e;
            }
            var list = (ItemList)dig.Parse(data);
            return list;
        }
    }
}
