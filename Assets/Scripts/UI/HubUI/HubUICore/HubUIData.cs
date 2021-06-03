using UnityEngine;


namespace BeastHunterHubUI
{
    [CreateAssetMenu(fileName = "HubUIData", menuName = "CreateData/HubUIData/MainData", order = 0)]
    public class HubUIData : ScriptableObject
    {
        #region Fields

        [Header("STARTING GAME DATA")]
        [SerializeField] private GameDataStruct _startGameDataStruct;


        [Header("GLOBAL SETTINGS")]
        [SerializeField] private GameTimeSettings _gameTimeSettings;
        [SerializeField] private int _charactersAmountForHire;

        //TODO: rework entity datas to start settings and global settings
        [Header("ENTITIES SETTINGS")]
        [SerializeField] private AllCharactersData _charactersData;
        [SerializeField] private int _citiesShopsSlotsAmount;


        [Space(20, order = 1), Header("GLOBAL DATA POOLS", order = 2)]
        [SerializeField] private QuestData[] _questsPool;
        [SerializeField] private HuntingQuestData[] _huntingQuestsDataPool;
        [SerializeField] private BossData[] _bossesDataPool;
        [SerializeField] private ItemDataPools _itemDataPools;

        [Space(20, order = 1), Header("UI PREFABS", order = 2)]
        [SerializeField] private GameObject _messageWindowPrefab;

        [Space(20, order = 1), Header("MAP ROOM SETTINGS", order = 2)]
        public MapDataStruct MapDataStruct;

        [Space(20, order = 1), Header("QUEST ROOM SETTINGS", order = 2)]
        public QuestRoomDataStruct QuestRoomDataStruct;

        [Space(20, order = 1), Header("WORK ROOM SETTINGS", order = 2)]
        public WorkRoomUIDataStruct WorkRoomDataStruct;

        #endregion


        #region Properties

        public GameDataStruct StartGameDataStruct => _startGameDataStruct;
        public GameTimeSettings GameTimeSettings => _gameTimeSettings;
        public GameObject MessageWindowPrefab => _messageWindowPrefab;
        public AllCharactersData AllCharactersData => _charactersData;
        public QuestData[] QuestsPool => _questsPool;
        public int CitiesShopsSlotsAmount => _citiesShopsSlotsAmount;
        public int CharactersAmountForHire => _charactersAmountForHire;
        public ItemDataPools ItemDataPools => _itemDataPools;
        public BossData[] BossesDataPool => _bossesDataPool;
        public HuntingQuestData[] HuntingQuestsDataPool => _huntingQuestsDataPool;

        #endregion
    }
}
