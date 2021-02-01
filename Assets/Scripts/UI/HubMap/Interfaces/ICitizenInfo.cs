using UnityEngine.UI;

namespace BeastHunter
{
    public interface ICitizenInfo
    {
        public string Name { get; }
        public Image Portrait { get; }
        public bool IsHaveQuest { get; }
        public string QuestDescription { get; }
    }
}
