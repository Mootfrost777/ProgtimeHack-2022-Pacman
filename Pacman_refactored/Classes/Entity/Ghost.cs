using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pacman_refactored.Interfaces;
using Pacman_refactored.Enums;

namespace Pacman_refactored.Classes.Entity
{
    public class Ghost : Entity, IAnimate, IMovable
    {
        public override Texture2D Texture { get; set; }

        public override Vector2 Position { get; set; }
        public override Direction Direction { get; set; }

        public override int[] StartPosition { get; set; }

        public override float Speed { get; set; }
        public override bool IsAlive { get; set; }

        public override int CellSize { get; set; }

        public override Rectangle SourceRect { get; set; }
        public override Rectangle Boundingbox { get; set; }

        public override int TextureNumber { get; set; }
        public override int TextureCount { get; set; }

        public GhostType GhostType { get; set; }

        public Ghost(Texture2D texture, int[] startPosition, float speed, int cellSize, GhostType ghostType)
        {
            Texture = texture;
            TextureNumber = 0;
            TextureCount = Texture.Width / CellSize;
            CellSize = cellSize;
            StartPosition = startPosition;
            Position = new Vector2(startPosition[0] * CellSize, startPosition[1] * CellSize);
            Direction = Direction.None;
            Speed = speed;
            GhostType = ghostType;
        }
    }
}
