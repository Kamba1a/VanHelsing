﻿using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapUIData: ScriptableObject
    {
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _sellingItemUIPrefab;
        [SerializeField] private GameObject _locationTextUIPrefab;
        [SerializeField] private GameObject _characterUIPrefab;
        [SerializeField] private GameObject _equipmentCellUIPrefab;
        [SerializeField] private GameObject _inventoryItemUIPrefab;
        [SerializeField] private int _hikeEquipmentPanelCellAmount;
        [SerializeField] private bool _mapOnStartEnabled;

        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public GameObject LocationTextUIPrefab => _locationTextUIPrefab;
        public GameObject CharacterUIPrefab => _characterUIPrefab;
        public GameObject EquipmentCellUIPrefab => _equipmentCellUIPrefab;
        public GameObject InventoryItemUIPrefab => _inventoryItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public int HikeEquipmentPanelCellAmount => _hikeEquipmentPanelCellAmount;


        #region TEMPORARY CONTENT FOR TESTING HUB MAP

        [Header("TEMPORARY CONTENT FOR TESTING HUB MAP")]
        [SerializeField] private HubMapUIItem[] _items;
        [SerializeField] private HubMapUICitizen[] _citizens;
        [SerializeField] private HubMapUICity[] _cities;
        [SerializeField] private HubMapUILocation[] _locations;
        [SerializeField] private HubMapUICharacter[] _characters;
        [SerializeField] private List<int> _inventoryItemsId;

        public HubMapUIItem[] Items => _items;
        public HubMapUICitizen[] Citizens => _citizens;
        public HubMapUICity[] Cities => _cities;
        public HubMapUILocation[] Locations => _locations;
        public HubMapUICharacter[] Characters => _characters;
        public List<int> InventoryItemsId => _inventoryItemsId;

        #if UNITY_EDITOR
        private int _itemsLength;
        private int _citizensLength;
        private int _citiesLength;
        private int _locationsLength;
        private int _charactersLength;

        private void OnValidate()
        {
            if (_itemsLength != _items.Length)
            {
                for (int i = 0; i < _items.Length; i++) _items[i].SetId(i);
                _itemsLength = _items.Length;
            }

            if (_citizensLength != _citizens.Length)
            {
                for (int i = 0; i < _citizens.Length; i++) _citizens[i].SetId(i);
                _citizensLength = _citizens.Length;
            }

            if (_citiesLength != _cities.Length)
            {
                for (int i = 0; i < _cities.Length; i++) _cities[i].SetId(i);
                _citiesLength = _cities.Length;
            }

            if (_locationsLength != _locations.Length)
            {
                for (int i = 0; i < _locations.Length; i++) _locations[i].SetId(i);
                _locationsLength = _locations.Length;
            }

            if (_charactersLength != _characters.Length)
            {
                for (int i = 0; i < _characters.Length; i++) _characters[i].SetId(i);
                _charactersLength = _characters.Length;
            }

            for (int i = 0; i < _characters.Length; i++)
            {
                if (_characters[i].ItemsId.Length != _hikeEquipmentPanelCellAmount)
                {
                    _characters[i].SetEquipmentsSize(_hikeEquipmentPanelCellAmount);
                }
            }
        }
        #endif

        public Dictionary<int, int> CurrentDialogsNumbers { get; set; }

        private void OnEnable()
        {
            CurrentDialogsNumbers = new Dictionary<int, int>();
            for (int i = 0; i< _characters.Length; i++)
            {
                CurrentDialogsNumbers.Add(_characters[i].Id, 0);
            }
        }

        #endregion
    }
}
