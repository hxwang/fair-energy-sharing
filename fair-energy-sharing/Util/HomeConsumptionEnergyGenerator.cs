using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace fair_energy_sharing.Util
{
    public static class HomeConsumptionEnergyGenerator
    {
        public static List<double> GenerateHomeEnergyConsumption(Config config, String filePath) {
            List<double> rnt = new List<double>();
            StreamReader sr = new StreamReader(filePath);
            rnt = sr.ReadListPerLine(config.TotalTimeSlot);
            return rnt;           
        }
    }
}
