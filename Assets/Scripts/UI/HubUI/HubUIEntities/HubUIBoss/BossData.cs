using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "BossData", menuName = "CreateData/HubUIData/BossData", order = 0)]
    public abstract class BossData : ScriptableObject
    {
        [Header("BaseBossData")]
        [SerializeField] private string _name;
        [SerializeField] private BossSizeType _bossSize;
        [SerializeField] private AttractionOrAvoidanceType _attraction;
        [SerializeField] private AttractionOrAvoidanceType _avoidance;
        [SerializeField] private VulnerabilityType _vulnerability;


        public string Name => _name;
        public BossSizeType BossSize => _bossSize;
        public AttractionOrAvoidanceType Attraction => _attraction;
        public AttractionOrAvoidanceType Avoidance => _avoidance;
        public VulnerabilityType Vulnerability => _vulnerability;
    }
}
