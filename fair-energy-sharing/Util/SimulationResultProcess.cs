using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;
using System.IO;

namespace fair_energy_sharing.Util
{
    public static class  SimulationResultProcess
    {

        /// <summary>
        /// compute one round result
        /// </summary>
        /// <param name="homes"></param>
        /// <param name="path"></param>
        public static void ProcessHomeResult(List<Home> homes, String path)
        {

            homes.Sort(new TotalSupplyIncreaseComparator());

            var costList = new List<double>();
            var energyInList = new List<double>();
            var energyOutList = new List<double>();
            var finalReputationList = new List<double>();

            homes.ForEach(h =>
            {
                costList.Add(h.EnergyCostList.Sum());
                energyInList.Add(h.AcquiredEnergyList.Sum());
                energyOutList.Add(h.SuppliedEnergyList.Sum());
                finalReputationList.Add(h.Reputation);
            });

            WriteAverageResultToFile(costList, path + "_cost.txt");
            WriteAverageResultToFile(energyInList, path + "_energyIn.txt");
            WriteAverageResultToFile(energyOutList, path + "_energyOut.txt");
            WriteAverageResultToFile(finalReputationList, path + "_reputation.txt");

        }

        /// <summary>
        /// write list to file in append mode
        /// create a new one if not exist
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        public static void WriteAverageResultToFile(List<double> list, String path)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    list.ForEach(item => sw.Write(item + " "));
                    sw.WriteLine();
                }
            }
            else {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    list.ForEach(item => sw.Write(item + " "));
                    sw.WriteLine();
                }
            
            }
        }

    }
}
