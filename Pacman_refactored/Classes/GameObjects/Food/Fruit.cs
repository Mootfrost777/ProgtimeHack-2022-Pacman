using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman_refactored.Classes.Food
{
    public class Fruit : Food
    {
        public override Texture2D Texture { get; set; }

        public override Vector2 Position { get; set; }
        public override int Scale { get; set; }
        public override int CellSize { get; set; }

        public override Rectangle SourceRect { get; set; }
        public override Rectangle Boundingbox { get; set; }

        public override int Prize { get; set; }
        public override bool IsAlive { get; set; }
    }
}
