using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pacman_refactored.Classes.GameObjects
{
    public class Map
    {
        private Texture2D _texture;

        public int[,] Matrix;
        public List<List<int[]>> Teleports;

        public int CellSize = 24;
    }
}
