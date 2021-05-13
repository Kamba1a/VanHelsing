using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastHunterHubUI
{
    public class AnimaliaBossModel : BaseBossModel
    {
        public override BossType BossType { get ; protected set; }

        public AnimaliaBossModel(BaseBossData data) : base(data)
        {
            BossType = BossType.Animalia;

        }
    }
}
