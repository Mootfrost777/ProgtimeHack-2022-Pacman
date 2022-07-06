using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using Pacman_refactored.Enums;

namespace Pacman_refactored.Interfaces
{
    public interface IControl
    {
        /// <summary>
        /// Get direction from input device data.
        /// </summary>
        /// <returns></returns>
        static Direction GetDirection()
        {
            GamePadState gamepadState = GamePad.GetState(0);
            KeyboardState keyboardState = Keyboard.GetState();

            bool stickUp = false;
            bool stickDown = false;
            bool stickLeft = false;
            bool stickRight = false;

            if (Math.Abs(gamepadState.ThumbSticks.Left.Y) > Math.Abs(gamepadState.ThumbSticks.Left.X) &&
                Math.Abs(gamepadState.ThumbSticks.Left.Y) > 0.25f)
            {
                if (gamepadState.ThumbSticks.Left.Y > 0) stickUp = true;
                else stickDown = true;
            }
            else if (Math.Abs(gamepadState.ThumbSticks.Left.X) > Math.Abs(gamepadState.ThumbSticks.Left.Y) &&
                Math.Abs(gamepadState.ThumbSticks.Left.X) > 0.25f)
            {
                if (gamepadState.ThumbSticks.Left.X > 0) stickRight = true;
                else stickLeft = true;
            }

            if (keyboardState.IsKeyDown(Keys.W) ||
                gamepadState.DPad.Up == ButtonState.Pressed ||
                stickUp)
            {
                return Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.S) ||
                gamepadState.DPad.Down == ButtonState.Pressed ||
                stickDown)
            {
                return Direction.Down;
            }
            else if (keyboardState.IsKeyDown(Keys.A) ||
                gamepadState.DPad.Left == ButtonState.Pressed ||
                stickLeft)
            {
                return Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.D) ||
                gamepadState.DPad.Right == ButtonState.Pressed ||
                stickRight)
            {
                return Direction.Right;
            }
            return Direction.None;
        }
    }
}
