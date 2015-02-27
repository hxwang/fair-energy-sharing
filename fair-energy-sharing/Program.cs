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

            testCGAssigner(config);
        }

        public static void testCGAssigner(Config config) { 
            SupplyAndDemandTrend sim = new SupplyAndDemandTrend(config);
            sim.runRepeatSimulation();
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
            }
        
        }
    }


}
