using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUIItem
    {
        public Sprite Image { get; }
        public float RequiredReputationForSale { get; }
    }
}
