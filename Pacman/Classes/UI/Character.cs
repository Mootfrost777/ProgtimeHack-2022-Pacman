using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Pacman.Classes.UI
{
    public class Character
    {
        public string description;
        public Point pos;
        public Point size;
        public Color color;

        public Character(string description, Point pos, Point size, Color color)
        {
            this.description = description;
            this.pos = pos;
            this.size = size;
            this.color = color;
        }
    }
}
