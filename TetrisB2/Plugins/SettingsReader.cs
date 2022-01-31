using System;
using System.Xml;
using Windows.System;

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

        public static string GetSoundTrackName()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "SelectedSoundtrack")
                                    return elem2.GetAttribute("name").ToString();
            return "tetris_soundtrack.mp3";
        }

        public static uint GetGridResetScore()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "GridResetScore")
                                    return uint.Parse(elem2.GetAttribute("score").ToString());
            return 50;
        }

        public static double GetSoundVolume()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "Volume")
                                    return double.Parse(elem2.GetAttribute("volume").ToString());
            return 50;
        }

        public static TetrisKeys GetKeys()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsPath);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "Keys")
                                {
                                    TetrisKeys res = new TetrisKeys();
                                    res.Down = (VirtualKey)uint.Parse(elem2.GetAttribute("down").ToString());
                                    res.Rotate = (VirtualKey)uint.Parse(elem2.GetAttribute("rotate").ToString());
                                    res.Left = (VirtualKey)uint.Parse(elem2.GetAttribute("left").ToString());
                                    res.Right = (VirtualKey)uint.Parse(elem2.GetAttribute("right").ToString());
                                    res.Pause = (VirtualKey)uint.Parse(elem2.GetAttribute("pause").ToString());
                                    return res;
                                }
            return null;
        }

        private static string SettingsPath = "settings.xml";
    }
}
