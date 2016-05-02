﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
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
            WriteToXmlFile<ObservableCollection<TipResursa>>("tags.xml",MainWindow.types);
        }

        public static ObservableCollection<TipResursa> LoadTip()
        {
            return ReadFromXmlFile<ObservableCollection<TipResursa>>("tipresursa.xml");
        }

        public static void SaveEtiketa()
        {
            WriteToXmlFile<ObservableCollection<Etiketa>>("etiketa.xml", MainWindow.tags);
        }

        public static ObservableCollection<Etiketa> LoadEtiketa()
        {
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
