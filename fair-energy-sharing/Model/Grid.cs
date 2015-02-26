using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Model
{
    class Grid
    {
        public double UnitEnergyPrice { get; private set; }
        

        public Grid(Config config) {
            this.UnitEnergyPrice = config.UnitEnergyPrice;
        }
    }
}
