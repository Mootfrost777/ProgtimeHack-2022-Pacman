using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using Pacman.Classes;
using Pacman.Classes.UI;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Threading;

using System.IO;

namespace Pacman
{
    public enum GameState
    {
        Game, Menu, GameOver, NextLevel, HowToPlay
    }

    public class Game1 : Game
    {
        public static GameState gameState = GameState.Menu;
        public static Vector2 screenSize = new Vector2(672, 850);
        public static Texture2D spriteSheet;

        // Default fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public int Level = 0;
        public static Pacman pacman = new Pacman();

        public static List<Food> Foods = new List<Food>();
        public static List<Ghost> Ghosts = new List<Ghost>();

        // UI fields
        private Menu menu;
        private HUD hud;
        private GameOver gameOver;
        private HowToPlay howToPlay;

        // Music fields
        public static SoundEffect deathSong;
        public static SoundEffect chompSound;
        public static SoundEffect ghostDiedSound;
        public static SoundEffect fruitEatenSound;


        public static Song mainSong;
        public static Song intermissionSong;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Set window size
            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            _graphics.ApplyChanges();

            // Initialize map and convert map for pathfinder
            Map.LoadMap();
            PathFinder.ConvertMap(Map.map);

            // Add ghosts
            Ghosts.Add(new Ghost("Blinky"));
            Ghosts.Add(new Ghost("Pinky"));
            Ghosts.Add(new Ghost("Inky"));
            Ghosts.Add(new Ghost("Clyde"));
            pacman.highScore = LoadHighScore();

            // Initialize menu and HUD
            menu = new Menu();
            hud = new HUD(new Vector2(0, 740));
            gameOver = new GameOver();
            howToPlay = new HowToPlay();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load sprites
            spriteSheet = Content.Load<Texture2D>("sprites");
            menu.LoadContent(Content, "gameFont");
            hud.LoadContent(Content, "gameFont");
            howToPlay.LoadContent(Content, "rulesFont");
            gameOver.LoadContent(Content, "gameOverFont");
            
            for (int i = 0; i < Foods.Count; i++)
            {
                Foods[i].LoadContent();
            }

            // Load music
            mainSong = Content.Load<Song>("mainSong");
            deathSong = Content.Load<SoundEffect>("deathSound");
            chompSound = Content.Load<SoundEffect>("chompSound");
            intermissionSong = Content.Load<Song>("intermissionSound");
            ghostDiedSound = Content.Load<SoundEffect>("ghostDeathSound");
            fruitEatenSound = Content.Load<SoundEffect>("fruitEaten");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            MediaPlayer.Play(mainSong);
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume = 40;
        }

        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(0);
            KeyboardState keyboardState = Keyboard.GetState();

            if ((gamePadState.Buttons.Start == ButtonState.Pressed) ||
                keyboardState.IsKeyDown(Keys.Escape))
            {
                gameState = GameState.Menu;
                RestartGame(true);
                pacman.Score = 0;
                pacman.ExtraLives = 4;
            }
            
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update();
                    break;
                case GameState.Game:
                    UpdateGame();
                    break;
                case GameState.GameOver:
                    gameOver.Update();
                    break;
                case GameState.HowToPlay:
                    howToPlay.Update();
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdateGame()
        {
            for (int i = 0; i < Foods.Count; i++)
            {
                Foods[i].Update();
            }
            for (int i = 0; i < Ghosts.Count; i++)
            {
                Ghosts[i].Update();
            }
            pacman.Update();
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space))
            {
                int a = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(_spriteBatch);
                    break;
                case GameState.Game:
                    DrawGame();
                    break;
                case GameState.GameOver:
                    gameOver.Draw(_spriteBatch);
                    break;
                case GameState.HowToPlay:
                    howToPlay.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGame()
        {
            // Draw map and HUD
            Map.Draw(_spriteBatch);
            hud.Draw(_spriteBatch, pacman.Score, pacman.ExtraLives);

            for (int i = 0; i < Foods.Count; i++)
            {
                Foods[i].Draw(_spriteBatch);
            }
            for (int i = 0; i < Ghosts.Count; i++)
            {
                Ghosts[i].Draw(_spriteBatch);
            }
            pacman.Draw(_spriteBatch);
        }

        public static void RestartGame(bool IsLevelComplete)
        {
            if (IsLevelComplete)
            {
                for (int i = 0; i < Foods.Count; i++)
                {
                    if (Foods[i].FoodType != "Fruit")
                    {
                        Foods[i].isAlive = true;
                    }
                    else
                    {
                        Foods[i].Prize += 200;
                    }
                }
                pacman.PointsEaten = 0;
            }
            else
            {
                pacman.ExtraLives -= 1;
            }
            pacman.Position = new Vector2(324, 564);
            for (int i = 0; i < Ghosts.Count; i++)
            {
                Ghosts[i].Position = Ghosts[i].StartPosition;
                Ghosts[i].AfraidTime = 0;
                Ghosts[i].IsAfraidEffectOn = false;
                Ghosts[i].IsAlive = true;
                Ghosts[i].Directions.Clear();
                Ghosts[i].CurrentDirection = DirectionsEnum.Stop;
                Ghosts[i].Speed = 1.5f;
            }
        }

        public static void UpdateHighScore(int highScore)
        {
            File.WriteAllText("highScore.txt", highScore.ToString());
        }
        
        public static int LoadHighScore()
        {
            if (!File.Exists("highScore.txt"))
                File.Create("highscore.txt").Dispose();
            
            string data = File.ReadAllText("highScore.txt");

            if (string.IsNullOrEmpty(data)) return 0;
            return int.Parse(data);
        }
    }
}
