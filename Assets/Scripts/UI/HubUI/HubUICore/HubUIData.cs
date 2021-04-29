using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "HubUIData", menuName = "CreateData/HubUIData/MainData", order = 0)]
    public class HubUIData : ScriptableObject
    {
        #region Fields

        [Header("GAME CONTENT")]
        [SerializeField] private AllCharactersData _charactersData;
        [SerializeField] private QuestData[] _questsPool;
        [SerializeField] private LocationData[] _locationDatas;
        [SerializeField] private CityData[] _citiesDatas;
        [SerializeField] private int _citiesShopsSlotsAmount;
        [SerializeField] private ItemDataPools _itemDataPools;
        public PlayerSettingsStruct PlayerSettings;
        public HubUITimeSettingsStruct TimeSettings;

        [Space(20, order = 1), Header("UI PREFABS", order = 2)]
        [SerializeField] private GameObject _messageWindowPrefab;

        [Space(20, order = 1), Header("MAP DATA SETTINGS", order = 2)]
        public MapDataStruct MapDataStruct;

        #endregion


        #region Properties

        public GameObject MessageWindowPrefab => _messageWindowPrefab;
        public AllCharactersData AllCharactersData => _charactersData;
        public QuestData[] QuestsPool => _questsPool;
        public LocationData[] Locations => (LocationData[])_locationDatas.Clone();
        public CityData[] Cities => (CityData[])_citiesDatas.Clone();
        public int CitiesShopsSlotsAmount => _citiesShopsSlotsAmount;
        public ItemDataPools ItemDataPools => _itemDataPools;

        #endregion
    }
}
