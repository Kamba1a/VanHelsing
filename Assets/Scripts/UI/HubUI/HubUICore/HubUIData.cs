using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "HubUIData", menuName = "CreateData/HubUIData/MainData", order = 0)]
    public class HubUIData : ScriptableObject
    {
        #region Fields

        //TODO: rework entity datas to start settings and global settings

        [Header("STARTING GAME DATA")]
        [SerializeField] private GameData _startGameDataStruct;

        [Space(20, order = 1), Header("GLOBAL DATA", order = 2)]
        [SerializeField] private GameTimeGlobalData _gameTimeGlobalData;
        [SerializeField] private CharactersGlobalData _charactersGlobalData;
        [SerializeField] private int _citiesShopsSlotsAmount;

        [Space(20, order = 1), Header("OBJECTS POOLS", order = 2)]
        [SerializeField] private QuestSO[] _questsPool;
        [SerializeField] private HuntingQuestSO[] _huntingQuestsDataPool;
        [SerializeField] private BossDataSO[] _bossesDataPool;
        [SerializeField] private ItemSOPools _itemDataPools;

        [Space(20, order = 1), Header("UI PREFABS", order = 2)]
        [SerializeField] private GameObject _messageWindowPrefab;

        [Space(20, order = 1), Header("MAP ROOM SETTINGS", order = 2)]
        [SerializeField] public MapDataStruct MapDataStruct;

        [Space(20, order = 1), Header("QUEST ROOM SETTINGS", order = 2)]
        [SerializeField] public QuestRoomDataStruct QuestRoomDataStruct;

        [Space(20, order = 1), Header("WORK ROOM SETTINGS", order = 2)]
        [SerializeField] public WorkRoomUIDataStruct WorkRoomDataStruct;

        #endregion


        #region Properties

        public CharactersGlobalData CharactersGlobalData => _charactersGlobalData;
        public GameData StartGameDataStruct => _startGameDataStruct;
        public GameTimeGlobalData GameTimeGlobalData => _gameTimeGlobalData;
        public GameObject MessageWindowPrefab => _messageWindowPrefab;
        public QuestSO[] QuestsPool => _questsPool;
        public int CitiesShopsSlotsAmount => _citiesShopsSlotsAmount;
        public ItemSOPools ItemDataPools => _itemDataPools;
        public BossDataSO[] BossesDataPool => _bossesDataPool;
        public HuntingQuestSO[] HuntingQuestsDataPool => _huntingQuestsDataPool;

        #endregion
    }
}
