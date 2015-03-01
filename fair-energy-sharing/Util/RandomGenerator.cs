using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.Util
{
    public static class RandomGenerator
    {
        static Random random = new Random();

        public static void SetSeed(int seed) {

            random = new Random(seed);
        }

        public static int Next() {

            return random.Next();
        }
 
    }
}
