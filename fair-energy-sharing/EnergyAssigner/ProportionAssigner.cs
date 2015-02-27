using fair_energy_sharing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.EnergyAssigner
{
    class ProportionAssigner: AssingerBase
    {
        public override void Assign(List<Home> homes)
        {
            var demanders = homes.Where(h => h.HomeType == HomeType.Demander).ToList();
            var suppliers = homes.Where(h => h.HomeType == HomeType.Supplier).ToList();

            double totalDemandEnergy = demanders.Sum(d => d.OriginEnergyDemand);
            double totalSupplyEnergy = suppliers.Sum(s => s.OriginEnergySupply);

            if (suppliers.Count != 0 && demanders.Count != 0 && totalDemandEnergy!=0 && totalSupplyEnergy!=0)
            {
                

                if (totalSupplyEnergy <= totalDemandEnergy)
                {
                    suppliers.ForEach(h => h.UpdateSuppliedEnergy(h.OriginEnergySupply));
                    DistributeEnergyToDemanders(demanders, totalDemandEnergy, totalSupplyEnergy);
                }
                else {
                    demanders.ForEach(h => h.UpdateAcquiredEnergy(h.OriginEnergyDemand));
                    DistributeEnergyRequestToSuppliers(suppliers, totalDemandEnergy, totalSupplyEnergy);
                
                }
            }

            UpdateReputationAndCost(suppliers, demanders);
        }

        public void DistributeEnergyToDemanders(List<Home> demanders, double totalEnergyDemand, double totalEnergySupply) {
            double energyUnit = totalEnergySupply / totalEnergyDemand;
            demanders.ForEach(d =>
            {
                d.UpdateAcquiredEnergy(d.OriginEnergyDemand * energyUnit);
            });
        }

        public void DistributeEnergyRequestToSuppliers(List<Home> suppliers, double totalEnergyDemand, double totalEnergySupply) {
            double energyUnit = totalEnergyDemand / totalEnergySupply;
            suppliers.ForEach(s =>
            {
                s.UpdateSuppliedEnergy(s.OriginEnergySupply * energyUnit);
            });
        
        }

    }
}
