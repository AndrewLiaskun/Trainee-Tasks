// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.IO;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace TicTacToe
{
    public static class Serializer
    {
        public static BoardCell[] JsonLoad(string path)
        {
            BoardCell[] loadedBoard;
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                loadedBoard = (BoardCell[])serializer.Deserialize(file, typeof(BoardCell[]));
            }
            return loadedBoard;
        }

        public static void JsonSave(TicTacToe game, string path)
        {
            var board = game.GetBoard();
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, board.Cells as BoardCell[]);
            }
        }

        public static void XmlSave(TicTacToe game, string path)
        {
            var board = game.GetBoard();
            var formatter = new XmlSerializer(typeof(BoardCell[]));
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, board.Cells);
            }
        }

        public static BoardCell[] XmlLoad(string path)
        {
            BoardCell[] loadedBoard;
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var formatter = new XmlSerializer(typeof(BoardCell[]));
                loadedBoard = (BoardCell[])formatter.Deserialize(fs);
            }
            return loadedBoard;
        }
    }
}