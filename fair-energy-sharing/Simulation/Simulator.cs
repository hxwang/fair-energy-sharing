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

        public Simulator(Config config, List<Home> homes, string assignerType) {
            this.Config = config;
            this.Homes = homes;
            this.Assigner = AssignerFactory.CreateAssigner(assignerType);
        }

        
        public void Simulate() {

            for (int i = 0; i < Config.TotalTimeSlot; i++) {
                //Console.WriteLine();
                Homes.ForEach(h => h.CurrTime = i);
                Assigner.Assign(this.Homes);
                PrintResult();
            }
        }

        public void PrintResult() {

            if (Config.PrintDetailOfEachHome)
            {
                Homes.ForEach(h =>
                {
                    Console.WriteLine("AvailableSupply/Supplied = {0:F4}/{1:F4}, Required/Assigned = {2:F4}/{3:F4}, Reputation = {4:F4}， Cost = {5:F4} ", h.OriginEnergySupply, h.CurrSuppliedEnergy, h.OriginEnergyDemand, h.CurrAcquiredEnergy, h.Reputation, h.CurrEnergyCost);
                });
            }
        }
    }
}
