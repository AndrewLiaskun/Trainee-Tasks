// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

using BattleShips.Misc;

using static BattleShips.Resources.Serialization;

namespace BattleShips.Utils
{
    public static class GameSerializer

    {
        public static bool TrySave<TObject>(TObject players, string path)
        {
            try
            {

                if (Path.GetExtension(path) == XmlExtention)
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    using (var writer = XmlDictionaryWriter.Create(fs, new XmlWriterSettings
                    {
                        Encoding = Encoding.UTF8,
                        IndentChars = "\t\t",
                        Indent = true,
                        NewLineOnAttributes = true,
                        CheckCharacters = true,
                    }))
                    {
                        fs.SetLength(0);
                        var serialize = new DataContractSerializer(typeof(TObject));

                        serialize.WriteObject(writer, players);
                        return true;
                    }

                if (Path.GetExtension(path) == JsonExtention)
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        fs.SetLength(0);
                        var serialize = new DataContractJsonSerializer(typeof(TObject));
                        serialize.WriteObject(fs, players);
                        return true;
                    }
                return false;
            }
            catch
            {

                return false;
            }
        }

        public static bool TryLoad<TObject>(string path, out TObject transfer) where TObject : new()
        {
            transfer = new TObject();
            try
            {

                if (Path.GetExtension(path) == XmlExtention)
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        using (var reader = XmlDictionaryReader.Create(fs))
                        {
                            var serialize = new DataContractSerializer(typeof(TObject));
                            transfer = (TObject)serialize.ReadObject(reader, true);
                            return true;
                        }
                    }
                if (Path.GetExtension(path) == JsonExtention)
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        var serialize = new DataContractJsonSerializer(typeof(TObject));
                        transfer = (TObject)serialize.ReadObject(fs);
                        return true;
                    }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}