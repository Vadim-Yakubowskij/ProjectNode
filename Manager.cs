using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace node1
{
    class Manager
    {
        private readonly string _source = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public const string FileName = "data.xml";
        public const string DirectoryName = "Timelon";
        public string Source => Path.Combine(SourceDirectory, FileName);
        public string SourceDirectory => Path.Combine(_source, DirectoryName);

        private void WriteCardListData(List<Tasklist> data)
        {
            using (StreamWriter writer = new StreamWriter(Source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Tasklist>));

                serializer.Serialize(writer, data);
            }
        }
        private void CreateDataSource()
        {
            Directory.CreateDirectory(SourceDirectory);

            if (File.Exists(Source))
            {
                return;
            }


            File.Create(Source).Close();


            WriteCardListData(new List<Tasklist>());
        }
    }
}
