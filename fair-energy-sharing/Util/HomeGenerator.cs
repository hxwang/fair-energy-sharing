using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using System.IO;

namespace fair_energy_sharing.Util
{
    public static class HomeGenerator
    {
        /// <summary>
        /// generate a list of home
        /// </summary>
        /// <param name="config"></param>
        /// <param name="harvestingPath"></param>
        /// <param name="homePathDir"></param>
        /// <returns></returns>
        public static List<Home> GenerateHome(Config config) {
            List<Home> rnt = new List<Home>();
            List<double> energyList = HarvestingEnergyGenerator.GenerateHarvestingEnergy(config);
            var random = new Random();
            var candidateHomeList = Enumerable.Range(1, config.TotalHomeCount).OrderBy(t => random.Next()).Take(config.SimulationHomeCount).ToList();
            for (int i = 0; i < candidateHomeList.Count; i++) {
                var energyConsumptionFile = config.HomeEnergyConsumptionTracePath + candidateHomeList[i] + ".txt";
                StreamReader sr = new StreamReader(energyConsumptionFile);
                List<double> energyConsumptionList = new List<double>();

                energyConsumptionList = sr.ReadListColumn(config.TotalTimeSlot);

                List<double> energyHarvestingList = new List<double>(energyList);
                RescaleHarvestingEnergy(energyHarvestingList, energyConsumptionList, config.HarvestingPeakOverConsumptionPeak);

                Home home = new Home(config, energyHarvestingList, energyConsumptionList);
                rnt.Add(home);
            
            }

            return rnt;
        }

        /// <summary>
        /// rescale the harvesting energy amount for each home to match with the power consumption
        /// Rule: the peak harvestign energy can cover x times of the peak energy demand, where x = solarPanelSize
        /// </summary>
        /// <param name="harvestingEnergy"></param>
        /// <param name="energyConsumptionList"></param>
        /// <param name="solarPanelSize"></param>
        public static void RescaleHarvestingEnergy(List<double> harvestingEnergy, List<double> energyConsumptionList, double solarPanelSize)
        {
            double usagePeak = energyConsumptionList.Max();
           
            double harvestPeak = harvestingEnergy.Max();
             Console.WriteLine("usagePeak = {0}", usagePeak);
             Console.WriteLine("consumptionPeak = {0}", harvestPeak);

            double scale = usagePeak==0 ? harvestPeak :  harvestPeak / usagePeak;
            for (int i = 0; i < harvestingEnergy.Count; i++) { 
                harvestingEnergy[i] = harvestingEnergy[i]/ scale * solarPanelSize;
            }
               
    
        }
    }
}
