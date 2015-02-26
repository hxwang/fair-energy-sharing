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
            testCGAssigner(config);
        }

        public static void testCGAssigner(Config config) { 
            SupplyAndDemandTrend sim = new SupplyAndDemandTrend(config);
            sim.runSimulation();
        }
    }
}
