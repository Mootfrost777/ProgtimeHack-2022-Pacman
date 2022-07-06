using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman_refactored.Classes.Interfaces
{
    public interface IBoostable
    {
        int BoostCooldown { get; set; }

        void OnBoost(Entity entity)
        {

        }
    }
}
