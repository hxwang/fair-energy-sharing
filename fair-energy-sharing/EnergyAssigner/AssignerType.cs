using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fair_energy_sharing.EnergyAssigner
{
    public enum AssignerType
    {
        CGAssigner,
        ProportionAssigner,
        PreferLargerDemandAssigner,
        PreferLargeReputationAssigner,
        NoShareAssigner,
        EqualAssigner
    }
}
