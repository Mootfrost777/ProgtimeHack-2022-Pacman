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
        private static List<Texture2D> textures = new List<Texture2D>();
        
        private string[,] map;
        private int[,] dirs = { { -1, 0 }, { 0, -1 }, { 1, 0 }, { 0, 1 } };

        private void ScanMap()
        {
            Queue<int[]> toVisit = new Queue<int[]>();
            List<int[]> visited = new List<int[]>();
            
            
        }
        
        private Tuple<string, int[,]> GetCellType(int[] cords)
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
                return new Tuple<string, int[,]>("transit", { { cords[0] + dirs[1, 0], cords[1] + dirs[1, 1] },  
                }
            }
        }

            
        public static void LoadContent(ContentManager Content)
        {
            textures.Add(Content.Load<Texture2D>("wall"));
            textures.Add(Content.Load<Texture2D>("food"));
            textures.Add(Content.Load<Texture2D>("energizer"));
            textures.Add(Content.Load<Texture2D>("floor"));
            textures.Add(Content.Load<Texture2D>("graph"));
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
                    Texture2D texture;
                    if (map[i, j] == "*")
                    {
                        texture = textures[1];
                    }
                    else if (map[i, j] == "#")
                    {
                        texture = textures[0];
                    }
                    else if (map[i, j] == "@")
                    {
                        texture = textures[2];
                    }
                    else if (map[i, j] == "&")
                    {
                        texture = textures[4];
                    }
                    else
                    {
                        texture = textures[3];
                    }
                    Rectangle rectangle = new Rectangle(new Point(j * 24, i * 24), new Point(24, 24));
                    spriteBatch.Draw(texture, rectangle, Color.White);
                    
                }
            }
        }
    }
}
