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
            deltePreviousResult(config);
            testCGAssigner(config);
        }


        public static void deltePreviousResult(Config config) {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(config.SimulationOutputPath);

            Empty(directory);
        
        }

        public static void Empty( System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }


        public static void testCGAssigner(Config config) { 
            SupplyAndDemandTrend sim = new SupplyAndDemandTrend(config);
            sim.runOneSimulation();
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
            }
        
        }
    }


}
