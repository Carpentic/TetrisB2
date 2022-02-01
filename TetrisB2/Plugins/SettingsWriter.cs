using System;
using System.Xml;
using Windows.Storage;
using Windows.System;

namespace TetrisB2.Plugins
{
    public class SettingsWriter
    {
        public async static void SetBlocksSize(Tuple<int, int> newSize)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

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
                                }
            doc.Save(file.Path);
        }

        public async static void SetGridSize(Tuple<int, int> newSize)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

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
                                }
            doc.Save(file.Path);
        }

        public async static void SetRapidFall(uint newSpeed)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PrivateProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "RapidFall")
                                    elem2.SetAttribute("speed", newSpeed.ToString());
            doc.Save(file.Path);
        }

        public async static void SetSoundTrackName(string newSoundTrackName)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "SelectedSoundtrack")
                                    elem2.SetAttribute("name", newSoundTrackName);
            doc.Save(file.Path);
        }

        public async static void SetGridResetScore(uint newScore)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "GridResetScore")
                                    elem2.SetAttribute("score", newScore.ToString());
            doc.Save(file.Path);
        }

        public async static void SetSoundVolume(double newVolume)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "Volume")
                                    elem2.SetAttribute("volume", newVolume.ToString());
            doc.Save(file.Path);
        }

        public async static void SetKey(string key, VirtualKey value)
        {
            StorageFile file = await LocalFolder.GetFileAsync(SettingsFileName);

            XmlDocument doc = new XmlDocument();
            doc.Load(file.Path);

            XmlElement root = doc.DocumentElement;

            if (root.HasChildNodes)
                foreach (XmlElement elem in root.ChildNodes)
                    if (elem.Name == "PublicProperties")
                        if (elem.HasChildNodes)
                            foreach (XmlElement elem2 in elem.ChildNodes)
                                if (elem2.Name == "Keys")
                                    elem2.SetAttribute(key, ((uint)value).ToString());

            doc.Save(file.Path);
        }

        private static StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
        private static string SettingsFileName = "settings.xml";
        // Folder : %LocalAppData%\Local\Packages\7038ca0f-88e7-4523-9bc4-ac968bf4fdc8_4ezdgcb9e0xaw\LocalState
    }
}
