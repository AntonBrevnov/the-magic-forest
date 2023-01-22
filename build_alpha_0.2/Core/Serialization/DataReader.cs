using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace build_alpha_0._2.Core
{
    public class DataReader
    {
        public static string LoadData(string FilePath, string DataTag)
        {
            var dataArray = new List<string>();
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    dataArray.Add(reader.ReadLine());
                }
                reader.Close();
            }
            for (int i = 0; i < dataArray.Count; i++)
            {
                if (dataArray[i].Contains(DataTag))
                {
                    string[] data = dataArray[i].Split(' ');
                    if (data.Length > 2)
                    {
                        string str = "";
                        for (int j = 1; j < data.Length; j++)
                            str += data[j] + " ";
                        return str;
                    }
                    else return data.Last();
                }
            }

            return "";
        }
        public static bool HasDataTag(string FilePath, string DataTag)
        {
            var dataArray = new List<string>();
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    dataArray.Add(reader.ReadLine());
                }
                reader.Close();
            }
            for (int i = 0; i < dataArray.Count; i++)
                if (dataArray[i].Contains(DataTag))
                    return true;

            return false;
        }
    }
}
