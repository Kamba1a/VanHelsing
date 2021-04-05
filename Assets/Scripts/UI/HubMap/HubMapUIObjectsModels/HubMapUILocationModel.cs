using UnityEngine;


namespace BeastHunter
{
    public class HubMapUILocationModel : HubMapUIMapObjectModel
    {
        #region Properties

        public int LoadSceneId { get; private set; }
        public Sprite Screenshot { get; private set;}
        public HubMapUIDwellerData[] Dwellers { get; private set; }
        public HubMapUIIngredientData[] Ingredients { get; private set; }

        #endregion


        public HubMapUILocationModel(HubMapUILocationData data)
        {
            MapObjectType = HubMapUIMapObjectType.Location;
            DataInstanceID = data.GetInstanceID();
            Name = data.Name;
            Description = data.Description;
            IsBlocked = data.IsBlockedAtStart;

            LoadSceneId = data.LoadSceneId;
            Screenshot = data.Screenshot;
            Dwellers = data.Dwellers;
            Ingredients = data.Ingredients;
        }
    }
}
