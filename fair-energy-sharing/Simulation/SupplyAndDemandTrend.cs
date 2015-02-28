using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using fair_energy_sharing.Util;
using fair_energy_sharing.EnergyAssigner;
using System.IO;

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

            //generate home
            this.Homes = Util.HomeGenerator.GenerateHome(this.Config);
            

            //run different algorithms
            foreach(String assignerType in Enum.GetNames(typeof(AssignerType))){
                List<Home> homes = CloneHomes(this.Homes);
                Console.WriteLine("--------------------{0}------------------", assignerType);
                Simulator = new Simulator(Config, homes, assignerType);
                Simulator.Simulate();
                //write simulation results into file
                SimulationResultProcess.ProcessHomeResult(homes, Config.SimulationOutputPath+ assignerType);
            }
            
        }

        //run multiple repeatitions
        public void runRepeatSimulation() {
            for (int i = 0; i < Config.Repeatition; i++) {
                runOneSimulation();
            }
        }


        /// <summary>
        /// test the corretness of the generated home
        /// Method: print the homes information to file
        /// Plot files in matlab to see the trend of energy consumption and harvesting energy
        /// </summary>
        public void testHomeSettings() {

            this.Homes = Util.HomeGenerator.GenerateHome(this.Config);
            FileSaver.SaveHomeListToFile(this.Homes);
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
