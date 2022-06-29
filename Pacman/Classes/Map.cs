using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;

namespace Pacman.Classes
{
    public class Map
    {
        private static Texture2D texture;
        
        private string[,] map;
        private int[,] dirs = { { -1, 0 }, { 0, -1 }, { 1, 0 }, { 0, 1 } };

        private void ScanMap()
        {
            Queue<int[]> toVisit = new Queue<int[]>();
            List<int[]> visited = new List<int[]>();
            
            
        }
        
        /*private Tuple<string, int[,]> GetCellType(int[] cords)
        {
            bool[] walls = new bool[4];
            for (int i = 0; i < dirs.GetLength(0); i++)            
            {     
                if (map[cords[0] + dirs[i, 0],
                        cords[1] + dirs[i, 1]]
                        == "#")
                {
                    walls[i] = true;
                }
            }

            if (walls[0] == walls[2] && walls[0] == true)
            {
            }
        }*/

            
        public static void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("sprites");
        }

        public void LoadMap()
        {
            string file = File.ReadAllText("map.txt").Replace("\r", "");
            string[] rows = file.Split('\n');
            map = new string[rows.Length, rows[0].Length];

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    map[i, j] = rows[i][j].ToString();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Point pos;
                    Point size;
                    
                    switch (map[i, j])
                    {
                        case "┃":
                            pos = new Point(36, 96);
                            size = new Point(12, 24);
                            break;
                        case "━":
                            pos = new Point(0, 98);
                            size = new Point(24, 12);
                            break;
                        case ".":
                            pos = new Point(510, 0);
                            size = new Point(30, 30);
                            break;
                        case "┏":
                            pos = new Point(98, 98);
                            size = new Point(24, 26);
                            break;
                        case "┓":
                            pos = new Point(325, 241);
                            size = new Point(51, 57);
                            break;
                        default:
                            pos = new Point(0, 0);
                            size = new Point(0, 0);
                            break;
                    }
                    Rectangle sourceRect = new Rectangle(pos, size);
                    Rectangle destinationRect = new Rectangle(new Point(j * 24, i * 24), size);
                    spriteBatch.Draw(texture,
                        destinationRect,
                        sourceRect,
                        Color.White);
                    
                }
            }
        }
    }
}
