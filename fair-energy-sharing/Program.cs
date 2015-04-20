using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Simulation;

namespace fair_energy_sharing
{
     class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config();
            UpdateConfig(args, config);
            Util.RandomGenerator.SetSeed(config.Seed);

            if(config.ClearFiles)
                deltePreviousResult(config);
            PrintConfig(config);
            if(config.IsRunReputationCurSim)
                SimReputationCurve(config);
            else testCGAssigner(config);
            
        }


        public static void deltePreviousResult(Config config) {
            if (config.IsRunReputationCurSim)
            {
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(config.ReputationCurveSimulationOutputPath);

                Empty(directory);
            }
            else
            {
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(config.SimulationOutputPath);

                Empty(directory);
            }

            
        
        }

        public static void Empty( System.IO.DirectoryInfo directory)
        {
            if (directory.Exists == false) directory.Create();

            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }


        public static void testCGAssigner(Config config) { 
            SupplyAndDemandTrend sim = new SupplyAndDemandTrend(config);
            sim.runRepeatSimulation();
        }

        public static void SimReputationCurve(Config config) {
            ReputationCurve sim = new ReputationCurve(config);
            sim.RunOneSimulation();
        }

        public static void UpdateConfig(string[] args, Config config) {

            var i = 0;
            while (i < args.Length) {
                var arg = args[i];
                if (arg == "-t") {
                    config.TotalTimeSlot = int.Parse(args[++i]);
                }
                else if (arg == "-price") {
                    config.UnitEnergyPrice = double.Parse(args[++i]);
                }
                else if (arg == "-r") {
                    config.Repeatition = int.Parse(args[++i]);
                }
                else if (arg == "-hc") {
                    config.SimulationHomeCount = int.Parse(args[++i]);
                }
                else if(arg == "-ratio"){
                    config.HarvestingPeakOverConsumptionPeak = double.Parse(args[++i]);
                    config.SimulationOutputPath = config.SimulationOutputPath + config.HarvestingPeakOverConsumptionPeak*100+@"\";
                    config.ReputationCurveSimulationOutputPath = config.ReputationCurveSimulationOutputPath + config.HarvestingPeakOverConsumptionPeak * 100 + @"\";
                }
                else if (arg == "-per") {
                    config.Percentile = double.Parse(args[++i]);
                }
                else if (arg == "-simRepCur") {
                    config.IsRunReputationCurSim = true;
                    config.TotalTimeSlot = 2;
                }
                else if (arg == "-notClearFile") {
                    config.ClearFiles = false;
                }
                i++;
               
            }
        
        }

        public static void PrintConfig(Config config) {
            Console.WriteLine("Config ratio = {0}, simHomeCount = {1}, timeSlot = {2}", config.HarvestingPeakOverConsumptionPeak, config.SimulationHomeCount, config.TotalTimeSlot);
        
        }
    }


}
