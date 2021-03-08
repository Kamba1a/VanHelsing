using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapItem
    {
        public int Id { get; }
        public Sprite Image { get; }
        public float RequiredReputationForSale { get; }
    }
}
