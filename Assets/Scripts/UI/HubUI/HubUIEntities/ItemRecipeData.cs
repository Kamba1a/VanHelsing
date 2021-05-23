using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "ItemRecipe", menuName = "CreateData/HubUIData/ItemRecipe", order = 0)]
    public class ItemRecipeData : ScriptableObject
    {
        [SerializeField] private int _baseHoursNumberToComplete;
        [SerializeField] private BaseItemData _item;
        [SerializeField] private IngredientItemData[] _requiredIngredients; //todo: ingredients amount


        public int BaseHoursNumberToComplete => _baseHoursNumberToComplete;
        public BaseItemData Item => _item;
        public IngredientItemData[] RequiredIngredients => _requiredIngredients;
    }
}
