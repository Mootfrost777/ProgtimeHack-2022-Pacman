using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Media;

namespace Pacman.Classes.UI
{
    public class Menu
    {
        private Texture2D logo;
        private KeyboardState keyboardState;
        private KeyboardState prevKeyboardState;

        private GamePadState gamePadState;
        private GamePadState prevGamePadState;

        private Label label = new Label();

        private static List<Character> characters = new List<Character>()
        {
            new Character("- shadow       \"blinky\"", new Point(0, 144), new Point(22, 22), Color.Red),
            new Character("- speedy       \"pinky\"", new Point(0, 192), new Point(22, 22), Color.Pink),
            new Character("- bashfull     \"inky\"", new Point(192, 192), new Point(22, 22), Color.Cyan),
            new Character("- pokey        \"clyde\"", new Point(0, 216), new Point(22, 22), Color.Orange)
        };

        private static string[] menuElements
            = { "play", "how to play", "quit" };

        private int selectedItem = 0;

        public void LoadContent(ContentManager content, string fontName)
        {
            logo = content.Load<Texture2D>("logo");
            label.LoadContent(content, fontName);
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
                && selectedItem > 0 )
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
                    Game1.gameState = GameState.Game;
                    Game1.RestartGame(true);
                    Game1.pacman.Score = 0;
                    Game1.pacman.ExtraLives = 4;
                    MediaPlayer.Stop();
                }
                else if (selectedItem == 1)
                {
                    Game1.gameState = GameState.HowToPlay;
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
            label.SetData($"Hi-score:{Game1.pacman.highScore}",
                new Vector2(50, 20),
                Color.DarkRed);

            label.Draw(spriteBatch);
                
            Rectangle desinationRect = new Rectangle(
                new Point((int)Game1.screenSize.X / 2 - 200, 100),
                new Point(400, 140));
            spriteBatch.Draw(logo,
                desinationRect,
                Color.White);
            
            for (int i = 0; i < characters.Count; i++)
            {
                Character character = characters[i];
                Rectangle sorceRect = new Rectangle(character.pos, character.size);
                spriteBatch.Draw(Game1.spriteSheet,
                    new Vector2(60, 270 + i * character.size.Y * 1.3f),
                    sorceRect,
                    Color.White);

                label.SetData(character.description,
                    new Vector2(75 + character.size.X, 270 + i * character.size.Y * 1.3f),
                    character.color);
                label.Draw(spriteBatch);
            }

            for (int i = 0; i < menuElements.Length; i++)
            {
                string element = menuElements[i];
                if (selectedItem == i)
                {
                    element = "> " + element + " <";
                }
                label.SetData(element,
                    new Vector2(0, 600 + i * 35),
                    Color.White);
                label.HorizontalCenter((int)Game1.screenSize.X);

                label.Draw(spriteBatch);
            }
        }
    }
}
