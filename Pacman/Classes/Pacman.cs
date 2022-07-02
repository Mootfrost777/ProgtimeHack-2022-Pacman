using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Media;

namespace Pacman
{
    public class Pacman
    {
        public Rectangle PacmanRectangle = new Rectangle();

        public int CurrentTextureNumber;
        public int PointsEaten;
        public int Score;
        public float Rotation;
        public string Direction;
        public float Speed;
        public Vector2 Position;
        public bool IsEnergyzerEffectOn;
        public int EnergyzerTime;
        public int ExtraLives;
        public int AnimationCooldawn;
        public string Obstackles;
        public Rectangle sourceRectangle;
        public Keys PressedKey;
        public double Scale;
        public int Level = 0;
        public int highScore;
        public int ghostsEaten = 1;

        string[] file = File.ReadAllText("map.txt").Replace("\r", "").Split('\n');

        
        public Pacman()
        {
            PointsEaten = 0;
            Score = 0;
            Speed = 1.5f;
            Rotation = 0;
            ExtraLives = 4;
            Position = new Vector2(324, 564);
            CurrentTextureNumber = 0;
            EnergyzerTime = 0;
            Obstackles = "┃━┏┓┗┛";
            IsEnergyzerEffectOn = false;
            PacmanRectangle = new Rectangle(0, 0, (int)(24), (int)(24));
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            if ((PressedKey == Keys.None || Direction == null) && state.GetPressedKeyCount() != 0)
            {
                PressedKey = state.GetPressedKeys()[0];
            }
            if ((Position.Y - 12) % 24 == 0 && (Position.X - 12) % 24 == 0)
            {
                if (PressedKey == Keys.W && !Obstackles.Contains(file[(int)(Position.Y / 24 - 1)][(int)Position.X / 24]))
                {
                    Rotation = 0;
                    PressedKey = Keys.None;
                    if (Direction != "Up")
                    {
                        Direction = "Up";
                    }
                }
                else if (PressedKey == Keys.D && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 + 1)]))
                {
                    Rotation = (float)(0.5 * Math.PI);
                    PressedKey = Keys.None;
                    if (Direction != "Right")
                    {
                        Direction = "Right";
                    }
                }
                else if (PressedKey == Keys.S && !Obstackles.Contains(file[(int)(Position.Y / 24 + 1)][(int)Position.X / 24]))
                {
                    Rotation = (float)(Math.PI);
                    PressedKey = Keys.None;
                    if (Direction != "Down")
                    {
                        Direction = "Down";
                    }
                }
                else if (PressedKey == Keys.A && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 - 1)]))
                {
                    Rotation = (float)(1.5 * Math.PI);
                    PressedKey = Keys.None;
                    if (Direction != "Left")
                    {
                        Direction = "Left";
                    }
                }
            }
            if (Direction == "Up" && !Obstackles.Contains(file[(int)(Position.Y / 24 - 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, Position.Y - Speed);
            }
            else if (Direction == "Right" && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 + 0.5)]))
            {
                Position = new Vector2(Position.X + Speed, Position.Y);
            }
            else if (Direction == "Down" && !Obstackles.Contains(file[(int)(Position.Y / 24 + 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, Position.Y + Speed);
            }
            else if (Direction == "Left" && !Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 - 0.5)]))
            {
                Position = new Vector2(Position.X - Speed, Position.Y);
            }
            if (Direction == "Up" && Obstackles.Contains(file[(int)(Position.Y / 24 - 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, ((int)(Position.Y / 24)) * 24 + 12);
                Direction = null;
            }
            else if (Direction == "Right" && Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 + 0.5)]))
            {
                Position = new Vector2(((int)(Position.X / 24)) * 24 + 12, Position.Y);
                Direction = null;
            }
            else if (Direction == "Down" && Obstackles.Contains(file[(int)(Position.Y / 24 + 0.5)][(int)Position.X / 24]))
            {
                Position = new Vector2(Position.X, ((int)(Position.Y / 24)) * 24 + 12);
                Direction = null;
            }
            else if (Direction == "Left" && Obstackles.Contains(file[(int)Position.Y / 24][(int)(Position.X / 24 - 0.5)]))
            {
                Position = new Vector2(((int)(Position.X / 24)) * 24 + 12, Position.Y);
                Direction = null;
            }
            if (Direction == "Left" && file[(int)Position.Y / 24][(int)Position.X / 24] == '1')
            {
                Position = new Vector2(648, 348);
            }
            else if (Direction == "Right" && file[(int)Position.Y / 24][(int)Position.X / 24] == '2')
            {
                Position = new Vector2(24, 348);
            }
            PacmanRectangle.X = (int)Position.X;
            PacmanRectangle.Y = (int)Position.Y;
            AnimationCooldawn++;
            if (AnimationCooldawn == 4)
            {
                CurrentTextureNumber++;
                AnimationCooldawn = 0;
            }
            if (CurrentTextureNumber > 2)
            {
                CurrentTextureNumber = 0;
            }
            if (EnergyzerTime == 0 && IsEnergyzerEffectOn)
            {
                ghostsEaten = default;
                IsEnergyzerEffectOn = false;
                MediaPlayer.Stop();
                Speed /= 2;
            }
            else if (EnergyzerTime != 0 && !IsEnergyzerEffectOn)
            {
                IsEnergyzerEffectOn = true;
                Speed *= 2;
            }
            if (EnergyzerTime != 0)
            {
                EnergyzerTime--;
            }
            // Pacman completed the level
            if (PointsEaten >= Game1.Foods.Count - 1)
            {
                Game1.RestartGame(true);
            }

            if (Score > highScore)
            {
                highScore = Score;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentTextureNumber == 0)
            {
                sourceRectangle = new Rectangle(new Point(2, 168), new Point(22, 22));
            }
            else if (CurrentTextureNumber == 1)
            {
                sourceRectangle = new Rectangle(new Point(74, 72), new Point(22, 22));
            }
            else if(CurrentTextureNumber == 2)
            {
                sourceRectangle = new Rectangle(new Point(26, 72), new Point(22, 22));
            }
            Scale = 24 / sourceRectangle.Width;
            spriteBatch.Draw(Game1.spriteSheet, Position, sourceRectangle, Color.White, Rotation, new Vector2(12, 12), (float)Scale, SpriteEffects.None, 0);
        }
    }
}