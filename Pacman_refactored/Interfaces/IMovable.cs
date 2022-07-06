using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using Pacman_refactored.Enums;
using Pacman_refactored.Classes;

namespace Pacman_refactored.Interfaces
{
    public interface IMovable
    {
        /// <summary>
        /// Move an entity in a given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="map"></param>
        static void DirectionMove(Direction direction, Vector2 position, float speed,
            Map map)
        {
            // Move entity
            if (direction == Direction.Up && map.Matrix[(int)(position.Y / map.CellSize - 0.5), (int)position.X / map.CellSize] == 0)
            {
                position.Y -= speed;
            }
            else if (direction == Direction.Right && map.Matrix[(int)position.Y / map.CellSize, (int)(position.X / map.CellSize + 0.5)] == 0)
            {
                position.X += speed;
            }
            else if (direction == Direction.Down && map.Matrix[(int)(position.Y / map.CellSize + 0.5), (int)position.X / map.CellSize] == 0)
            {
                position.Y += speed;
            }
            else if (direction == Direction.Left && map.Matrix[(int)position.Y / map.CellSize, (int)(position.X / map.CellSize - 0.5)] == 0)
            {
                position.X -= speed;
            }
            

            // Teleporters handling
            foreach (var linkedTeleports in map.Teleports)
            {
                for (int i = 0; i < linkedTeleports.Count; i++)
                {
                    if (linkedTeleports[i][0] == (int)position.Y / 24 &&
                        linkedTeleports[i][1] == (int)position.X / 24)
                    {
                        if (i == 0)
                        {
                            position = new Vector2(linkedTeleports[1][0] * map.CellSize, linkedTeleports[1][1] * map.CellSize);
                        }
                        else
                        {
                            position = new Vector2(linkedTeleports[0][0] * map.CellSize, linkedTeleports[0][1] * map.CellSize);
                        }
                    }
                }
            }

            // Check wall collision
            CheckWallCollision(direction, position, map);
        }

         /// <summary>
         /// Check if entity collides with wall.
         /// </summary>
         /// <param name="direction"></param>
         /// <param name="position"></param>
         /// <param name="map"></param>
         /// <returns name="direction"></returns>
        static Direction CheckWallCollision(Direction direction, Vector2 position, Map map)
        {
            // Check if entity hits into wall
            if (direction == Direction.Up && map.Matrix[(int)(position.Y / map.CellSize - 0.5), (int)position.X / map.CellSize] != 0)
            {
                position.Y = (int)(position.Y / map.CellSize) * map.CellSize + map.CellSize / 2;
                direction = Direction.None;
            }
            else if (direction == Direction.Right && map.Matrix[(int)position.Y / map.CellSize, (int)(position.X / map.CellSize + 0.5)] != 0)
            {
                position.X = (int)(position.X / map.CellSize) * map.CellSize + map.CellSize / 2;
                direction = Direction.None;
            }
            else if (direction == Direction.Down && map.Matrix[(int)(position.Y / map.CellSize + 0.5), (int)position.X / map.CellSize] != 0)
            {
                position.Y = (int)(position.Y / map.CellSize) * map.CellSize + map.CellSize / 2;
                direction = Direction.None;
            }
            else if (direction == Direction.Left && map.Matrix[(int)position.Y / map.CellSize, (int)(position.X / map.CellSize - 0.5)] != 0)
            {
                position.X = (int)(position.X / map.CellSize) * map.CellSize + map.CellSize / 2;
                direction = Direction.None;
            }

            return direction;
        }
    }
}
