using System;
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

        private static string SettingsPath = "settings.xml";
    }
}
