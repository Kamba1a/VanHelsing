namespace BeastHunterHubUI
{
    public abstract class BaseBossModel
    {
        public abstract BossType BossType { get; protected set; }
        public string Name { get; private set; }
        public BossSizeType BossSize { get; private set; }
        public AttractionOrAvoidanceType Attraction { get; private set; }
        public AttractionOrAvoidanceType Avoidance { get; private set; }
        public VulnerabilityType Vulnerability { get; private set; }


        public BaseBossModel(BaseBossData data)
        {
            Name = data.Name;
            BossSize = data.BossSize;
            Attraction = data.Attraction;
            Avoidance = data.Avoidance;
            Vulnerability = data.Vulnerability;
        }
    }
}
