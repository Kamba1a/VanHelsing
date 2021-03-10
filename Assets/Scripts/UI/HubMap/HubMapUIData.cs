using System;
using System.Collections.Generic;
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
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignItemsListIdsInOrder")]
        private List<HubMapUIItem> _items;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignCitizensListIdsInOrder")]
        private List<HubMapUICitizen> _citizens;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignCitiesListIdsInOrder")]
        private List<HubMapUICity> _cities;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignLocationsListIdsInOrder")]
        private List<HubMapUILocation> _locations;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignCharactersListIdsInOrder")]
        private List<HubMapUICharacter> _characters;
        [SerializeField]
        private List<int> _inventoryItemsId;

        public List<HubMapUIItem> Items => _items;
        public List<HubMapUICitizen> Citizens => _citizens;
        public List<HubMapUICity> Cities => _cities;
        public List<HubMapUILocation> Locations => _locations;
        public List<HubMapUICharacter> Characters => _characters;
        public List<int> InventoryItemsId => _inventoryItemsId;

        #if UNITY_EDITOR
        [HideInInspector] private int _nextItemId;
        [HideInInspector] private int _nextCitizenId;
        [HideInInspector] private int _nextCityId;
        [HideInInspector] private int _nextLocationId;
        [HideInInspector] private int _nextCharacterId;

        private int _itemsLength;
        private int _citizensLength;
        private int _citiesLength;
        private int _locationsLength;
        private int _charactersLength;

        [ContextMenu("Reassign all lists ids in order")]
        private void ReassignAllListsIdsInOrder()
        {
            ReassignItemsListIdsInOrder();
            ReassignCitizensListIdsInOrder();
            ReassignCitiesListIdsInOrder();
            ReassignLocationsListIdsInOrder();
            ReassignCharactersListIdsInOrder();
            Debug.Log("All identifiers in all lists have been reassigned in order");
        }

        private void ReassignItemsListIdsInOrder()
        {
            for (int i = 0; i < _items.Count; i++) _items[i].SetId(i);
            _nextItemId = _items.Count;
        }

        private void ReassignCitizensListIdsInOrder()
        {
            for (int i = 0; i < _citizens.Count; i++) _citizens[i].SetId(i);
            _nextCitizenId = _citizens.Count;
        }

        private void ReassignCitiesListIdsInOrder()
        {
            for (int i = 0; i < _cities.Count; i++) _cities[i].SetId(i);
            _nextCityId = _cities.Count;
        }

        private void ReassignLocationsListIdsInOrder()
        {
            for (int i = 0; i < _locations.Count; i++) _locations[i].SetId(i);
            _nextLocationId = _locations.Count;
        }

        private void ReassignCharactersListIdsInOrder()
        {
            for (int i = 0; i < _characters.Count; i++) _characters[i].SetId(i);
            _nextCharacterId = _characters.Count;
        }

        //private void MovingListItemsValidate(List<HubMapUIBaseEntity> list)
        //{
        //    if (list[0].Id != 0)
        //    {
        //        Debug.LogWarning("The first element of the list must be null and have a null ID!");
        //        bool isItemRemoved = true;
        //        for (int i = 1; i < list.Count; i++)
        //        {
        //            if (list[i].Id == 0)
        //            {
        //                list[i] = list[0];
        //                list[0] = null;
        //                isItemRemoved = false;
        //            }
        //            if (isItemRemoved)
        //            {
        //                list.Insert(0, new HubMapUIItem());
        //            }
        //        }
        //    }
        //}

        //private void ListValidate2(List<HubMapUIBaseEntity> list)
        //{
        //    if (_itemsLength != list.Count)
        //    {
        //        if (_itemsLength < list.Count)
        //        {
        //            for (int i = 1; i < list.Count; i++)
        //            {
        //                if (list[i].Id == list[i - 1].Id)
        //                {
        //                    list[i].SetId(_nextItemId++);
        //                }
        //            }
        //        }
        //        _itemsLength = list.Count;
        //    }
        //}

        private void OnValidate()
        {
            if (_items[0].Id != 0)
            {
                Debug.LogWarning("The first element of the list must be null and have a null ID!");
                bool isItemRemoved = true;
                for (int i = 1; i < _items.Count; i++)
                {
                    if (_items[i].Id == 0)
                    {
                        _items[i] = _items[0];
                        _items[0] = null;
                        isItemRemoved = false;
                    }
                }
                if (isItemRemoved)
                {
                    _items.Insert(0, new HubMapUIItem());
                }
            }

            if (_itemsLength != _items.Count)
            {
                if (_itemsLength < _items.Count)
                {
                    for (int i = 1; i < _items.Count; i++)
                    {
                        if (_items[i].Id == _items[i - 1].Id)
                        {
                            _items[i].SetId(_nextItemId++);
                        }
                    }
                }
                _itemsLength = _items.Count;
            }

            if (_citizensLength != _citizens.Count)
            {
                _citizensLength = _citizens.Count;
            }

            if (_citiesLength != _cities.Count)
            {
                _citiesLength = _cities.Count;
            }

            if (_locationsLength != _locations.Count)
            {
                _locationsLength = _locations.Count;
            }

            if (_charactersLength != _characters.Count)
            {
                _charactersLength = _characters.Count;
            }

            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i].ItemsId.Length != _hikeEquipmentPanelCellAmount)
                {
                    _characters[i].SetEquipmentsSize(_hikeEquipmentPanelCellAmount);
                }
            }
        }
        #endif

        public Dictionary<IHubMapUICitizen, int> CurrentDialogsNumbers { get; set; }

        private void OnEnable()
        {
            CurrentDialogsNumbers = new Dictionary<IHubMapUICitizen, int>();
            for (int i = 0; i< _citizens.Count; i++)
            {
                CurrentDialogsNumbers.Add(_citizens[i], 0);
            }
        }

        #endregion
    }
}
