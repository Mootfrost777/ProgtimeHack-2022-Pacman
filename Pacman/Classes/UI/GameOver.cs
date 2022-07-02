using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman.Classes.UI
{
    public class GameOver
    {
        int counter = 0;
        private KeyboardState keyboardState;
        private KeyboardState prevKeyboardState;
        private int selectedItem = 0;

        private string[] menuElements
            = { "play again", "main menu", "quit" };
        
        public void Update()
        {
            keyboardState = Keyboard.GetState();

            // Move in the menu
            if (keyboardState.IsKeyDown(Keys.W) && selectedItem > 0 && keyboardState != prevKeyboardState)
            {
                selectedItem--;
            }
            if (keyboardState.IsKeyDown(Keys.S) && selectedItem < menuElements.Length - 1 && keyboardState != prevKeyboardState)
            {
                selectedItem++;
            }

            // Select menu item
            if (keyboardState.IsKeyDown(Keys.Enter) && keyboardState != prevKeyboardState)
            {
                if (selectedItem == 0)
                {
                    Game1.RestartGame(true);
                    Game1.gameState = GameState.Game;
                    Game1.pacman.Score = 0;
                    Game1.pacman.ExtraLives = 4;
                }
                else if (selectedItem == 1)
                {
                    Game1.gameState = GameState.Menu;
                }
                else if (selectedItem == 2)
                {
                    Environment.Exit(0);
                }
            }
            prevKeyboardState = keyboardState;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (counter <= 30)
            {
                Label label = new Label("GAME OVER", new Vector2(0, 150), Color.Red);
                label.HorizontalCenterGameOver((int)Game1.screenSize.X);

                label.DrawGameOver(spriteBatch);
            }
            else if (counter >= 60)
            {
                counter = 0;
            }
            counter++;

            for (int i = 0; i < menuElements.Length; i++)
            {
                string element = menuElements[i];
                if (selectedItem == i)
                {
                    element = "> " + element + " <";
                }
                Label label = new Label(element,
                    new Vector2(0, 500 + i * 35),
                    Color.White);
                label.HorizontalCenter((int)Game1.screenSize.X);

                label.Draw(spriteBatch);
            }
        }
    }
}
