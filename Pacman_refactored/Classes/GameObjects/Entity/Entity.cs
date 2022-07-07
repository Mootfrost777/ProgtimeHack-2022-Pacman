using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Pacman_refactored.Interfaces;
using Pacman_refactored.Enums;

namespace Pacman_refactored.Classes.GameObjects.Entity
{
    public abstract class Entity : IMovable, IRotatable, IAnimate
    {
        public abstract Texture2D Texture { get; set; }

        public abstract Vector2 Position { get; set; }
        public abstract Direction Direction { get; set; }
        public abstract float Rotation { get; set; }

        public abstract int[] StartPosition { get; set; }

        public abstract float Speed { get; set; }
        public abstract bool IsAlive { get; set; }

        public abstract int CellSize { get; set; }
        public abstract Rectangle SourceRect { get; set; }
        public abstract Rectangle Boundingbox { get; set; }

        public abstract int TextureNumber { get; set; }
        public abstract int TextureCount { get; set; }


        public virtual void Update(GameTime gameTime)
        {
            Boundingbox = new Rectangle((int)Position.X, (int)Position.Y, CellSize, CellSize);
            IMovable.DirectionMove(Direction, Position, Speed, Game1.Map);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rotation = IRotatable.Rotate(Direction, Rotation);
            if (Direction != Direction.None)
            {
                IAnimate.Animate(spriteBatch, Texture, TextureNumber, TextureCount, CellSize, Position, Rotation);
            }
            else
            {
                SourceRect = new Rectangle(new Point(0, 0), new Point(CellSize));
                spriteBatch.Draw(Texture, Position, SourceRect, Color.White, Rotation, new Vector2(CellSize / 2), 1, SpriteEffects.None, 0);
            }
        }
    }
}
