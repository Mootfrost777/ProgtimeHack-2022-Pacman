using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using Pacman_refactored.Classes;
using Pacman_refactored.Enums;
using Pacman_refactored.Classes.Entity;
using Pacman_refactored.Classes.UI;
using Pacman_refactored.Classes.Food;

namespace Pacman_refactored
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Static fields
        public static GameState GameState;
        public static Vector2 ScreenSize;
        /// <summary>
        /// Size of one game cell.
        /// </summary>
        public static int CellSize;
        /// <summary>
        /// Default speed of all game objects.
        /// </summary>
        public static float Speed;
        
        // Static game objects
        public Map Map;

        // Dynamic game onjects
        public Pacman Pacman;
        public List<Food> Foods;
        public List<Ghost> Ghosts;

        // UI
        public MainMenu MainMenu;
        public GameOverMenu GameOverMenu;
        public Hud Hud;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            GameState = GameState.Menu;
            ScreenSize = new Vector2(660, 850);
            CellSize = 24;

            Map = new Map();
            
            Pacman = new Pacman(Speed, CellSize);
            Foods = new List<Food>();
            Ghosts = new List<Ghost>();


            MainMenu = new MainMenu(ScreenSize);
            GameOverMenu = new GameOverMenu(ScreenSize);
            Hud = new Hud(ScreenSize);
        }

        protected override void Initialize()
        {
            // Set screen size.
            _graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            _graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            _graphics.ApplyChanges();

            //
            Map.Load();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
