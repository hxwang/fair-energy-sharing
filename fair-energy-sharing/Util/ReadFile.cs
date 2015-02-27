using fair_energy_sharing.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Util
{
    class ReadFile
    {
        public static List<Home> InitHomeFromFile(Config config) {
            List<Home> homes = new List<Home>();

            StreamReader consumptionReader = new StreamReader(config.ConsumptionTracePath);
            StreamReader harvestingReader = new StreamReader(config.HarvestingTracePath);

            while (!consumptionReader.EndOfStream && !harvestingReader.EndOfStream) { 
                
                var consumptionList = consumptionReader.ReadList();
                var harvestingList = harvestingReader.ReadList();

                Home h = new Home(config, harvestingList, consumptionList);
                homes.Add(h);
            }

            return homes;
        }
    }
}
