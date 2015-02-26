﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;

namespace fair_energy_sharing.EnergyAssigner
{
    public abstract class AssingerBase: IAssigner
    {
        public abstract void Assign(List<Home> homes);

        public virtual void updateDemanderCredits(List<Home> demanders)
        {
            demanders.ForEach(h => { h.Reputation -= h.CurrAcquiredEnergy; });
        }

        public virtual void updateSupplierCredits(List<Home> suppliers) {
            suppliers.ForEach(h => { h.Reputation += h.CurrSuppliedEnergy; });
        }
    }
}
