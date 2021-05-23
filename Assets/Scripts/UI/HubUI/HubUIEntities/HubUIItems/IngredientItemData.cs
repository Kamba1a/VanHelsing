using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "IngredientItem", menuName = "CreateData/HubUIData/Items/IngredientItem", order = 0)]
    public class IngredientItemData : BaseItemData
    {
        public override ItemType ItemType => ItemType.Ingredient;
    }
}
