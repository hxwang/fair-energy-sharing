using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.EnergyAssigner;

namespace fair_energy_sharing
{
    public class Config
    {
        public double UnitEnergyPrice { get;  set; }
        public int TotalTimeSlot { get;  set; }
        public AssignerType AssignerType { get; private set; }


        public String ConsumptionTracePath{get; private set;}
        public String HarvestingTracePath { get; private set; }


        #region printSetting
        public Boolean PrintDetailOfEachHome { get; private set; }

        #endregion

        /// <summary>
        /// TODO: read from file
        /// </summary>
        public Config() {
            this.UnitEnergyPrice = 1;
            this.TotalTimeSlot = 4;
           


            #region printSetting
            PrintDetailOfEachHome = true;
            #endregion

            //TODO: set valid path
            this.ConsumptionTracePath = @"..\..\..\data\consumptionTrace.txt";
            this.HarvestingTracePath = @"..\..\..\data\harvestingTrace.txt";
        }
    }
}