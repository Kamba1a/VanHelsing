﻿using UnityEngine;


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
        [SerializeField] private int _randomCharactersAmount;
        [SerializeField] private ItemDataPools _itemDataPools;
        [SerializeField] private HuntingQuestData[] _huntingQuestsDataPool;
        [SerializeField] private BossData[] _bossesDataPool;
        public PlayerSettingsStruct PlayerSettings;
        public HubUITimeSettingsStruct TimeSettings;

        [Space(20, order = 1), Header("UI PREFABS", order = 2)]
        [SerializeField] private GameObject _messageWindowPrefab;

        [Space(20, order = 1), Header("MAP ROOM SETTINGS", order = 2)]
        public MapDataStruct MapDataStruct;

        [Space(20, order = 1), Header("QUEST ROOM SETTINGS", order = 2)]
        public QuestRoomDataStruct QuestRoomDataStruct;

        #endregion


        #region Properties

        public GameObject MessageWindowPrefab => _messageWindowPrefab;
        public AllCharactersData AllCharactersData => _charactersData;
        public QuestData[] QuestsPool => _questsPool;
        public LocationData[] Locations => (LocationData[])_locationDatas.Clone();
        public CityData[] Cities => (CityData[])_citiesDatas.Clone();
        public int CitiesShopsSlotsAmount => _citiesShopsSlotsAmount;
        public int RandomCharactersAmount => _randomCharactersAmount;
        public ItemDataPools ItemDataPools => _itemDataPools;
        public BossData[] BossesDataPool => _bossesDataPool;
        public HuntingQuestData[] HuntingQuestsDataPool => _huntingQuestsDataPool;

        #endregion
    }
}
