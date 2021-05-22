using UnityEngine;


namespace BeastHunterHubUI
{
    public class ItemRecipeData : ScriptableObject
    {
        [SerializeField] private int _baseHoursNumberToComplete;
        [SerializeField] private BaseItemData _item;
        [SerializeField] private IngredientData[] _requiredIngredients; //todo: ingredients amount


        public int BaseHoursNumberToComplete => _baseHoursNumberToComplete;
        public BaseItemData Item => _item;
        public IngredientData[] RequiredIngredients => _requiredIngredients;
    }
}
