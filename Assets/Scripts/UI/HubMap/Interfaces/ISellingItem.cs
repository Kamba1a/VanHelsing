using UnityEngine;

namespace BeastHunter
{
    public interface ISellingItem
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Sprite Image { get; }
        public float Cost { get; }
        public float RequiredReputation { get; }
        public bool IsEnoughReputation { get; } 
    }
}
