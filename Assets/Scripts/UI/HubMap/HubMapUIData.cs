﻿using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapUIData : ScriptableObject
    {
        #region Fields

        [Header("UI prefabs")]
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _locationTextUIPrefab;
        [SerializeField] private GameObject _characterUIPrefab;
        [SerializeField] private GameObject _equipmentSlotUIPrefab;
        [SerializeField] private GameObject _inventorySlotUIPrefab;
        [SerializeField] private GameObject _shopSlotUIPrefab;
        [SerializeField] private GameObject _answerButtonUIPrefab;

        [Header("UI prefs")]
        [SerializeField] private bool _mapOnStartEnabled;

        [Header("Objects on map")]
        [SerializeField] private HubMapUICityData _city;
        [SerializeField] private HubMapUILocation _location_1;
        [SerializeField] private HubMapUILocation _location_2;
        [SerializeField] private HubMapUILocation _location_3;
        [SerializeField] private HubMapUILocation _location_4;

        [Header("Game content for UI")]
        [SerializeField] private HubMapUINewPlayerData _newPlayerData;
        [SerializeField] private int _buyBackStorageSlotsAmount;
        [SerializeField] private int _shopSlotsAmount;
        [SerializeField] private List<HubMapUIQuestData> _quests;
        [SerializeField, ContextMenuItem("Reset ids", "DialogListResetIds")] private List<HubMapUIDialogNode> _dialogs;

        private HubMapUIPlayerModel _player;

        #if UNITY_EDITOR
        private int _dialogsListCount;
        private int _nextDialogsListId;
        #endif

        #endregion


        #region Properties

        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject ShopSlotUIPrefab => _shopSlotUIPrefab;
        public GameObject LocationTextUIPrefab => _locationTextUIPrefab;
        public GameObject CharacterUIPrefab => _characterUIPrefab;
        public GameObject EquipmentSlotUIPrefab => _equipmentSlotUIPrefab;
        public GameObject InventorySlotUIPrefab => _inventorySlotUIPrefab;
        public GameObject AnswerButtonUIPrefab => _answerButtonUIPrefab;

        public bool MapOnStartEnabled => _mapOnStartEnabled;

        public HubMapUICityData City => _city;
        public HubMapUILocation Location_1 => _location_1;
        public HubMapUILocation Location_2 => _location_2;
        public HubMapUILocation Location_3 => _location_3;
        public HubMapUILocation Location_4 => _location_4;

        public int BuyBackStorageSlotsAmount => _buyBackStorageSlotsAmount;
        public List<HubMapUIDialogNode> Dialogs => _dialogs;
        public HubMapUIDialogsController DialogsController { get; private set; }
        public HubMapUIQuestService QuestService { get; private set; }
        public HubMapUIPlayerModel Player 
        {
            get
            {
                if (_player == null)
                {
                    _player = new HubMapUIPlayerModel(_newPlayerData);
                }
                return _player;
            }
            private set
            {
                _player = value;
            }
        }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            DialogsController = new HubMapUIDialogsController();
            QuestService = new HubMapUIQuestService(_quests);

            #if UNITY_EDITOR
            _dialogsListCount = _dialogs.Count;
            _nextDialogsListId = NextDialogsListId();
            #endif
        }

        private void OnValidate()
        {
            OnChangeDialogListIdValidate();
        }

        #endregion


        #region Methods

        #if UNITY_EDITOR
        private void OnChangeDialogListIdValidate()
        {
            if (_dialogsListCount != _dialogs.Count)
            {
                if (_dialogsListCount < _dialogs.Count)
                {
                    for (int i = 1; i < _dialogs.Count; i++)
                    {
                        if (_dialogs[i].Id == _dialogs[i - 1].Id)
                        {
                            _dialogs[i].SetId(_nextDialogsListId++);
                        }
                    }
                }
                else if (_dialogsListCount > _dialogs.Count)
                {
                    _nextDialogsListId = NextDialogsListId();
                }
                _dialogsListCount = _dialogs.Count;
            }
        }

        private int NextDialogsListId()
        {
            _nextDialogsListId = 0;
            for (int i = 0; i < _dialogs.Count; i++)
            {
                if (_dialogs[i].Id > _nextDialogsListId) _nextDialogsListId = _dialogs[i].Id;
            }
            return ++_nextDialogsListId;
        }

        private void DialogListResetIds() //ContextMenuItem
        {
            for (int i = 0; i < _dialogs.Count; i++) _dialogs[i].SetId(i);
            _nextDialogsListId = _dialogs.Count;
        }
        #endif

        #endregion
    }
}
