using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Pacman_refactored.Classes.Interfaces
{
    public interface IAnimate
    {
        const int FramesCooldown = 10;

        static void Animate(SpriteBatch spriteBatch, Texture2D texture,
            int textureNumber, int textureCount,
            int cellSize, Vector2 position,
            float rotation)
        {
            if (textureNumber == textureCount)
            {
                textureNumber = 0;
            }
            rotation = rotation * (float)Math.PI / 180;

            Rectangle sourceRect = new Rectangle(new Point(0, cellSize * textureNumber), new Point(cellSize));
            spriteBatch.Draw(texture, position, sourceRect, Color.White, rotation, new Vector2(cellSize / 2), 1, SpriteEffects.None, 0);

            textureNumber++;
        }
    }
}
