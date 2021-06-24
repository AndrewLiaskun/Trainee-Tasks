// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Newtonsoft.Json;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public static class Serializer
    {

        public static bool TrySave(GameInfo game, string path)
        {
            try
            {
                if (Path.GetExtension(path) == ".xml")
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        var serialize = new DataContractSerializer(typeof(GameInfo));
                        serialize.WriteObject(fs, game);
                    }

                if (Path.GetExtension(path) == ".json")
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        var serialize = new DataContractJsonSerializer(typeof(GameInfo));
                        serialize.WriteObject(fs, game);
                    }

                return true;
            }
            catch
            {

                return false;
            }
        }

        public static bool TryLoad(out GameInfo game, string path)
        {
            game = new GameInfo();
            try
            {

                if (Path.GetExtension(path) == ".xml")
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        using (var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
                        {
                            var serialize = new DataContractSerializer(typeof(GameInfo));
                            game = (GameInfo)serialize.ReadObject(reader, true);
                        }
                    }
                if (Path.GetExtension(path) == ".json")
                    using (var fs = new FileStream(path, FileMode.Open))
                    {
                        var serialize = new DataContractJsonSerializer(typeof(GameInfo));
                        game = (GameInfo)serialize.ReadObject(fs);
                    }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}