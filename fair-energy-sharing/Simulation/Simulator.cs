using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using fair_energy_sharing.EnergyAssigner;

namespace fair_energy_sharing.Simulation
{
    class Simulator
    {
        public Config Config { get; private set; }
        public List<Home> Homes { get; private set; }
        public IAssigner Assigner { get; private set; }

        public Simulator(Config config, List<Home> homes) {
            this.Config = config;
            this.Homes = homes;
            this.Assigner = AssignerFactory.CreateAssigner(config.AssignerType);
        }

        
        public void Simulate() {

            for (int i = 0; i < Config.TotalTimeSlot; i++) {
                Console.WriteLine();
                Homes.ForEach(h => h.CurrTime = i);
                Assigner.Assign(this.Homes);
                PrintResult();
            }
        }

        public void PrintResult() { 
        
            Homes.ForEach(h => {
                Console.WriteLine("AvailableSupply/Supplied = {0}/{1}, Required/Assigned = {2}/{3}, Reputation = {4}， Cost = {5} ",  h.OriginEnergySupply, h.CurrSuppliedEnergy, h.OriginEnergyDemand, h.CurrAcquiredEnergy, h.Reputation, h.CurrEnergyCost);
            });
        }
    }
}
