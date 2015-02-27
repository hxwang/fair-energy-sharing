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
        public static List<double> GenerateEnergy(Config config, String realSolarTracePath) {
            List<double> rnt = new List<double>();
            StreamReader sr = new StreamReader(realSolarTracePath);           
            sr.ReadList(config.TotalTimeSlot);
            return rnt;
        }
    }
}
