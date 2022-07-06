using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Mootfrost.Monogame.Label.Properties;

namespace Mootfrost.Monogame.Label
{
    public class Label
    {
        public Vector2 Position { get; set; }
        public Vector2 EndPosition { get; set; }

        public SpriteFont SpriteFont { get; set; }
        public Color Color { get; set; }

        public string Text { get; set; }

        public float Scale { get; set; } = 1;
        public float Rotation { get; set; } = 0;

        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;
        public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;

        public Label()
        {
            Position = new Vector2(0);
            EndPosition = new Vector2(200);

            Text = "Label";
            Color = Color.White;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprteFont"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="endPosition">Right side of label. Needed for alignments. Can be screen size.</param>
        /// <param name="color"></param>
        public Label(SpriteFont sprteFont, string text, Vector2 position, Vector2 endPosition, Color color)
        {
            SpriteFont = sprteFont;
            Text = text;
            Position = position;
            EndPosition = endPosition;
            Color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprteFont"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="endPosition">Right side of label. Needed for alignments. Can be screen size.</param>
        /// <param name="scale">Needed for scaling font. You can use multiple spritefonts instead.</param>
        /// <param name="color"></param>
        public Label(SpriteFont sprteFont, string text, Vector2 position, Vector2 endPosition, float scale, Color color)
        {
            SpriteFont = sprteFont;
            Text = text;
            Position = position;
            EndPosition = endPosition;
            Scale = scale;
            Color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprteFont"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="endPosition">Right side of label. Needed for alignments. Can be screen size.</param>
        /// <param name="scale">Needed for scaling font. You can use multiple spritefonts instead.</param>
        /// <param name="rotation">Label rotation in radians.</param>
        /// <param name="color"></param>
        public Label(SpriteFont sprteFont, string text, Vector2 position, Vector2 endPosition, float scale, float rotation, Color color)
        {
            SpriteFont = sprteFont;
            Text = text;
            Position = position;
            EndPosition = endPosition;
            Scale = scale;
            Rotation = rotation;
            Color = color;
        }

        private float SetHorizontalAlignment()
        {
            float x = Position.X;
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = Position.X;
                    break;
                case HorizontalAlignment.Center:
                    x = Position.X + EndPosition.X / 2 - SpriteFont.MeasureString(Text).X / 2;
                    break;
                case HorizontalAlignment.Right:
                    x = EndPosition.X - SpriteFont.MeasureString(Text).X;
                    break;
            }
            return x;
        }

        private float SetVericalAlignment()
        {
            float y = Position.Y;
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = Position.Y;
                    break;
                case VerticalAlignment.Center:
                    y = Position.Y + EndPosition.Y / 2 - SpriteFont.MeasureString(Text).Y / 2;
                    break;
                case VerticalAlignment.Bottom:
                    y = EndPosition.Y - SpriteFont.MeasureString(Text).Y;
                    break;
            }
            return y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(SetHorizontalAlignment(),
                                           SetVericalAlignment());

            spriteBatch.DrawString(SpriteFont, Text, position,
                Color, Rotation, new Vector2(SpriteFont.MeasureString(Text).X / 2,
                                             SpriteFont.MeasureString(Text).Y / 2),
                Scale, SpriteEffects.None, 0);
        }
    }
}
