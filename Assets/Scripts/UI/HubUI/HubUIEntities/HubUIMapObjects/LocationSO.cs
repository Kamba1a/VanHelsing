using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "Location", menuName = "CreateData/HubUIData/Location", order = 0)]
    public class LocationSO : MapObjectSO
    {
        #region Fields

        [SerializeField] private LocationStruct _locationData;

        //toremove
        [Header("Location data")]
        [SerializeField] private int _loadSceneId;
        [SerializeField] private Sprite _screenshot;
        [SerializeField] private int _travelTime;
        [SerializeField] private DwellerSO[] _dwellers;
        [SerializeField] private IngredientItemSO[] _ingredients;

        #endregion


        #region Properties

        public LocationStruct LocationData => _locationData;

        //toremove
        public int LoadSceneId => _loadSceneId;
        public Sprite Screenshot => _screenshot;
        public int TravelTime => _travelTime;
        public DwellerSO[] Dwellers => _dwellers;
        public IngredientItemSO[] Ingredients => _ingredients;

        #endregion


        #region HubMapUIMapObjectData

        public override MapObjectType GetMapObjectType()
        {
            return MapObjectType.Location;
        }

        #endregion
    }
}
