using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman.Classes.UI
{
    class Label
    {
        public static SpriteFont spriteFont;
        public static SpriteFont gameOverFont;
        private Color color_default;
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public static string fontName = "gameFont";

        public string FontName { set { fontName = value; } }

        public Label()
        {
            Position = new Vector2(100, 100);
            Text = "Label";
            Color = Color.White;
            color_default = Color;
        }

        public Label(string Text, Vector2 Position, Color Color)
        {
            this.Text = Text;
            this.Position = Position;
            this.Color = Color;
            color_default = Color;
        }


        public void HorizontalCenter(int screenWidth)
        {
            Position = new Vector2(screenWidth / 2 - spriteFont.MeasureString(Text).X / 2, Position.Y);
        }

        public void HorizontalCenterGameOver(int screenWidth)
        {
            Position = new Vector2(screenWidth / 2 - gameOverFont.MeasureString(Text).X / 2, Position.Y);
        }

        public void HorizontalRight(int screenWidth)
        {
            Position = new Vector2(screenWidth - spriteFont.MeasureString(Text).X, Position.Y);
        }

        public void ResetColor()
        {
            Color = color_default;
        }

        public static void LoadContent(ContentManager Content)
        {
            spriteFont = Content.Load<SpriteFont>(fontName);
            gameOverFont = Content.Load<SpriteFont>("gameOverFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, Text, Position, Color);
        }

        public void DrawGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameOverFont, Text, Position, Color);
        }
    }
}