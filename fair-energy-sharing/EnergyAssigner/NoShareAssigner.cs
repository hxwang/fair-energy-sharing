using fair_energy_sharing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.EnergyAssigner
{
    class NoShareAssigner:AssingerBase
    {

        public override void Assign(List<Home> homes)
        {
            var demanders = homes.Where(h => h.HomeType == HomeType.Demander).ToList();
            var suppliers = homes.Where(h => h.HomeType == HomeType.Supplier).ToList();
            UpdateReputationAndCost(suppliers, demanders);
        }
    }
}
