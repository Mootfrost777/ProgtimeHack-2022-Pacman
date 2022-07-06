using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman_refactored.Classes
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public enum GameState
    {
        Game,
        Menu,
        GameOver,
        NextLevel,
        HowToPlay,
        Exit
    }

    public enum GhostType
    {
        Blinky,
        Pinky,
        Inky,
        Clyde
    }
}
