using UnityEngine;

namespace BeastHunter
{
    public interface IHubMapUIItem
    {
        public int Id { get; }
        public Sprite Image { get; }
        public float RequiredReputationForSale { get; }
    }
}
