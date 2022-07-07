using Microsoft.Xna.Framework;

namespace Pacman_refactored.Classes
{
    /// <summary>
    /// Default game properties and textures paths.
    /// </summary>
    public static class GameProperties
    {
        public static readonly Vector2 ScreenSize = new Vector2(660, 850);

        /// <summary>
        /// Default size of one cell in game.
        /// </summary>
        public const int CellSize = 24;

        /// <summary>
        /// Default entities moving speed.
        /// </summary>
        public const float Speed = 1.5f;


        #region Textures
        // Enviroment
        public const string MapSprite = "Enviroment/map";


        // Entities
        public const string PacmanSprite = "Entities/pacman";

        public const string BlinkySprite = "Entities/Ghosts/blinky";
        public const string PinkySprite = "Entities/Ghosts/pinky";
        public const string InkySprite = "Entities/Ghosts/inky";
        public const string ClydeSprite = "Entities/Ghosts/clyde";


        // Foods
        public const string DotSprite = "Foods/dot";
        public const string EnergizerSprite = "Foods/energizer";

        public const string StrawberrySprite = "Foods/Fruits/strawberry";
        #endregion
    }
}
