using System.Collections.Generic;
using System.IO;

namespace build_alpha_0._2.Core
{
    public class DataWriter
    {
        public static void SaveData(string FilePath, string DataTag, string Data)
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
                    dataArray[i] = DataTag + " " + Data;
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        for (int j = 0; j < dataArray.Count; j++)
                            writer.WriteLine(dataArray[j]);
                        writer.Close();
                    }
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                for (int i = 0; i < dataArray.Count; i++)
                    writer.WriteLine(dataArray[i]);
                writer.WriteLine(DataTag + " " + Data);
                writer.Close();
            }
        }
        public static void SaveData(string FilePath, string DataTag, bool Data)
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
                    dataArray[i] = DataTag + " " + Data;
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        for (int j = 0; j < dataArray.Count; j++)
                            writer.WriteLine(dataArray[j]);
                        writer.Close();
                    }
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                for (int i = 0; i < dataArray.Count; i++)
                    writer.WriteLine(dataArray[i]);
                writer.WriteLine(DataTag + " " + Data);
                writer.Close();
            }
        }
        public static void SaveData(string FilePath, string DataTag, char Data)
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
                    dataArray[i] = DataTag + " " + Data;
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        for (int j = 0; j < dataArray.Count; j++)
                            writer.WriteLine(dataArray[j]);
                        writer.Close();
                    }
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                for (int i = 0; i < dataArray.Count; i++)
                    writer.WriteLine(dataArray[i]);
                writer.WriteLine(DataTag + " " + Data);
                writer.Close();
            }
        }
        public static void SaveData(string FilePath, string DataTag, int Data)
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
                    dataArray[i] = DataTag + " " + Data;
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        for (int j = 0; j < dataArray.Count; j++)
                            writer.WriteLine(dataArray[j]);
                        writer.Close();
                    }
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                for (int i = 0; i < dataArray.Count; i++)
                    writer.WriteLine(dataArray[i]);
                writer.WriteLine(DataTag + " " + Data);
                writer.Close();
            }
        }
        public static void SaveData(string FilePath, string DataTag, float Data)
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
                    dataArray[i] = DataTag + " " + Data;
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        for (int j = 0; j < dataArray.Count; j++)
                            writer.WriteLine(dataArray[j]);
                        writer.Close();
                    }
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                for (int i = 0; i < dataArray.Count; i++)
                    writer.WriteLine(dataArray[i]);
                writer.WriteLine(DataTag + " " + Data);
                writer.Close();
            }
        }
        public static void SaveData(string FilePath, string DataTag, double Data)
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
                    dataArray[i] = DataTag + " " + Data;
                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        for (int j = 0; j < dataArray.Count; j++)
                            writer.WriteLine(dataArray[j]);
                        writer.Close();
                    }
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                for (int i = 0; i < dataArray.Count; i++)
                    writer.WriteLine(dataArray[i]);
                writer.WriteLine(DataTag + " " + Data);
                writer.Close();
            }
        }
    }
}
