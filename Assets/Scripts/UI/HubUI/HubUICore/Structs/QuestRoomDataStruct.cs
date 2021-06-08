﻿using UnityEngine;


namespace BeastHunterHubUI
{
    [System.Serializable]
    public struct QuestRoomDataStruct
    {
        #region Fields

        [SerializeField] private GameObject _questListItemPrefab;
        [SerializeField] private int _huntingQuestAmount;

        #endregion


        #region Properties

        public GameObject QuestListItemPrefab => _questListItemPrefab;
        public int HuntingQuestAmount => _huntingQuestAmount;

        #endregion
    }
}
