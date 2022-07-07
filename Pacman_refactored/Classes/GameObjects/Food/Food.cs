using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman_refactored.Classes.Food
{
    public abstract class Food
    {
        public abstract Texture2D Texture { get; set; }

        public abstract Vector2 Position { get; set; }
        public abstract int Scale { get; set; }
        public abstract int CellSize { get; set; }

        public abstract Rectangle SourceRect { get; set; }
        public abstract Rectangle Boundingbox { get; set; }

        public abstract int Prize { get; set; }

        public abstract bool IsAlive { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                spriteBatch.Draw(Texture, Position, SourceRect, Color.White, 0, new Vector2(CellSize / 2), (float)Scale, SpriteEffects.None, 0);
            }
        }
    }
}
