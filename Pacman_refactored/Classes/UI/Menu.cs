using System;
using System.Collections.Generic;
using System.Text;

using Pacman_refactored.Enums;
using Pacman_refactored.Interfaces;

namespace Pacman_refactored.Classes.UI
{
    public abstract class Menu : IControl
    {
        public abstract string[] MenuItems { get; set; }
    }
}
