using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman_refactored.Classes.UI.Label
{
    public class Label
    {
        public Vector2 Position { get; set; }

        public SpriteFont SpriteFont { get; set; }
        public Color Color { get; set; }

        public string Text { get; set; }

        public Label()
        {
            Position = new Vector2(0, 0);
            Text = "Label";
            Color = Color.White;
        }

        public Label(SpriteFont sprteFont, string text, Vector2 position, Color color)
        {
            SpriteFont = sprteFont;
            Text = text;
            Position = position;
            Color = color;
        }
    }
}
