using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Util
{
    public static class StreamReaderExtension
    {
        public static List<double> ReadList(this StreamReader self)
        {
            var line = self.ReadLine();
            var items = line.Split(' ');
            var list = new List<Double>();
            foreach (String s in items)
            {
                list.Add(double.Parse(s));
            }

            return list;
        
        }
    }
}
