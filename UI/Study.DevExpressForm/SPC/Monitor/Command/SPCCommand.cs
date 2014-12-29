using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.DevExpressForm.SPC.Monitor.Command
{
    public static class SPCCommand
    {
        public static List<SPCCommandbase> GetCommandList()
        {
            List<SPCCommandbase> result = new List<SPCCommandbase>();
            var properties = typeof(SPCCommand).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            foreach(var property in properties)
            {
               result.Add((SPCCommandbase)property.GetValue(null, null));
            }
            return result; 
        }
        public static SPCCommandbase[] GetCommandArray()
        {
            
            var properties = typeof(SPCCommand).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            int count = properties.Length;
            var result = new SPCCommandbase[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = (SPCCommandbase)properties[i].GetValue(null, null);
            }
            return result;
        }
        public static SPCCommandbase SPCRule1 
        { 
            get
            {
                return new spcRule1();
            }
         }
        public static SPCCommandbase SPCRule2
        {
            get
            {
                return new spcRule2();
            }
        }
        public static SPCCommandbase SPCRule3
        {
            get
            {
                return new spcRule3();
            }
        }
        public static SPCCommandbase SPCRule4
        {
            get
            {
                return new spcRule4();
            }
        }
        public static SPCCommandbase SPCRule5
        {
            get
            {
                return new spcRule5();
            }
        }
        public static SPCCommandbase SPCRule6
        {
            get
            {
                return new spcRule6();
            }
        }
        public static SPCCommandbase SPCRule7
        {
            get
            {
                return new spcRule7();
            }
        }
        public static SPCCommandbase SPCRule8
        {
            get
            {
                return new spcRule8();
            }
        }
        public static SPCCommandbase GetCommandfromTitle(string name)
        {
            return (SPCCommandbase)typeof(SPCCommand).GetProperty(name).GetValue(null, null);
        }

    }
}
