using fair_energy_sharing.EnergyAssigner;
using fair_energy_sharing.Model;
using fair_energy_sharing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Simulation
{
    class ReputationCurve
    {
      
        Config Config;
        Simulator sim;

        public ReputationCurve(Config config) {
            this.Config = config;
          
        }

        public void RunOneSimulation() {
            
            foreach (String assignerType in Enum.GetNames(typeof(AssignerType)))
            {
                List<Home> homes = Util.HomeGenerator.GenerateSimulatedHome(this.Config);
                //Console.WriteLine("--------------------{0}------------------", assignerType);
                sim = new Simulator(Config, homes, assignerType);

                sim.Simulate(1,1);
               
                //write simulation results into file
                //Note that the simulation result output file "reputationlist" will be the file for plotting figures
                SimulationResultProcess.ProcessReputationCurveResult(homes, Config.ReputationCurveSimulationOutputPath + assignerType);
            }
        }

        /// <summary>
        /// clone homes
        /// </summary>
        /// <param name="homes"></param>
        /// <returns></returns>
        public List<Home> CloneHomes(List<Home> homes)
        {
            List<Home> rnt = new List<Home>();
            homes.ForEach(h =>
            {
                rnt.Add(h.CloneWithReputation());

            });
            return rnt;
        }

    }
}
