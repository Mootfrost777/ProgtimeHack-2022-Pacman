using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman_refactored.Enums;
using System;
using System.Collections.Generic;
using System.Text;

using Mootfrost.Monogame.Label;

namespace Pacman_refactored.Classes.UI
{
    public class MainMenu : Menu
    {
        public Texture2D Logo { get; set; }
        
        public override SpriteFont SpriteFont { get; set; }
        
        public override Vector2 Position { get; set; }
        public override Vector2 ScreenSize { get; set; }
        
        public override string[] MenuItems { get; set; }
        public override int SelectedItem { get; set; }

        public MainMenu(SpriteFont spriteFont, Vector2 position, Vector2 screenSize)
        {
            SpriteFont = spriteFont;
            Position = position;
            ScreenSize = screenSize;
            MenuItems = new string[] { "play", "how to play", "quit" };
            SelectedItem = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Handle enter button
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw high score label
            Label label = new Label(SpriteFont, $"Hi-score:{1}",
                new Vector2(50, 20), ScreenSize, Color.DarkRed);
            label.Draw(spriteBatch);
            
            // Draw logo
            Rectangle desinationRect = new Rectangle(
                new Point((int)ScreenSize.X / 2 - Logo.Width / 2, 100),
                new Point(400, 140));
            spriteBatch.Draw(Logo, desinationRect, Color.White);

            // Draw menu selector
            base.Draw(spriteBatch); 
        }
    }
}
