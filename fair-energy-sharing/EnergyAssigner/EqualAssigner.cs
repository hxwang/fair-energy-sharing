using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;

namespace fair_energy_sharing.EnergyAssigner
{
    class EqualAssigner: AssingerBase
    {

        public override void Assign(List<Home> homes)
        {
            var demanders = homes.Where(h => h.HomeType == HomeType.Demander).ToList();
            var suppliers = homes.Where(h => h.HomeType == HomeType.Supplier).ToList();

            double totalDemandEnergy = demanders.Sum(d => d.OriginEnergyDemand);
            double totalSupplyEnergy = suppliers.Sum(s => s.OriginEnergySupply);

            if (suppliers.Count != 0 && demanders.Count != 0 && totalDemandEnergy != 0 && totalSupplyEnergy != 0)
            {


                if (totalSupplyEnergy <= totalDemandEnergy)
                {
                    suppliers.ForEach(h => h.UpdateSuppliedEnergy(h.OriginEnergySupply));
                    DistributeEnergyToDemanders(demanders, totalDemandEnergy, totalSupplyEnergy);
                }
                else
                {
                    demanders.ForEach(h => h.UpdateAcquiredEnergy(h.OriginEnergyDemand));
                    DistributeEnergyRequestToSuppliers(suppliers, totalDemandEnergy, totalSupplyEnergy);

                }
            }

            UpdateReputationAndCost(suppliers, demanders);
        }

        public void DistributeEnergyToDemanders(List<Home> demanders, double totalEnergyDemand, double totalEnergySupply)
        {

            while (totalEnergySupply > 1e-3)
            {
                //Console.WriteLine("totalEnergySupply ={0}",totalEnergySupply);
                int count = demanders.Where(d => d.OriginEnergyDemand - d.CurrAcquiredEnergy  >1e-3).Count();
                if (count == 0) break;
                double share = totalEnergySupply / count;
                demanders.ForEach(d =>
                {
                    if (d.CurrAcquiredEnergy <d.OriginEnergyDemand ) {
                        double toAssign = Math.Min(share, d.OriginEnergyDemand - d.CurrAcquiredEnergy);
                        d.UpdateAcquiredEnergy(d.CurrAcquiredEnergy + toAssign);
                        totalEnergySupply = totalEnergySupply - toAssign;
                    }
                    
                });
            }
        }

        public void DistributeEnergyRequestToSuppliers(List<Home> suppliers, double totalEnergyDemand, double totalEnergySupply)
        {
            while (totalEnergyDemand > 1e-3)
            {
                //Console.WriteLine("totalEnergyDemand ={0}", totalEnergyDemand);
                int count = suppliers.Where(d =>  d.OriginEnergySupply - d.CurrSuppliedEnergy > 1e-3).Count();
                if (count == 0) break;
                double share = totalEnergyDemand / count;
                suppliers.ForEach(d =>
                {
                    if (d.CurrSuppliedEnergy < d.OriginEnergySupply)
                    {
                        double toAssign = Math.Min(share, d.OriginEnergySupply - d.CurrSuppliedEnergy);
                        d.UpdateSuppliedEnergy(d.CurrSuppliedEnergy + toAssign);
                        totalEnergyDemand = totalEnergyDemand - toAssign;
                    }

                });
            }

        }
    }
}
