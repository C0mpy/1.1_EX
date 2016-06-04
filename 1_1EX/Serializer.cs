using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using _1_1EX.Model;

namespace _1_1EX
{

    //staticna klasa ~ ne instancira se samo pozoves Serializer.Write/Read
    static class Serializer
    {

        public static void SaveMapModel()
        {
            
               WriteToXmlFile<List<MapModel>>(MainWindow.active_map + "_mapmodel.xml", MainWindow.map_model[MainWindow.active_map]);
            
           
        }

        public static List<MapModel> LoadMapModel()
        {
            if (!File.Exists(MainWindow.active_map + "_mapmodel.xml"))
            {
                SaveMapModel();
            }
            return ReadFromXmlFile<List<MapModel>>(MainWindow.active_map + "_mapmodel.xml");
        }

        public static void WriteResources()
        {
            WriteToXmlFile<List<Resurs>>("resursi.xml", MainWindow.resursi);
        }

        public static List<Resurs> ReadResources()
        {
            if (!File.Exists("resursi.xml"))
            {
                WriteResources();
            }
            return ReadFromXmlFile<List<Resurs>>("resursi.xml");
        }

        public static void SaveTip()
        {
            WriteToXmlFile<ObservableCollection<TipResursa>>("tipresursa.xml", MainWindow.types);
        }

        public static ObservableCollection<TipResursa> LoadTip()
        {
            if (!File.Exists("tipresursa.xml")){
                SaveTip();
            }
            return ReadFromXmlFile<ObservableCollection<TipResursa>>("tipresursa.xml");
        }

        public static void SaveEtiketa()
        {
            WriteToXmlFile<ObservableCollection<Etiketa>>("etiketa.xml", MainWindow.tags);
        }

        public static ObservableCollection<Etiketa> LoadEtiketa()
        {
            if (!File.Exists("etiketa.xml"))
            {
                SaveEtiketa();
            }
            return ReadFromXmlFile<ObservableCollection<Etiketa>>("etiketa.xml");
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
