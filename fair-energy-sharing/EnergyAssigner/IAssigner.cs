using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fair_energy_sharing.Model;

namespace fair_energy_sharing.EnergyAssigner
{
    public interface IAssigner
    {
        Config Config { get; set; }
        void Assign(List<Home> homes);
    }
}
