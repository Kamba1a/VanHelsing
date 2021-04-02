using UnityEngine;


namespace BeastHunter
{
    public class HubMapUILocationModel : HubMapUIMapObjectModel
    {
        #region Properties

        public Sprite Screenshot { get; private set;}
        public HubMapUIDwellerData[] Dwellers { get; private set; }
        public HubMapUIIngredientData[] Ingredients { get; private set; }

        #endregion


        public HubMapUILocationModel(HubMapUILocationData data)
        {
            DataInstanceID = data.GetInstanceID();
            Name = data.Name;
            Description = data.Description;
            IsBlocked = data.IsBlockedAtStart;

            Screenshot = data.Screenshot;
            Dwellers = data.Dwellers;
            Ingredients = data.Ingredients;
        }
    }
}
