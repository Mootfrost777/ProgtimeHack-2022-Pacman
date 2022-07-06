using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman_refactored.Interfaces
{
    public interface IBoostable
    {
        int BoostCooldown { get; set; }
    }
}
