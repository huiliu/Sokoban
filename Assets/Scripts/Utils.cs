﻿using System.Collections.Generic;
using System.IO;
using Base;

public static class Utils
{
    public static string ConfigPath = UnityEngine.Application.streamingAssetsPath + "/Configs/";
    public static List<Map> LoadMap(string file)
    {
        var lines = File.ReadAllLines(ConfigPath + file);
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

        return ParseSokFile(mapFile);
    }

    private static List<Map> ParseSokFile(List<List<string>> mapsData)
    {
        var maps = new List<Map>();
        foreach(var m in mapsData)
        {
            var map = new Map();
            var mapData = new List<List<Cell>>();
            for (var r = 0; r < m.Count; ++r)
            {
                var count = m[r].Length;
                var row = new List<Cell>();
                for (var c = 0; c < count; ++c)
                {
                    row.Add(CreateCellOrBlock(map, m[r][c], r, c));
                }

                mapData.Add(row);
            }

            map.SetData(mapData);
            maps.Add(map);
        }

        return maps;
    }

    private static Cell CreateCellOrBlock(Map map, char data, int r, int c)
    {
        const char Wall = '#';
        const char Floor = ' ';
        const char Goal = '.';
        const char Box = '$';
        const char BoxOnGoal = '*';
        const char Pusher = '@';
        const char PusherOnGoal = '+';

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
