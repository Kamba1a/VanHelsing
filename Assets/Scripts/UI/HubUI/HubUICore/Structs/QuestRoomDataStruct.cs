using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct QuestRoomDataStruct
    {
        [SerializeField] private GameObject _questListItemPrefab;
        [SerializeField] private int _huntingQuestAmount;


        public GameObject QuestListItemPrefab => _questListItemPrefab;
        public int HuntingQuestAmount => _huntingQuestAmount;
    }
}
