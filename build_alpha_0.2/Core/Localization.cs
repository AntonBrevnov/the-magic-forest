using System;
using System.Collections.Generic;

namespace build_alpha_0._2.Core
{
    public class Localization
    {
        private static string localizationType;
        public static string LocaleType
        {
            get { return localizationType; }
            set { localizationType = value; }
        }
        private static Dictionary<string, string> parametrs = 
            new Dictionary<string, string>();

        public static void AddParametr(string name, string value)
        {
            parametrs.Add(name, value);
        }
        public static string GetParametr(string name)
        {
            try
            {
                return parametrs[name];
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Error: localization array doesn't has this parametr [name: {name}]");
                return "null";
            }
        }
    }
}
