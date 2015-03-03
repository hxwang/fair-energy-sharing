using fair_energy_sharing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace fair_energy_sharing.EnergyAssigner
{
    public class PreferLargerDemandAssigner: AssingerBase
    {

        public override void Assign(List<Home> homes)
        {
            var demanders = homes.Where(h => h.HomeType == HomeType.Demander).ToList();
            var suppliers = homes.Where(h => h.HomeType == HomeType.Supplier).ToList();

            if (suppliers.Count != 0 && demanders.Count != 0)
            {
                demanders.Sort(new DemandDecreaseComparator());
                suppliers.Sort(new SupplyDecreaseComparator());

                double totalDemandEnergy = demanders.Sum(d => d.OriginEnergyDemand);
                double totalSupplyEnergy = suppliers.Sum(s => s.OriginEnergySupply);

                if (totalSupplyEnergy <= totalDemandEnergy)
                {
                    suppliers.ForEach(h => h.UpdateSuppliedEnergy(h.OriginEnergySupply));
                    AssignEnergyToDemanders(demanders, totalSupplyEnergy);
                }
                else
                {
                    demanders.ForEach(h => h.UpdateAcquiredEnergy(h.OriginEnergyDemand));
                    AssignEnergyRequestToSuppliers(suppliers, totalDemandEnergy);
                }
            }

            UpdateReputationAndCost(suppliers, demanders);
        }

        /// <summary>
        /// distribute the total energy supply to demanders
        /// rule: demanders which have larger energy demand has higher priority
        /// </summary>
        /// <param name="demanders"></param>
        /// <param name="totalEnergySupply"></param>
        public void AssignEnergyToDemanders(List<Home> demanders, double totalEnergySupply) {
            int i = 0;
            while (totalEnergySupply > 0 && i<demanders.Count) {
                Home d = demanders[i];
                double toAssign = Math.Min(totalEnergySupply, d.OriginEnergyDemand);
                d.UpdateAcquiredEnergy(toAssign);
                totalEnergySupply -= toAssign;
                i++;
            }
            if (totalEnergySupply > 0) {
                throw new Exception("There should be no energy remain!");
            }
        }

        /// <summary>
        /// distribute energy request to each energy suppliers
        /// rule: suppliers which have larger energy supply has higher priority
        /// </summary>
        /// <param name="suppliers"></param>
        /// <param name="totalEnergyDemand"></param>
        public void AssignEnergyRequestToSuppliers(List<Home> suppliers, double totalEnergyDemand) {
            int i = 0;
            while (totalEnergyDemand > 0 && i < suppliers.Count) {
                Home s = suppliers[i];
                double toAssign = Math.Min(s.OriginEnergySupply, totalEnergyDemand);
                s.UpdateSuppliedEnergy(toAssign);
                totalEnergyDemand -= toAssign;
                i++;
            }
        }
    }
}
