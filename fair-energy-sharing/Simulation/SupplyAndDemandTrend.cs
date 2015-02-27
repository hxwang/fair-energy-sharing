using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using fair_energy_sharing.Util;
using fair_energy_sharing.EnergyAssigner;

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
      


        public void runOneSimulation() {
            this.Homes = Util.ReadFile.InitHomeFromFile(Config);  
   
            foreach(String assignerType in Enum.GetNames(typeof(AssignerType))){
                List<Home> homes = CloneHomes(this.Homes);
                Console.WriteLine("--------------------{0}------------------", assignerType);
                Simulator = new Simulator(Config, homes, assignerType);
                Simulator.Simulate();
            }
            
        }


        /// <summary>
        /// clone homes
        /// </summary>
        /// <param name="homes"></param>
        /// <returns></returns>
        public List<Home> CloneHomes(List<Home> homes) {
            List<Home> rnt = new List<Home>();
            homes.ForEach(h =>
                {
                    rnt.Add(h.Clone());

                });
            return rnt;
        }

        

    }
}
