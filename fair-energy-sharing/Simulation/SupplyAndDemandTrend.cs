using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using fair_energy_sharing.Util;

namespace fair_energy_sharing.Simulation
{
    public class SupplyAndDemandTrend
    {

        List<Home> Homes;
        Config Config;
        Simulator Simulator;

        public SupplyAndDemandTrend(Config config){
            this.Config = config;
            Homes = new List<Home>();
        }

        public void init()
        { 
            this.Homes = Util.ReadFile.InitHomeFromFile(Config);           
        }


        public void runSimulation() {
            init();
            Simulator = new Simulator(Config, Homes);
            Simulator.Simulate();
        }

        

    }
}
