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
            List<Home> suppliers = homes.Where(h => h.HomeType == HomeType.Supplier).ToList();
            List<Home> demanders = homes.Where(h => h.HomeType == HomeType.Demander).ToList();
            double totalEnergySupply = suppliers.Sum(h => h.OriginEnergySupply);
            double totalEnergyDemand = demanders.Sum(h => h.OriginEnergyDemand);

            if (demanders.Count != 0 && suppliers.Count != 0)
            {
                if (totalEnergySupply <= totalEnergyDemand)
                {
                    //set the supplied energy of suppliers
                    suppliers.ForEach(s => s.UpdateSuppliedEnergy(s.OriginEnergySupply));
                    List<Home> candiates = new List<Home>();

                    //pre assign energy to ajust reputation
                    totalEnergySupply = PreprocessEnergyToAdjustReputation(homes, candiates, totalEnergySupply);

                    //if there are still energy remain, using CGconstraint to assign the energy
                    if (totalEnergySupply > 1e-3 )
                    {
                        if (candiates.Count == 0) {
                            Console.WriteLine("Error, there are {0} energy left, candiates home should be larger than 0!", totalEnergySupply);
                        }
                        MatchSupplyLessThanDemand(candiates, totalEnergySupply);
                        
                    }

                    //calcualte total energy assign
                    //Note the total energy assign = energy assigned at above two steps (ajusting reputation assign, and CGAssign)
                    calculateTotalAssign(demanders);

                }

                else MatchSupplyLargerThanDemand(suppliers, demanders);
            }


            //update reputation and energy cost
            UpdateReputationAndCost(suppliers, demanders);
        }


        public double PreprocessEnergyToAdjustReputation(List<Home> homes,List<Home> candiates, double totalEnergySupply) {

            homes.ForEach(h1 => h1.ResetAcquiredAdjustReputationEnergy());
            List<Home> demanders = homes.Where(h1 => h1.HomeType == HomeType.Demander).ToList();

          

            //sort demanders by reputaiton
            demanders.Sort(new ReputationDecreseComparator());

            Home h = demanders[0];
            demanders.RemoveAt(0);
            candiates.Add(h);
            double currReputationLevel = h.Reputation;

            while (demanders.Count != 0) {
                Home currH = demanders[0];
                demanders.RemoveAt(0);
                double toAssign = candiates.Sum(candidateHome => { return Math.Min(currReputationLevel - currH.Reputation, candidateHome.OriginEnergyDemand - candidateHome.AcquiredAdjustReputationEnergy); });
                if (toAssign <= totalEnergySupply)
                {
                    totalEnergySupply -= toAssign;                 
                    candiates.ForEach(ca =>
                    {
                        ca.AcquiredAdjustReputationEnergy += Math.Min(currReputationLevel - currH.Reputation, ca.OriginEnergyDemand - ca.AcquiredAdjustReputationEnergy);

                    });
                    candiates.Add(currH);

                    currReputationLevel = currH.Reputation;
                    //TODO: check correctness
                    candiates.RemoveAll(home => home.RemainEnergyDemandAfterAjustReputation <= 1e-3);
                }
                else break;
                

            }
            
            return totalEnergySupply;
        }

        /// <summary>
        /// calculate total assign
        /// </summary>
        /// <param name="demanders"></param>
        public void calculateTotalAssign(List<Home> demanders) {
            demanders.ForEach(d =>
                {
                    d.UpdateAcquiredEnergy(d.AcquiredAdjustReputationEnergy + d.CurrAcquiredEnergy);

                });
        }


        public void MatchSupplyLessThanDemand(List<Home> demanders, double totalEnergySupply)
        {

           
            double totalEnergyDemand = demanders.Sum(h => h.OriginEnergyDemand);

            Boolean computeLoss = false;
            if (totalEnergySupply > totalEnergyDemand / 2) {
                computeLoss = true;
                totalEnergySupply = totalEnergyDemand - totalEnergySupply;
            }

            demanders.Sort(new RemainDemandIncreaseComparator());

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
                    totalEnergySupply = 0;
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

            if (totalEnergySupply > 0) {
                Console.WriteLine("There are still energy remain!");
            }

         
        }


       


        public void MatchSupplyLargerThanDemand(List<Home> suppliers, List<Home> demanders) {

            demanders.ForEach(h => h.UpdateAcquiredEnergy(h.OriginEnergyDemand));

            double totalEnergySupply = suppliers.Sum(h => h.OriginEnergySupply);
            double totalEnergyDemand = demanders.Sum(h => h.OriginEnergyDemand);

            Boolean computeLoss = false;
            if (totalEnergyDemand > totalEnergySupply / 2)
            {
                computeLoss = true;
                totalEnergyDemand = totalEnergySupply - totalEnergyDemand;
            }

            suppliers.Sort(new SupplyIncreaesComparator());

            for (int i = 0; i < suppliers.Count; i++)
            {
                Home h = suppliers[i];
                double toAssign = h.OriginEnergySupply / 2 * (suppliers.Count - i);
                if (toAssign <= totalEnergyDemand)
                {
                    totalEnergyDemand = totalEnergyDemand - h.OriginEnergySupply / 2;
                    h.UpdateSuppliedEnergy(h.OriginEnergySupply / 2);
                }
                else
                {
                    double equalAssign = totalEnergyDemand / (suppliers.Count - i);
                    for (int j = i; j < suppliers.Count; j++)
                    {
                        Home remainHome = suppliers[j];
                        remainHome.UpdateSuppliedEnergy(equalAssign);
                    }
                    break;
                }
            }

            //if we use distribute loss, then the assigned energy need to update according
            //In specific, it should be updated as h.OriginEnergyDemand - h.CurrAcquiredEnergy
            if (computeLoss)
            {
                suppliers.ForEach(h =>
                {
                    h.UpdateSuppliedEnergy(h.OriginEnergySupply - h.CurrSuppliedEnergy);
                });
            }
        }
        

       
    }
}

