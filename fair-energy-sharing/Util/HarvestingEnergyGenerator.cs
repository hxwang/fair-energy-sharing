using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Util
{
    public class HarvestingEnergyGenerator
    {
        /// <summary>
        /// read real solartrace from file
        /// </summary>
        /// <param name="config"></param>
        /// <param name="realSolarTracePath"></param>
        /// <returns></returns>
        public static List<double> GenerateHarvestingEnergy(Config config) {
            List<double> rnt = new List<double>();
            StreamReader sr = new StreamReader(config.HarvestingTracePath);
            rnt = sr.ReadListColumn(config.TotalTimeSlot);
            return rnt;
        }

       
    }
}
