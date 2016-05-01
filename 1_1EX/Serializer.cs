using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using _1_1EX.Model;

namespace _1_1EX
{

    //staticna klasa ~ ne instancira se samo pozoves Serializer.Write/Read
    static class Serializer
    {
        public static void WriteResources( List<Resurs> data)
        {
            WriteToXmlFile<List<Resurs>>(MainWindow.active_map + ".xml", data);
        }

        public static List<Resurs> ReadResources()
        {
            return ReadFromXmlFile<List<Resurs>>(MainWindow.active_map+".xml");
        }

        public static void SaveTip()
        {
            WriteToXmlFile<List<TipResursa>>("tipresursa.xml",MainWindow.tipovi);
        }

        public static List<TipResursa> LoadTip()
        {
            return ReadFromXmlFile<List<TipResursa>>("tipresursa.xml");
        }

        public static void SaveEtiketa()
        {
            WriteToXmlFile<List<Etiketa>>("etiketa.xml", MainWindow.etikete);
        }

        public static List<Etiketa> LoadEtiketa()
        {
            return ReadFromXmlFile<List<Etiketa>>("etiketa.xml");
        }


        private static void WriteToXmlFile<T>(string path, T data) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(path);
                serializer.Serialize(writer, data);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private static T ReadFromXmlFile<T>(string path) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(path);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }
}
