using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman.Classes.UI
{
    class Label
    {
        public SpriteFont spriteFont;
        private Color color_default;
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

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

        public void HorizontalRight(int screenWidth)
        {
            Position = new Vector2(screenWidth - spriteFont.MeasureString(Text).X, Position.Y);
        }

        public void ResetColor()
        {
            Color = color_default;
        }

        public void LoadContent(ContentManager Content, string fontName)
        {
            spriteFont = Content.Load<SpriteFont>(fontName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, Text, Position, Color);
        }

        public void SetData(string text, Vector2 position, Color color)
        {
            Text = text;
            Position = position;
            Color = color;
        }
    }
}