using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastHunterHubUI
{
    public class HuntingQuest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public BossData BossData { get; set; }

        public BossType BossType { get; private set; }
        //public BossSubtype BossSubtype { get; private set; } //todo
        public BossSizeType BossSize { get; private set; }
        public AttractionOrAvoidanceType Attraction { get; private set; }
        public AttractionOrAvoidanceType Avoidance { get; private set; }
        public VulnerabilityType Vulnerability { get; private set; }
    }
}
