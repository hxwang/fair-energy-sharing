using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.EnergyAssigner;

namespace fair_energy_sharing
{
    public class Config
    {
        public double UnitEnergyPrice { get; private set; }
        public int TotalTimeSlot { get; private set; }
        public AssignerType AssignerType { get; private set; }


        public String ConsumptionTracePath{get; private set;}
        public String HarvestingTracePath { get; private set; }
        /// <summary>
        /// TODO: read from file
        /// </summary>
        public Config() {
            this.UnitEnergyPrice = 1;
            this.TotalTimeSlot = 4;
            this.AssignerType = EnergyAssigner.AssignerType.CGAssigner;
            
            //TODO: set valid path
            this.ConsumptionTracePath = @"../../../data/consumptionTrace.txt";
            this.HarvestingTracePath = @"../../../data/harvestingTrace.txt";
        }
    }
}
