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
      

        #region general simulation setting
        public int Repeatition { get;  set; }
        public int SimulationHomeCount { get; set; }
        public int TotalHomeCount { get; set; }
        public int Days { get; set; }
        public int Seed { get; set; }

        public List<int> FilterHomeIdList { get; private set; }

        //tune the solar panel size to satisfy home needs
        public double HarvestingPeakOverConsumptionPeak { get; set; }
        //determine HOC using peak
        public Boolean HOCwithPeak { get; set; }
        //Instead of using peak to decide energy using HOC, using the percentile data to energy
        public double Percentile { get; set; }
        public Boolean ValidHOC { get; set; }

        #endregion

        #region reputation curve simulation setting
        public double SimHomeElectricityDemand;
        public  double SimTotalEnergySupply;
        public  Boolean IsRunReputationCurSim;
        #endregion

        #region filePath
        public String SimulationOutputPath { get;  set; }
        public String HomeEnergyConsumptionTracePath { get; private set; }
        public String HarvestingTracePath { get; private set; }

        //file path of simulation home reputation list
        public String SimHomeReputationPath { get; set; }
        public String ReputationCurveSimulationOutputPath { get; set; }
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
            //1440
            this.TotalTimeSlot = 1440;
           


            #region printSetting
            PrintDetailOfEachHome = false;
            #endregion

            //TODO: set valid path
            #region path setting
            //this.ConsumptionTracePath = @"..\..\..\data\consumptionTrace.txt";
            this.HomeEnergyConsumptionTracePath = @"..\..\smart-grid-workloads\smart-home\processedData\microgrid_5_days\";
            //this.HarvestingTracePath = @"..\..\..\data\processedTrace\solar.txt";
            this.HarvestingTracePath = @"..\..\smart-grid-workloads\solarTrace\processedTrace\solar.txt";
            
            this.SimulationOutputPath = @"..\simulationResult\";
            this.ReputationCurveSimulationOutputPath = @"..\simulationResult\ReputationCurve\";
            this.SimHomeReputationPath = @"..\..\smart-grid-workloads\smart-home\processedData\sim_homes\";
            #endregion

            #region simulation setting
            this.Repeatition = 30;
            this.SimulationHomeCount = 100;
            this.TotalHomeCount = 311;
            this.HarvestingPeakOverConsumptionPeak = 0.3;
            this.Seed = 8765;
            //this.FilterHomeIdList = new List<int> { 185,3 };
            this.FilterHomeIdList = new List<int>();
            this.HOCwithPeak = false;
            this.Percentile = 0.75;
            this.ValidHOC = true;

            #region simulation reputation curve
            this.SimHomeElectricityDemand = 50;
            this.SimTotalEnergySupply = 2000;
            this.IsRunReputationCurSim = false;
            #endregion

            #endregion

           


        }
    }
}