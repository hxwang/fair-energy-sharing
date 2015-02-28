using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using System.IO;

namespace fair_energy_sharing.Util
{
    public static class FileSaver
    {

        public static void SaveHomeListToFile(List<Home> homes) {
            String harvestingPath =  "harvesting.txt";
            String consumptionPath = "consumption.txt";
            File.Delete(harvestingPath);
            File.Delete(consumptionPath);

            homes.ForEach(h =>
            {
                SaveListToFile(h.EnergyHarvestingList, harvestingPath);
                SaveListToFile(h.EnergyConsumptionList,consumptionPath);

            }

            );
        
        }

        public static void SaveListToFile(List<double> list, String path){
            StreamWriter sw;
            sw = new StreamWriter(path, true);


            list.ForEach(i => sw.Write(i+" "));
            sw.WriteLine();

            sw.Close();
        }
    }
}
