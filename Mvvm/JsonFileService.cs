using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Mvvm
{
    public class JsonFileService : IFileService
    {
        public List<Phone> Open(string filename)
        {
            List<Phone> phones;
            var jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Phone>));
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                phones = jsonFormatter.ReadObject(fs) as List<Phone>;
            }

            return phones;
        }

        public void Save(string filename, List<Phone> phonesList)
        {
            var jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Phone>));
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, phonesList);
            }
        }
    }
}