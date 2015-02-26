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
        #endregion 
        

        /// <summary>
        /// TODO: how to initialized the energy harvesting list, and energy consumption list of different homes
        /// </summary>
        public Home(Config config, List<double> energyHarvestingList, List<Double> energyConsumptionList)
        {

            #region init
            this.EnergyHarvestingList = energyHarvestingList;
            this.EnergyConsumptionList = energyConsumptionList;

            this.SuppliedEnergyList = new double[config.TotalTimeSlot];
            this.AcquiredEnergyList = new double[config.TotalTimeSlot];
            this.EnergyCostList = new double[config.TotalTimeSlot];                        

            this.Reputation = 0;
            this.CurrTime = 0;

            #endregion

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

    }


    //Comparator to sort reputaiton in non-increasing order
    class ReputationDecreseComparator : IComparer<Home> {
        public int Compare(Home x, Home y) {
            return y.Reputation.CompareTo(x.Reputation);
        }
    }

    //Comparator to sort energy demand in non-decreasing order
    class DemandIncreaseComparator : IComparer<Home> {
        public int Compare(Home x, Home y) {
            return Math.Abs(x.OriginEnergyDemand).CompareTo(Math.Abs(y.OriginEnergyDemand));
        }
    }
}
