using System;
using System.Collections.Generic;
using System.IO;
using Base;

namespace Logic
{
    public static class Utils
    {
        public static IList<List<string>> LoadAndPurifyLevelFile(List<string> lines)
        {
            var mapFile = new List<List<string>>();
            var map = null as List<string>;
            var processing = false;
            foreach (var line in lines)
            {
                if (line.StartsWith(":"))
                {
                    processing = false;
                    if (map != null)
                    {
                        mapFile.Add(map);
                        map = null;
                    }
                    continue;
                }

                if (line.Length == 0)
                {
                    processing = false;
                    if (map != null)
                    {
                        mapFile.Add(map);
                        map = null;
                    }
                    continue;
                }

                if (line.StartsWith("#") ||
                    line.StartsWith(" ") && line.TrimStart(' ').StartsWith("#"))
                {
                    if (!processing)
                        map = new List<string>();

                    processing = true;
                    map.Add(line);

                    Log.Info("LoadMap", "Line[{0}] Length: {1}", line, line.Length);
                }
            }

            if (processing && map != null)
            {
                mapFile.Add(map);
                map = null;
            }

            return mapFile;
        }

        public static IList<LevelMap> ParseSokFile(List<List<string>> mapsData)
        {
            var i = 0;
            var maps = new List<LevelMap>();
            foreach (var m in mapsData)
            {
                var level = ParseLevelData(m);
                level.LevelID = i++;
                maps.Add(level);
            }

            return maps.AsReadOnly();
        }

        public static LevelMap ParseLevelData(List<string> data)
        {
            var map = new LevelMap();
            var mapData = new List<List<Cell>>();
            for (var r = 0; r < data.Count; ++r)
            {
                var count = data[r].Length;
                var row = new List<Cell>();
                for (var c = 0; c < count; ++c)
                {
                    row.Add(CreateCellOrBlock(map, data[r][c], r, c));
                }

                mapData.Add(row);
            }
            map.SetData(mapData);

            return map;
        }

        public const char Wall = '#';
        public const char Floor = ' ';
        public const char Goal = '.';
        public const char Box = '$';
        public const char BoxOnGoal = '*';
        public const char Pusher = '@';
        public const char PusherOnGoal = '+';
        private static Cell CreateCellOrBlock(LevelMap map, char data, int r, int c)
        {

            var cell = null as Cell;
            if (data == Floor ||
                data == Wall ||
                data == Box ||
                data == Pusher)
                cell = new Cell(map, CellType.Floor, new Point(r, c));

            if (data == Goal ||
                data == BoxOnGoal ||
                data == PusherOnGoal)
                cell = new Cell(map, CellType.Goal, new Point(r, c));

            var block = null as Entity;
            if (data == Wall)
                block = new Wall(cell);

            if (data == Box ||
                data == BoxOnGoal)
                block = new Box(cell);

            if (data == Pusher ||
                data == PusherOnGoal)
                block = new Pusher(cell);


            if (block != null)
                cell.SetEntity(block);

            return cell;
        }
    }

    public static class ActionExt
    {
        public static void SafeInvoke(this Action action)
        {
            if (action == null)
                return;

            try
            {
                action.Invoke();
            }
            catch(Exception err)
            {
                Log.Exception(err);
            }
        }

        public static void SafeInvoke<T>(this Action<T> action, T t)
        {
            if (action == null)
                return;

            try
            {
                action.Invoke(t);
            }
            catch(Exception err)
            {
                Log.Exception(err);
            }

        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 t1, T2 t2)
        {
            if (action == null)
                return;

            try
            {
                action.Invoke(t1, t2);
            }
            catch(Exception err)
            {
                Log.Exception(err);
            }

        }
    }
}
