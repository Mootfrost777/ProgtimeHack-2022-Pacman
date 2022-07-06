using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman_refactored.Classes.Interfaces;

namespace Pacman_refactored.Classes
{
    public class Pacman : Entity, IMovable, IRotatable, IAnimate, IControl
    {
        public override Texture2D Texture { get; set; }

        public override Vector2 Position { get; set; }
        public override Direction Direction { get; set; }
        public override float Rotation { get; set; }

        public override int[] StartPosition { get; set; }

        public override float Speed { get; set; }
        public override bool IsAlive { get; set; }


        public override int CellSize { get; set; }
        public override Rectangle SourceRect { get; set; }
        public override Rectangle Boundingbox { get; set; }

        public override int TextureNumber { get; set; }
        public override int TextureCount { get; set; }

        public Pacman(Texture2D texture, int[] startPosition, float speed, int cellSize)
        {
            Texture = texture;
            StartPosition = startPosition;
            Position = new Vector2(startPosition[0] * cellSize, startPosition[1] * cellSize);
            Speed = speed;
            CellSize = cellSize;
            IsAlive = true;
            TextureNumber = 0;
            TextureCount = Texture.Width / cellSize;
            Direction = Direction.Up;
        }
    }
}
