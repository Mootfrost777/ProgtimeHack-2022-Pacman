﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Pacman.Classes
{
    public class Ghost
    {
        public int CurrentDirectionTexturesNumber { get; set; }
        public int CurrentTextureNumber { get; set; }
        public float Speed = 1.5f;
        public Vector2 Position { get; set; }
        public bool IsAfraidEffectOn { get; set; }
        public int AfraidTime { get; set; }
        public int AnimationCooldawn { get; set; }
        public bool IsAlive { get; set; }
        public string Obstackles { get; set; }
        public Rectangle GhostRectangle  = new Rectangle();
        public List<List<Rectangle>> CommonTextureSourceRectangles { get; set; }
        public List<Rectangle> AfraidTextureSourceRectangles { get; set; } = new List<Rectangle>();
        public List<List<Rectangle>> DeathTextureSourceRectangles { get; set; } = new List<List<Rectangle>>();
        public double Scale { get; set; }
        public string GhostName { get; set; }
        public Vector2 StartPosition { get; set; }
        public Vector2 AfraidFinishPosition { get; set; }
        public List<DirectionsEnum> Directions { get; set; }
        public DirectionsEnum CurrentDirection { get; set; }
        public PathFinder Finder { get; set; }

        string[] file = File.ReadAllText("map.txt").Replace("\r", "").Split('\n');

        public Ghost(string GhostName)
        {
            Finder = new PathFinder();
            this.GhostName = GhostName;
            Point size = new Point(22, 22);
            Point StartTexturePosition = new Point();
            CurrentDirectionTexturesNumber = 0;
            CurrentTextureNumber = 0;
            IsAfraidEffectOn = false;
            AfraidTime = 0;
            IsAlive = true;
            Obstackles = "┃━┏┓┗┛";
            Directions = new List<DirectionsEnum>();
            if (GhostName == "Blinky")
            {
                StartTexturePosition = new Point(0, 144);
                StartPosition = new Vector2(324, 324);
                AfraidFinishPosition = new Vector2(624, 696);
            }
            else if (GhostName == "Pinky")
            {
                StartTexturePosition = new Point(0, 192);
                StartPosition = new Vector2(276, 348);
                AfraidFinishPosition = new Vector2(48, 696);
            }
            else if (GhostName == "Inky")
            {
                StartTexturePosition = new Point(192, 192);
                StartPosition = new Vector2(372, 348);
                AfraidFinishPosition = new Vector2(624, 48);
            }
            else if (GhostName == "Clyde")
            {
                StartTexturePosition = new Point(0, 216);
                StartPosition = new Vector2(324, 348);
                AfraidFinishPosition = new Vector2(48, 48);
            }
            AfraidTextureSourceRectangles.Add(new Rectangle(new Point(192, 96), size));
            AfraidTextureSourceRectangles.Add(new Rectangle(new Point(216, 96), size));
            Position = StartPosition;
            GhostRectangle = new Rectangle(0, 0, 24, 24);
            CommonTextureSourceRectangles = new List<List<Rectangle>>();
            for (int i = 0; i < 4; i++)
            {
                CommonTextureSourceRectangles.Add(new List<Rectangle>());
                for (int j = 0; j < 2; j++)
                {
                    CommonTextureSourceRectangles[i].Add(new Rectangle(new Point(StartTexturePosition.X + 24 * (i * 2 + j), StartTexturePosition.Y), size));
                }
            }
            StartTexturePosition = new Point(192, 216);
            for (int i = 0; i < 4; i++)
            {
                DeathTextureSourceRectangles.Add(new List<Rectangle>());
                for (int j = 0; j < 2; j++)
                {
                    DeathTextureSourceRectangles[i].Add(new Rectangle(new Point(StartTexturePosition.X + 24 * (i * 2 + j), StartTexturePosition.Y), size));
                }
            }
        }

        public void Update()
        {
            if ((Directions.Count == 0 || CurrentDirection == DirectionsEnum.Stop))
            {
                if (IsAlive && !IsAfraidEffectOn && GhostName == "Blinky")
                {
                    Vector2 PersonalPosition = GetPersonalTargetPosition();
                    Directions = Finder.FindWave((int)Position.X / 24, (int)Position.Y / 24, (int)PersonalPosition.X / 24, (int)PersonalPosition.Y / 24);
                }
                else if (!IsAlive)
                {
                    Directions = Finder.FindWave((int)Position.X / 24, (int)Position.Y / 24, (int)StartPosition.X / 24, (int)StartPosition.Y / 24);
                }
                else if (IsAfraidEffectOn)
                {
                    Directions = Finder.FindWave((int)Position.X/24, (int)Position.Y/24, (int)AfraidFinishPosition.X/24, (int)AfraidFinishPosition.Y/24);
                }
            }
            if ((Position.Y - 12) % 24 == 0 && (Position.X - 12) % 24 == 0 && Directions.Count != 0)
            {
                if (Directions[0] == DirectionsEnum.Up && !Obstackles.Contains(file[(int)(Position.Y / 24 - 1)][(int)Position.X / 24]))
                {
                    CurrentDirection = DirectionsEnum.Up;
                    CurrentDirectionTexturesNumber = 3;
                    Directions.Remove(Directions[0]);
                }
                else if (Directions[0] == DirectionsEnum.Right && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 + 1)]))
                {
                    CurrentDirection = DirectionsEnum.Right;
                    CurrentDirectionTexturesNumber = 0;
                    Directions.Remove(Directions[0]);
                }
                else if (Directions[0] == DirectionsEnum.Down && !Obstackles.Contains(file[(int)(Position.Y / 24 + 1)][(int)Position.X / 24]))
                {
                    CurrentDirection = DirectionsEnum.Down;
                    CurrentDirectionTexturesNumber = 1;
                    Directions.Remove(Directions[0]);
                }
                else if (Directions[0] == DirectionsEnum.Left && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 - 1)]))
                {
                    CurrentDirection = DirectionsEnum.Left;
                    CurrentDirectionTexturesNumber = 2;
                    Directions.Remove(Directions[0]);
                }
                else if (Directions[0] == DirectionsEnum.Stop)
                {
                    CurrentDirection = DirectionsEnum.Stop;
                    Directions.Remove(Directions[0]);
                }
            }
            if (CurrentDirection == DirectionsEnum.Up && !Obstackles.Contains(file[(int)(Position.Y / 24 - 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, Position.Y - Speed);
            }
            else if (CurrentDirection == DirectionsEnum.Right && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 + 0.5)]))
            {
                Position = new Vector2(Position.X + Speed, Position.Y);
            }
            else if (CurrentDirection == DirectionsEnum.Down && !Obstackles.Contains(file[(int)(Position.Y / 24 + 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, Position.Y + Speed);
            }
            else if (CurrentDirection == DirectionsEnum.Left && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 - 0.5)]))
            {
                Position = new Vector2(Position.X - Speed, Position.Y);
            }
            if (CurrentDirection == DirectionsEnum.Up && Obstackles.Contains(file[(int)(Position.Y / 24 - 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, ((int)(Position.Y / 24)) * 24 + 12);
                CurrentDirection = DirectionsEnum.Stop;
            }
            else if (CurrentDirection == DirectionsEnum.Right && Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 + 0.5)]))
            {
                Position = new Vector2(((int)(Position.X / 24)) * 24 + 12, Position.Y);
                CurrentDirection = DirectionsEnum.Stop;
            }
            else if (CurrentDirection == DirectionsEnum.Down && Obstackles.Contains(file[(int)(Position.Y / 24 + 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, ((int)(Position.Y / 24)) * 24 + 12);
                CurrentDirection = DirectionsEnum.Stop;
            }
            else if (CurrentDirection == DirectionsEnum.Left && Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 - 0.5)]))
            {
                Position = new Vector2(((int)(Position.X / 24)) * 24 + 12, Position.Y);
                CurrentDirection = DirectionsEnum.Stop;
            }
            if (CurrentDirection == DirectionsEnum.Left && file[(int)Position.Y / 24][(int)Position.X / 24] == '1')
            {
                Position = new Vector2(648, 348);
            }
            else if (CurrentDirection == DirectionsEnum.Right && file[(int)Position.Y / 24][(int)Position.X / 24] == '2')
            {
                Position = new Vector2(24, 348);
            }
            GhostRectangle.X = (int)Position.X;
            GhostRectangle.Y = (int)Position.Y;
            if (Position.X/24 == StartPosition.X/24 && Position.Y/24 == StartPosition.Y/24 && !IsAlive)
            {
                IsAlive = true;
                Speed /= 4;
            }
            if (AfraidTime == 0 && IsAfraidEffectOn)
            {
                IsAfraidEffectOn = false;
                Directions.Clear();
            }
            else if (AfraidTime != 0 && !IsAfraidEffectOn)
            {
                IsAfraidEffectOn = true;
                Directions.Clear();
            }
            if (AfraidTime != 0)
            {
                AfraidTime--;
            }
            if (AnimationCooldawn == 6)
            {
                CurrentTextureNumber++;
                AnimationCooldawn = 0;
            }
            AnimationCooldawn++;
            if (CurrentTextureNumber > 1)
            {
                CurrentTextureNumber = 0;
            }
            if (GhostRectangle.Intersects(Game1.pacman.PacmanRectangle) && !IsAfraidEffectOn && IsAlive)
            {
                // Pacman died
                if (Game1.pacman.ExtraLives > 0)
                {
                    Game1.deathSong.Play();
                    Game1.RestartGame(false);
                }
                else
                {
                    Game1.UpdateHighScore(Game1.pacman.highScore);
                    Game1.gameState = GameState.GameOver;
                    Game1.deathSong.Play();
                }
            }
            else if (GhostRectangle.Intersects(Game1.pacman.PacmanRectangle) && IsAfraidEffectOn && IsAlive)
            {
                Speed *= 4;
                Debug.WriteLine(200 * Game1.pacman.ghostsEaten);
                Game1.pacman.highScore += 200 * Game1.pacman.ghostsEaten;
                Game1.pacman.ghostsEaten++;
                Game1.ghostDiedSound.Play();
                Directions.Clear();
                IsAlive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Scale = 24 / 22;
            Rectangle CurrentSourceRectangle = new Rectangle();
            if (!IsAfraidEffectOn && IsAlive)
            {
                CurrentSourceRectangle = CommonTextureSourceRectangles[CurrentDirectionTexturesNumber][CurrentTextureNumber];
            }
            else if (!IsAlive)
            {
                CurrentSourceRectangle = DeathTextureSourceRectangles[CurrentDirectionTexturesNumber][CurrentTextureNumber];
            }
            else if (IsAfraidEffectOn)
            {
                CurrentSourceRectangle = AfraidTextureSourceRectangles[CurrentTextureNumber];
            }
            spriteBatch.Draw(Game1.spriteSheet, Position, CurrentSourceRectangle, Color.White, 0, new Vector2(11, 11), (float)Scale, SpriteEffects.None, 0);
        }

        public Vector2 GetPersonalTargetPosition()
        {
            if (GhostName == "Blinky")
            {
                return Game1.pacman.Position;
            }
            else if (GhostName == "Pinky")
            {

            }
            return new Vector2();
        }
    }
}
