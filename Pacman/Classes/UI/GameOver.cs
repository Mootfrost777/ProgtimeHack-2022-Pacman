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
        private Label label = new Label();
        private Label menuLabel = new Label();

        private GamePadState gamePadState;
        private GamePadState prevGamePadState;

        private string[] menuElements
            = { "play again", "main menu", "quit" };
        
        public void LoadContent(ContentManager Content, string fontName)
        {
            label.LoadContent(Content, fontName);
            menuLabel.LoadContent(Content, "gameFont");
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(0);

            bool stickUp = false;
            bool stickDown = false;

            if (Math.Abs(gamePadState.ThumbSticks.Left.Y) > Math.Abs(gamePadState.ThumbSticks.Left.X) &&
                Math.Abs(gamePadState.ThumbSticks.Left.Y) > 0.5f)
            {
                if (gamePadState.ThumbSticks.Left.Y > 0) stickUp = true;
                else stickDown = true;
            }

            if (((keyboardState.IsKeyDown(Keys.W) && keyboardState != prevKeyboardState) ||
                (stickUp && prevGamePadState.ThumbSticks.Left.Y <= 0.5f) ||
                ((gamePadState.DPad.Up == ButtonState.Pressed) && (prevGamePadState.DPad.Up == ButtonState.Released)))
                && selectedItem > 0)
            {
                selectedItem--;
            }
            if (((keyboardState.IsKeyDown(Keys.S) && keyboardState != prevKeyboardState) ||
                (stickDown && prevGamePadState.ThumbSticks.Left.Y >= -0.5f) ||
                ((gamePadState.DPad.Down == ButtonState.Pressed) && (prevGamePadState.DPad.Down == ButtonState.Released)))
                && selectedItem < menuElements.Length - 1)
            {
                selectedItem++;
            }
            
            // Select menu item
            if ((keyboardState.IsKeyDown(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed))
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
            prevGamePadState = gamePadState;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (counter <= 30)
            {
                label.SetData("GAME OVER", new Vector2(0, 150), Color.Red);
                label.HorizontalCenter((int)Game1.screenSize.X);

                label.Draw(spriteBatch);
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
                menuLabel.SetData(element,
                    new Vector2(0, 600 + i * 35),
                    Color.White);
                menuLabel.HorizontalCenter((int)Game1.screenSize.X);

                menuLabel.Draw(spriteBatch);
            }
        }
    }
}
