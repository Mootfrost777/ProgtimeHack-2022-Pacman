using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pacman.Classes
{
    public class Food
    {
        public string FoodType { get; set; }
        
        public Rectangle FoodRectangle { get; set; }
        public Rectangle sourceRectangle { get; set; }
        
        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        
        public int Prize { get; set; }
        public bool isAlive { get; set; }

        public Food(string foodType, Vector2 coordinate)
        {
            FoodType = foodType;
            Position = coordinate;
        }
        public void LoadContent()
        {
            if (FoodType == "Point")
            {   
                sourceRectangle = new Rectangle(new Point(230, 2), new Point(8, 8));
                Prize = 10;
                isAlive = true;
            }
            else if (FoodType == "Energyzer")
            {
                sourceRectangle = new Rectangle(new Point(252, 0), new Point(12, 12));
                Prize = 50;
                isAlive = true;
            }
            else if (FoodType == "Fruit")
            {
                sourceRectangle = new Rectangle(new Point(26, 122), new Point(20, 20));
                Prize = 100;
                isAlive = false;
            }
            FoodRectangle = new Rectangle((int)Position.X, (int)Position.Y, 12, 12);

        }

        int counter = 0;
        public void Update()
        {
            if (isAlive || FoodType == "Fruit")
            {
                if (FoodType == "Fruit" && Game1.pacman.PointsEaten == 70 || Game1.pacman.PointsEaten == 170)
                {
                    isAlive = true;
                }
                if (FoodRectangle.Intersects(Game1.pacman.PacmanRectangle) && isAlive)
                {
                    if (FoodType == "Energyzer")
                    {
                        Game1.pacman.PointsEaten++;
                        MediaPlayer.Play(Game1.intermissionSong);
                        Game1.pacman.EnergyzerTime = 360;
                        for (int i = 0; i < Game1.Ghosts.Count; i++)
                        {
                            Game1.Ghosts[i].AfraidTime = 360;
                        }
                    }
                    else if (FoodType != "Fruit")
                    {
                        Game1.pacman.PointsEaten++;
                        if (!Game1.pacman.IsEnergyzerEffectOn)
                        {
                            Game1.chompSound.Play();
                        }
                    }
                    else if (FoodType == "Fruit")
                    {
                        Game1.fruitEatenSound.Play();
                    }
                    isAlive = false;
                    Game1.pacman.Score += Prize;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                Scale = 12 / sourceRectangle.Width;
                if (FoodType == "Fruit")
                {
                    Scale = 2;
                }
                spriteBatch.Draw(Game1.spriteSheet, Position, sourceRectangle, Color.White, 0, new Vector2(12, 12), (float)Scale, SpriteEffects.None, 0);
            }
        }
    }
}
