﻿using System;
using System.Xml;

namespace TetrisB2.Plugins
{
    public class SettingsReader
    {
        public static Tuple<int, int> GetBlocksSize()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PrivateProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "Blocks")
                                    return Tuple.Create<int, int>(int.Parse(elem2.GetAttribute("width").ToString()), int.Parse(elem2.GetAttribute("height").ToString()));
            return null;
        }

        public static Tuple<int, int> GetGridSize()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PrivateProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "GridSize")
                                    return Tuple.Create<int, int>(int.Parse(elem2.GetAttribute("width").ToString()), int.Parse(elem2.GetAttribute("height").ToString()));
            return null;
        }

        public static uint GetRapidFall()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PrivateProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "RapidFall")
                                    return uint.Parse(elem2.GetAttribute("speed").ToString());
            return 1;
        }

        private static string SettingsPath = "settings.xml";
    }
}
