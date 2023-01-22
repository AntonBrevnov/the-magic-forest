using System.Collections.Generic;

namespace build_alpha_0._2.Core
{
    public class GlobalData
    {
        private static Dictionary<string, double> NumericVariables = new Dictionary<string, double>();
        private static Dictionary<string, string> TextVariables = new Dictionary<string, string>();

        public static void AddNumericVariable(string name, double value)
        {
            foreach (var data in NumericVariables)
            {
                if (data.Key == name) return;
            }

            NumericVariables.Add(name, value);
        }
        public static void AddTextVariable(string name, string value)
        {
            foreach (var data in TextVariables)
            {
                if (data.Key == name) return;
            }

            TextVariables.Add(name, value);
        }
        public static void RemoveNumericVariable(string name)
        {
            NumericVariables.Remove(name);
        }
        public static void RemoveTextVariable(string name)
        {
            TextVariables.Remove(name);
        }

        public static void ChangeNumericValue(string name, double newValue)
        {
            NumericVariables.Remove(name);
            NumericVariables.Add(name, newValue);
        }
        public static void ChangeTextValue(string name, string newValue)
        {
            TextVariables.Remove(name);
            TextVariables.Add(name, newValue);
        }

        public static double GetNumericValue(string name)
        {
            foreach (var data in NumericVariables)
            {
                if (data.Key == name) return data.Value;
            }

            return 0;
        }
        public static string GetTextValue(string name)
        {
            foreach (var data in TextVariables)
            {
                if (data.Key == name) return data.Value;
            }

            return null;
        }
    }
}
