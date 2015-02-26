using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;

namespace fair_energy_sharing.EnergyAssigner
{

    public class CGAssigner : AssingerBase
    {
        public override void Assign(List<Home> homes)
        {
            SupplyLessThanDemand(homes);
        }

        public void SupplyLessThanDemand(List<Home> homes)
        {

            List<Home> suppliers = homes.Where(h => h.HomeType == HomeType.Supplier).ToList();
            List<Home> demanders = homes.Where(h => h.HomeType == HomeType.Demander).ToList();

            suppliers.ForEach(s => s.UpdateSuppliedEnergy(s.OriginEnergySupply));

            double totalEnergySupply = suppliers.Sum(h => h.OriginEnergySupply);
            double totalEnergyDemand = demanders.Sum(h => h.OriginEnergyDemand);

            Boolean computeLoss = false;
            if (totalEnergySupply > totalEnergyDemand / 2) {
                computeLoss = true;
                totalEnergySupply = totalEnergyDemand - totalEnergySupply;
            }

            demanders.Sort(new DemandIncreaseComparator());

            for (int i = 0; i < demanders.Count; i++) {
                Home h = demanders[i];
                double toAssign = h.OriginEnergyDemand / 2 * (demanders.Count - i);
                if (toAssign <= totalEnergySupply)
                {
                    totalEnergySupply = totalEnergySupply - h.OriginEnergyDemand / 2;
                    h.UpdateAcquiredEnergy(h.OriginEnergyDemand / 2);
                }
                else {
                    double equalAssign = totalEnergySupply / (demanders.Count - i);
                    for (int j = i; j < demanders.Count; j++) {
                        Home remainHome = demanders[j];
                        remainHome.UpdateAcquiredEnergy(equalAssign);
                    }
                    break;
                }
            }

            //if we use distribute loss, then the assigned energy need to update according
            //In specific, it should be updated as h.OriginEnergyDemand - h.CurrAcquiredEnergy
            if (computeLoss) {
                demanders.ForEach(h =>
                {
                    h.UpdateAcquiredEnergy(h.OriginEnergyDemand - h.CurrAcquiredEnergy);
                });
            }

            updateDemanderCredits(demanders);
            updateSupplierCredits(suppliers);
        }

        

       
    }
}

