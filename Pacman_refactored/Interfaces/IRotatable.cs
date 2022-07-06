using System;
using System.Collections.Generic;
using System.Text;

using Pacman_refactored.Enums;

namespace Pacman_refactored.Interfaces
{
    public interface IRotatable
    {
        static float Rotate(Direction direction, float currentAngle)
        {
            float angle = 0;
            switch (direction)
            {
                case Direction.Up:
                    angle = 90;
                    break;
                case Direction.Down:
                    angle = 270;
                    break;
                case Direction.Left:
                    angle = 180;
                    break;
                case Direction.Right:
                    angle = 0;
                    break;
                default:
                    angle = currentAngle;
                    break;
            }
            return angle;
        }
    }
}
