using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman.Classes.UI
{
    public class HUD
    {
        private Vector2 position;
        private Label label = new Label();

        public HUD(Vector2 position)
        {
            this.position = position;
        }

        public void LoadContent(ContentManager Content, string fontName)
        {
            label.LoadContent(Content, fontName);
        }

        public void Draw(SpriteBatch spriteBatch, int score, int lives)
        {
            label.SetData($"Score:{score}", position, Color.DarkRed);
            label.Draw(spriteBatch);

            label.SetData($"Hi-score:{Game1.pacman.highScore}", position, Color.DarkRed);
            label.HorizontalRight((int)Game1.screenSize.X);
            label.Draw(spriteBatch);

            label.SetData($"Level:{Game1.pacman.Level + 1}", position + new Vector2(0, 40), Color.White);
            label.HorizontalRight((int)Game1.screenSize.X);
            label.Draw(spriteBatch);

            for (int i = 0; i < lives; i++)
            {
                Rectangle sourceRect = new Rectangle(new Point(288, 72), new Point(44, 46));
                Rectangle destinationRect = new Rectangle(new Point(i * 49, (int)position.Y + 50),
                    new Point(48, 48));
                spriteBatch.Draw(Game1.spriteSheet,
                    destinationRect,
                    sourceRect,
                    Color.White);
            }
        }
    }
}
