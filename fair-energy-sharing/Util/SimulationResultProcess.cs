﻿using System;
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
        public static void ProcessHomeResult(List<Home> homes, String path, double elapseTime)
        {

            homes.Sort(new TotalSupplyIncreaseComparator());
            if (homes[homes.Count - 1].SuppliedEnergyList.Sum() - homes[homes.Count - 2].SuppliedEnergyList.Sum() >= 400)
            {
                Console.WriteLine("HomeId = {0}, {1}/{2}", homes[homes.Count - 1].HomeId, homes[homes.Count - 1].SuppliedEnergyList.Sum(), homes[homes.Count - 2].SuppliedEnergyList.Sum());
            }
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
<<<<<<< HEAD
                //WriteListResultToFile(h.ReputationList.ToList(), path + "homeCount_"+homes.Count+"_reputationList.txt");
                WriteListResultToFile(h.ReputationList.ToList(), path  + "_reputationList.txt");
            });

            //WriteListResultToFile(costList, path + "homeCount_" + homes.Count + "_cost.txt");
            //WriteDoubleResultToFile(costList.Average(), path + "homeCount_" + homes.Count + "_Meancost.txt");
            //WriteListResultToFile(energyInList, path + "homeCount_" + homes.Count + "_energyIn.txt");
            //WriteListResultToFile(energyOutList, path + "homeCount_" + homes.Count + "_energyOut.txt");
            //WriteListResultToFile(finalReputationList, path + "homeCount_" + homes.Count + "_reputation.txt");
            //WriteDoubleResultToFile(elapseTime, path + "homeCount_" + homes.Count + "_timeCost.txt");

            WriteListResultToFile(costList, path + "_cost.txt");
            WriteDoubleResultToFile(costList.Average(), path  + "_Meancost.txt");
            WriteListResultToFile(energyInList, path  + "_energyIn.txt");
            WriteListResultToFile(energyOutList, path  + "_energyOut.txt");
            WriteListResultToFile(finalReputationList, path + "_reputation.txt");
            WriteDoubleResultToFile(elapseTime, path  + "_timeCost.txt");
            
=======
                WriteListResultToFile(h.ReputationList.ToList(), path + "homeCount_"+homes.Count+"_reputationList.txt");
            });

            WriteListResultToFile(costList, path + "homeCount_" + homes.Count + "_cost.txt");
            WriteDoubleResultToFile(costList.Average(), path + "homeCount_" + homes.Count + "_Meancost.txt");
            WriteListResultToFile(energyInList, path + "homeCount_" + homes.Count + "_energyIn.txt");
            WriteListResultToFile(energyOutList, path + "homeCount_" + homes.Count + "_energyOut.txt");
            WriteListResultToFile(finalReputationList, path + "homeCount_" + homes.Count + "_reputation.txt");
            WriteDoubleResultToFile(elapseTime, path + "homeCount_" + homes.Count + "_timeCost.txt");
>>>>>>> c3c3753b69a86172fe84a44371a4d3f09d457260

        }

        public static void ProcessReputationCurveResult(List<Home> homes, String path)
        {
            //sort home in order by ID
            homes.Sort(new HomeIdComparator());
         
            
            var energyInList = new List<double>();
          
            var finalReputationList = new List<double>();

            homes.ForEach(h =>
            {               
                energyInList.Add(h.CurrAcquiredEnergy);              
                finalReputationList.Add(h.Reputation);
                WriteListResultToFile(h.ReputationList.ToList(), path + "_reputationList.txt");
            });

          
            WriteListResultToFile(energyInList, path + "_energyIn.txt");
            
            WriteListResultToFile(finalReputationList, path + "_reputation.txt");
        }

        

        /// <summary>
        /// write list to file in append mode
        /// create a new one if not exist
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        public static void WriteListResultToFile(List<double> list, String path)
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

        public static void WriteDoubleResultToFile(double val, String path) {
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(val + " ");
                    sw.WriteLine();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(val + " ");
                    sw.WriteLine();
                }

            }
        
        
        }

    }
}
