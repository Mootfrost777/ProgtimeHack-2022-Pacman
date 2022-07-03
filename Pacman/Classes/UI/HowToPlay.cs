using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman.Classes.UI
{
    public class HowToPlay
    {
        private Label label = new Label();
        private static string rules =
            @"
Your task is to eat all -
food(dots) and not to be eaten by ghosts.
Use this keys to navigate through the map:
W - Up
A - Left
S - Down
D - Right
            
If you ate big dot(energizer), ghosts will run -
away rom you and you can eat them.

Press Escape to retutn to main menu.
            ";

        public void LoadContent(ContentManager Content, string fontName)
        {
            label.LoadContent(Content, fontName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            label.SetData(rules,
                new Vector2(0, 50),
                Color.White);
            label.Draw(spriteBatch);
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Game1.gameState = GameState.Menu;
            }
        }
    }
}
