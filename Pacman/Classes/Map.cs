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
    public static class Map
    {
        public static string[,] map;
        
        public static void LoadMap()
        {
            string file = File.ReadAllText("map.txt").Replace("\r", "");
            string[] rows = file.Split('\n');
            map = new string[rows.Length, rows[0].Length];

            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    map[i, j] = rows[i][j].ToString();
                    if (map[i,j] == ".")
                    {
                        Game1.Foods.Add(new Food("Point", new Vector2(24 * j + 12, 24 * i + 12)));
                    }
                    else if(map[i, j] == "O")
                    {
                        Game1.Foods.Add(new Food("Energyzer", new Vector2(24 * j + 12, 24 * i + 12)));
                    }
                    else if (map[i, j] == "C")
                    {
                        Game1.Foods.Add(new Food("Fruit", new Vector2(24 * j + 12, 24 * i + 12)));
                    }
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
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
                        case "┏":
                            pos = new Point(98, 98);
                            size = new Point(24, 26);
                            break;
                        case "┓":
                            pos = new Point(132, 98);
                            size = new Point(12, 26);
                            break;
                        case "┗":
                            pos = new Point(47, 108);
                            size = new Point(24, 12);
                            break;
                        case "┛":
                            pos = new Point(84, 113);
                            size = new Point(16, 7);
                            break;
                        default:
                            pos = new Point(0, 0);
                            size = new Point(0, 0);
                            break;
                    }
                    
                    // Это костыль pro max для нормальной карты, не использовать!!!!!!!
                    Rectangle sourceRect = new Rectangle(pos, size);
                    Rectangle destinationRect = new Rectangle(new Point(j * 24, i * 24), size);
                    spriteBatch.Draw(Game1.spriteSheet,
                                     destinationRect,
                                     sourceRect,
                                     Color.White);   
                    
                }
            }
        }
    }
}
