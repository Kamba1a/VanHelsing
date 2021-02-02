using UnityEngine;

namespace BeastHunter
{
    public interface ICitizenInfo
    {
        public int Id { get; }
        public string Name { get; }
        public Sprite Portrait { get; }
        public bool IsHaveQuest { get; }
        public string QuestDescription { get; }
    }
}
