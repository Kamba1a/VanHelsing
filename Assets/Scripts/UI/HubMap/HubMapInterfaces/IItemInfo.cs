using UnityEngine;

namespace BeastHunter
{
    public interface IItemInfo
    {
        public int Id { get; }
        public Sprite Image { get; }
        public float RequiredReputationForSale { get; }
    }
}
