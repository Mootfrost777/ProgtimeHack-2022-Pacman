using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Pacman.Classes
{
    public class PathFinder
    {
        private static int[,] Map { get; set; }
        private static List<string> walls = new List<string>()
        {
            "┃", "━", "┏", "┓", "┗", "┛"
        };
        private static int mapWidth;
        private static int mapHeight;

        public PathFinder()
        {
            mapHeight = Map.GetLength(0);
            mapWidth = Map.GetLength(1);
        }

        public static void ConvertMap(string[,] map)
        {
            int[,] res = new int[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (!walls.Contains(map[i, j]))
                    {
                        res[i, j] = 0;
                    }
                    else
                    {
                        res[i, j] = 1;
                    }
                }
            }
            Map = res;
        }

        public List<DirectionsEnum> FindWave(int startX, int startY, int targetX, int targetY)
        {
            bool add = true;
            int[,] cMap = new int[mapHeight, mapWidth];
            int x, y, step = 0;
            for (y = 0; y < mapHeight; y++)
                for (x = 0; x < mapWidth; x++)
                {
                    if (Map[y, x] == 1)
                        cMap[y, x] = -2;
                    else
                        cMap[y, x] = -1;
                }
            cMap[targetY, targetX] = 0;
            while (add == true)
            {
                add = false;
                for (y = 0; y < mapWidth - 1; y++)
                    for (x = 0; x < mapHeight; x++)
                    {
                        if (cMap[x, y] == step)
                        {
                            if (x - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;
                            if (y - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (x + 1 < mapHeight && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                cMap[x + 1, y] = step + 1;
                            if (y + 1 < mapHeight && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                cMap[x, y + 1] = step + 1;
                        }
                    }
                step++;
                add = true;
                if (cMap[startY, startX] != -1)
                    add = false;
                if (step > mapWidth * mapHeight)
                    add = false;
            }
            // Convert cma p to list of directions
            List<DirectionsEnum> directions = new List<DirectionsEnum>();
            int curX = startX;
            int curY = startY;
            while (cMap[curY, curX] != 0)
            {
                if (curX - 1 >= 0 && cMap[curY, curX - 1] == cMap[curY, curX] - 1)
                {
                    directions.Add(DirectionsEnum.Left);
                    curX--;
                }
                else if (curY - 1 >= 0 && cMap[curY - 1, curX] == cMap[curY, curX] - 1)
                {
                    directions.Add(DirectionsEnum.Up);
                    curY--;
                }
                else if (curX + 1 < mapWidth && cMap[curY, curX + 1] == cMap[curY, curX] - 1)
                {
                    directions.Add(DirectionsEnum.Right);
                    curX++;
                }
                else if (curY + 1 < mapHeight && cMap[curY + 1, curX] == cMap[curY, curX] - 1)
                {
                    directions.Add(DirectionsEnum.Down);
                    curY++;
                }
            }
            return directions;
        }
    }
}
