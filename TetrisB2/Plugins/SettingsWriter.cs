using System;
using System.Xml;
using Windows.System;

namespace TetrisB2.Plugins
{
    public class SettingsWriter
    {
        public static void SetBlocksSize(Tuple<int, int> newSize)
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
                                {
                                    elem2.SetAttribute("width", newSize.Item1.ToString());
                                    elem2.SetAttribute("height", newSize.Item2.ToString());
                                    return;
                                }
        }

        public static void SetGridSize(Tuple<int, int> newSize)
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
                                {
                                    elem2.SetAttribute("width", newSize.Item1.ToString());
                                    elem2.SetAttribute("height", newSize.Item2.ToString());
                                    return;
                                }

        }

        public static void SetRapidFall(uint newSpeed)
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
                                {
                                    elem2.SetAttribute("speed", newSpeed.ToString());
                                    return;
                                }
        }

        public static void SetSoundTrackName(string newSoundTrackName)
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
                                {
                                    elem2.SetAttribute("name", newSoundTrackName);
                                    return;
                                }
        }

        public static void SetGridResetScore(uint newScore)
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
                                {
                                    elem2.SetAttribute("score", newScore.ToString());
                                    return;
                                }
        }

        public static void SetSoundVolume(double newVolume)
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
                                {
                                    elem2.SetAttribute("volume", newVolume.ToString());
                                    return;
                                }
        }

        public static void SetKey(string key, VirtualKey value)
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
                                    elem2.SetAttribute(key, ((uint)value).ToString());
                                    return;
                                }
            return;
        }

        private static string SettingsPath = "settings.xml";
    }
}
