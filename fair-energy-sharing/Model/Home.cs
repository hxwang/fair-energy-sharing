using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Model
{
    public class Home
    {
       

        #region simulation setting
        public int CurrTime { get; set; }
        public Config Config { get; set; }

        public List<double> EnergyHarvestingList { get; private set; }
        public List<double> EnergyConsumptionList { get; private set; }

        public double CurrEnergyConsumption { get { return this.EnergyConsumptionList[CurrTime]; } }
        public double CurrEnergyHarvesting { get { return this.EnergyHarvestingList[CurrTime]; } }


        //public double CurrEnergyDiff
        //{
        //    get
        //    {
        //        double diff = this.CurrEnergyHarvesting - this.CurrEnergyConsumption;
        //        return Math.Abs(diff);
        //    }
        //}

        /// <summary>
        /// Current energy demand
        /// This value will change when a home assigned some energy
        /// </summary>
        public double OriginEnergyDemand {
            get {
                double diff = this.CurrEnergyHarvesting - this.CurrEnergyConsumption;
                if (diff < 0) return -diff;
                else return 0;
            }
        }

        public double OriginEnergySupply {
            get
            {
                double diff = this.CurrEnergyHarvesting - this.CurrEnergyConsumption;
                if (diff > 0) return diff;
                else return 0;
            }
        
        }

        public double AcquiredAdjustReputationEnergy
        {
            get;
            set;
        }

        public void ResetAcquiredAdjustReputationEnergy() {
            this.AcquiredAdjustReputationEnergy = 0;
        }

        public double RemainEnergyDemandAfterAjustReputation {
            get {
                return this.OriginEnergyDemand - this.AcquiredAdjustReputationEnergy;
            }
        }

        public double Reputation { get; set; }
        
        
        public HomeType HomeType
        {
            get
            {
                double diff = this.CurrEnergyHarvesting - this.CurrEnergyConsumption;
                if (diff < -1e-3)
                    return HomeType.Demander;
                else if (diff > 1e-3)
                    return HomeType.Supplier;
                else
                    return HomeType.Neither;
            }
        }

        

        #endregion

        #region simulation results
        /// <summary>
        /// valid energy supply at each time slot
        /// </summary>
        public  double[] SuppliedEnergyList;

        /// <summary>
        /// acquired energy supply at each time slot
        /// </summary>
        public  double[] AcquiredEnergyList;

        /// <summary>
        /// energy monetary cost at each time slot
        /// </summary>
        public  double[] EnergyCostList;
        private System.IO.StreamReader harvestingReader;
        private List<double> consumptionList;
        #endregion 
        

        /// <summary>
        /// TODO: how to initialized the energy harvesting list, and energy consumption list of different homes
        /// </summary>
        public Home(Config config, List<double> energyHarvestingList, List<Double> energyConsumptionList)
        {

            #region init
            int totalTimeSlot = energyHarvestingList.Count;
            if (config.TotalTimeSlot != totalTimeSlot) {
                throw new Exception(String.Format("The list size {0} is different from the total time slots {1}", totalTimeSlot, config.TotalTimeSlot));
            }
            this.EnergyHarvestingList = energyHarvestingList;
            this.EnergyConsumptionList = energyConsumptionList;

            this.SuppliedEnergyList = new double[totalTimeSlot];
            this.AcquiredEnergyList = new double[totalTimeSlot];
            this.EnergyCostList = new double[totalTimeSlot];                        

            this.Reputation = 0;
            this.CurrTime = 0;
            this.Config = config;
            #endregion

        }

        public Home Clone() {
            Home home = new Home(this.Config, this.EnergyHarvestingList, this.EnergyConsumptionList);
            return home;
        }
       

        public void UpdateSuppliedEnergy(double newVal)
        {
            this.SuppliedEnergyList[this.CurrTime] = newVal;
        }

        public void UpdateAcquiredEnergy(double newVal)
        {
            this.AcquiredEnergyList[this.CurrTime] = newVal;
        }

        public void UpdateEnergyCost(double newVal)
        {
            this.EnergyCostList[this.CurrTime] = newVal;
        }

        public double CurrSuppliedEnergy { get { return this.SuppliedEnergyList[this.CurrTime]; } }
        public double CurrAcquiredEnergy { get { return this.AcquiredEnergyList[this.CurrTime]; } }
        public double CurrEnergyCost { get { return this.EnergyCostList[this.CurrTime]; } }


        public void updateEnergyCost(double usedGridEnergyAmount)
        {
            this.EnergyCostList[this.CurrTime] = usedGridEnergyAmount * this.Config.UnitEnergyPrice;
        }

      
    }


    //Comparator to sort reputaiton in non-increasing order
    class ReputationDecreseComparator : IComparer<Home> {
        public int Compare(Home x, Home y) {
            return y.Reputation.CompareTo(x.Reputation);
        }
    }

    //TODO: change to sort using remaining energy demand 
    //Comparator to sort energy demand in non-decreasing order
    class RemainDemandIncreaseComparator : IComparer<Home> {
        public int Compare(Home x, Home y) {
            return Math.Abs(x.RemainEnergyDemandAfterAjustReputation).CompareTo(Math.Abs(y.RemainEnergyDemandAfterAjustReputation));
        }
    }

    //comparator to sort energy supply in non-increasing order
    class SupplyIncreaesComparator : IComparer<Home> {
        public int Compare(Home x, Home y)
        {
            return Math.Abs(x.OriginEnergySupply).CompareTo(Math.Abs(y.OriginEnergySupply));
        }
    }


    /// <summary>
    /// sort supply in deceasing order
    /// </summary>
    class SupplyDecreaseComparator : IComparer<Home>
    {
        public int Compare(Home x, Home y)
        {
            return y.OriginEnergySupply.CompareTo(x.OriginEnergySupply);
        }
    }


    /// <summary>
    /// sort demand in decreasing order
    /// </summary>
    class DemandDecreaseComparator: IComparer<Home>{
        public int Compare(Home x, Home y)
        {
            return y.OriginEnergyDemand.CompareTo(x.OriginEnergyDemand);
        }
    }
}
