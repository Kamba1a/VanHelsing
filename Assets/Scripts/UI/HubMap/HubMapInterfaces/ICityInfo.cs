using UnityEngine;

namespace BeastHunter
{
    public interface ICityInfo
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Sprite Fraction { get; }
        public int[] CitizensId { get; }
        public float CurrentPlayerReputation { get; }
        public int[] SellingItemsId { get; }
    }
}
