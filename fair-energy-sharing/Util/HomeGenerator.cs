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

            var candidateHomeList = Enumerable.Range(1, config.TotalHomeCount).ToList();
            for (int i = 0; i < config.FilterHomeIdList.Count; i++) {
                candidateHomeList.Remove(config.FilterHomeIdList[i]);
            }

                candidateHomeList = candidateHomeList.OrderBy(t => Util.RandomGenerator.Next()).Take(config.SimulationHomeCount).ToList();
            for (int i = 0; i < candidateHomeList.Count; i++) {
                var energyConsumptionFile = config.HomeEnergyConsumptionTracePath + candidateHomeList[i] + ".txt";
                StreamReader sr = new StreamReader(energyConsumptionFile);
                List<double> energyConsumptionList = new List<double>();

                energyConsumptionList = sr.ReadListColumn(config.TotalTimeSlot);

                List<double> energyHarvestingList = new List<double>(energyList);

                if(!config.ValidHOC)
                    RescaleHarvestingEnergy(energyHarvestingList, energyConsumptionList, config.HarvestingPeakOverConsumptionPeak, config);
                else RescaleHarvestingEnergyWithValidHOC(energyHarvestingList, energyConsumptionList, config.HarvestingPeakOverConsumptionPeak, config);

                Home home = new Home(config, energyHarvestingList, energyConsumptionList, candidateHomeList[i]);
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
        public static void RescaleHarvestingEnergy(List<double> harvestingEnergy, List<double> energyConsumptionList, double solarPanelSize, Config config)
        {
            double usage =  energyConsumptionList.Max();
           
            double harvest = harvestingEnergy.Max();

            if (!config.HOCwithPeak) {
                List<double> tmpHarvestingList = new List<double>(harvestingEnergy);
                tmpHarvestingList.Sort();
                int idx1 = (int)(tmpHarvestingList.Count * config.Percentile);
                harvest = tmpHarvestingList[idx1];

                List<double> tmpEnergyConsumptionList = new List<double>(energyConsumptionList);
                tmpEnergyConsumptionList.Sort();
                int idx2 = (int)(tmpEnergyConsumptionList.Count * config.Percentile);
                usage = tmpEnergyConsumptionList[idx2];
            
            }
            // Console.WriteLine("usage = {0}", usage);
            // Console.WriteLine("consumptionPeak = {0}", harvest);

            double scale = usage==0 ? harvest :  harvest / usage;
            for (int i = 0; i < harvestingEnergy.Count; i++) { 
                harvestingEnergy[i] = harvestingEnergy[i]/ scale * solarPanelSize;
            }
               
    
        }

        public static void RescaleHarvestingEnergyWithValidHOC(List<double> harvestingEnergy, List<double> energyConsumptionList, double solarPanelSize, Config config)
        {
            double usage = 0;
            List<double> tmpEnergyConsumptionList = new List<double>();
            for (int i = 0; i < energyConsumptionList.Count; i++) {
                if (harvestingEnergy[i] > 0)
                {
                    tmpEnergyConsumptionList.Add(energyConsumptionList[i]);
                  
                }
            }

            usage = tmpEnergyConsumptionList.Max();
         
            double harvest = harvestingEnergy.Max();

            if (!config.HOCwithPeak)
            {
                List<double> tmpHarvestingList = harvestingEnergy.Where(h => h > 0).ToList();
                tmpHarvestingList.Sort();
                int idx1 = (int)(tmpHarvestingList.Count * config.Percentile);
                harvest = tmpHarvestingList[idx1];


                tmpEnergyConsumptionList.Sort();
                int idx2 = (int)(tmpEnergyConsumptionList.Count * config.Percentile);
                usage = tmpEnergyConsumptionList[idx2];

            }
            // Console.WriteLine("usage = {0}", usage);
            // Console.WriteLine("consumptionPeak = {0}", harvest);

            double scale = usage == 0 ? harvest : harvest / usage;
            for (int i = 0; i < harvestingEnergy.Count; i++)
            {
                harvestingEnergy[i] = harvestingEnergy[i] / scale * solarPanelSize;
            }


        }
        
    }
}
