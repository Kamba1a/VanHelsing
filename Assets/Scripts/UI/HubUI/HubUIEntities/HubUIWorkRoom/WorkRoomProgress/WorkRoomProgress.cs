using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public class WorkRoomProgress : BaseWorkRoomProgress
    {
        [SerializeField] int _recipesSlots;

        public int RecipesSlots => _recipesSlots;
    }
}
