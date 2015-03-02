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


       
      

        #region simulation setting
        public int Repeatition { get;  set; }
        public int SimulationHomeCount { get; set; }
        public int TotalHomeCount { get; set; }
        public int Days { get; set; }
        public int Seed { get; set; }

        //tune the solar panel size to satisfy home needs
        public double HarvestingPeakOverConsumptionPeak { get; set; }

        #endregion
        #region filePath
        public String SimulationOutputPath { get;  set; }
        public String HomeEnergyConsumptionTracePath { get; private set; }
        public String HarvestingTracePath { get; private set; }
        #endregion

        #region printSetting
        public Boolean PrintDetailOfEachHome { get; private set; }

        #endregion

        /// <summary>
        /// TODO: read from file
        /// </summary>
        public Config() {
            //unit energy prices is around 0.13/kWh
            //in the simulation, the scheduling is every 5 minutes, there are in total 12 of 5 minutes in one hour
            this.UnitEnergyPrice = 0.13/12;
            this.TotalTimeSlot = 1440;
           


            #region printSetting
            PrintDetailOfEachHome = false;
            #endregion

            //TODO: set valid path
            #region path setting
            //this.ConsumptionTracePath = @"..\..\..\data\consumptionTrace.txt";
            this.HomeEnergyConsumptionTracePath = @"..\..\..\..\smart-grid-workloads\smart-home\processedData\microgrid_5_days\";
            //this.HarvestingTracePath = @"..\..\..\data\processedTrace\solar.txt";
            this.HarvestingTracePath = @"..\..\..\..\smart-grid-workloads\smart-grid-workloads\solarTrace\processedTrace\solar.txt";
            
            this.SimulationOutputPath = @"..\..\..\simulationResult\";
          

            #endregion

            #region simulation setting
            this.Repeatition = 30;
            this.SimulationHomeCount = 100;
            this.TotalHomeCount = 311;
            this.HarvestingPeakOverConsumptionPeak = 0.3;
            this.Seed = 8765;

            #endregion
        }
    }
}