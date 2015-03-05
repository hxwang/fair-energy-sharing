using System;
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
            demanders.ForEach(h => { h.UpdateReputation(-h.CurrAcquiredEnergy); });
        }

        public virtual void updateSupplierCredits(List<Home> suppliers) {
            suppliers.ForEach(h => { h.UpdateReputation( h.CurrSuppliedEnergy); });
        }

        public virtual void updateEnergyCost(List<Home> demanders) {
            demanders.ForEach(h => { h.updateEnergyCost(h.OriginEnergyDemand - h.CurrAcquiredEnergy); });
        }

        public void UpdateReputationAndCost(List<Home> suppliers, List<Home> demanders)
        {
            updateDemanderCredits(demanders);
            updateSupplierCredits(suppliers);
            updateEnergyCost(demanders);

        }
    }
}
