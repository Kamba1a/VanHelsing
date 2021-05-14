using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastHunterHubUI
{
    public class BossSubtype
    {
        public Enum Get { get; private set; }

        public BossSubtype(BossType bossType)
        {
            if (bossType == BossType.Animalia)
            {
                //Get = AnimaliaBossType;
            }
        }
    }
}
