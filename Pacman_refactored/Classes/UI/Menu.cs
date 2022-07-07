using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pacman_refactored.Enums;
using Pacman_refactored.Interfaces;
using Mootfrost.Monogame.Label;
using Mootfrost.Monogame.Label.Properties;

namespace Pacman_refactored.Classes.UI
{
    public abstract class Menu : IControl
    {
        public abstract SpriteFont SpriteFont { get; set; }

        public abstract Vector2 Position { get; set; }
        public abstract Vector2 ScreenSize { get; set; }

        public abstract string[] MenuItems { get; set; }
        public abstract int SelectedItem { get; set; }

        public Direction PrevDirection { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            Direction direction = IControl.GetDirection();
            if (direction != PrevDirection)
            {
                if (direction == Direction.Up && SelectedItem > 0)
                {
                    SelectedItem--;
                }
                else if (direction == Direction.Down && SelectedItem < MenuItems.Length - 1)
                {
                    SelectedItem++;
                }
            }
            PrevDirection = direction;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MenuItems.Length; i++)
            {
                string element = MenuItems[i];
                if (SelectedItem == i)
                {
                    element = $">{element}<";
                }
                Label label = new Label(SpriteFont, element, Position, ScreenSize, Color.White);
                label.HorizontalAlignment = HorizontalAlignment.Center;

                label.Draw(spriteBatch);
            }
        }

        public Direction GetDirection()
        {


            return IControl.GetDirection();
        }
    }
}
