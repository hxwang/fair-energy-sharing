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

        public void initTestSupplyLessThanDemand() {

            //Home h1 = new Home(this.Config, new List<double> { 12, 0, 0, 0 }, new List<double> { 0, 3, 0, 0 });
            //Home h2 = new Home(this.Config, new List<double> { 10, 0, 0, 0 }, new List<double> { 0, 10, 0, 0 });
            //Home h3 = new Home(this.Config, new List<double> { 8, 0, 0, 0 }, new List<double> { 0, 10, 0, 0 });
            //Home h4 = new Home(this.Config, new List<double> { 0, 10, 0, 0 }, new List<double> { 22, 0, 0, 0 });
            //Homes.Add(h1);
            //Homes.Add(h2);
            //Homes.Add(h3);
            //Homes.Add(h4);  
            Util.ReadFile.InitHomeFromFile(Config);

            
        }

        public void initTestSupplyLargerThanDemand()
        {

            Home h1 = new Home(this.Config, new List<double> { 0, 0, 0, 0 }, new List<double> { 100, 200, 300, 400 });
            Home h2 = new Home(this.Config, new List<double> { 100, 100, 100, 100 },new List<double> { 0, 0, 0, 0 });
            Home h3 = new Home(this.Config,  new List<double> { 200, 200, 200, 200 },new List<double> { 0, 0, 0, 0 });
            Home h4 = new Home(this.Config,  new List<double> { 300, 300, 300, 300 },new List<double> { 0, 0, 0, 0 });
            Homes.Add(h1);
            Homes.Add(h2);
            Homes.Add(h3); 
            Homes.Add(h4);
          

        }

        public void runSimulation() {
            initTestSupplyLessThanDemand();
            Simulator = new Simulator(Config, Homes);
            Simulator.Simulate();
        }

        

    }
}
