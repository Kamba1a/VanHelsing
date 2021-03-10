using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapUIData : ScriptableObject
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
        private int _nextItemId;
        private int _nextCitizenId;
        private int _nextCityId;
        private int _nextLocationId;
        private int _nextCharacterId;

        private int _itemsCount;
        private int _citizensCount;
        private int _citiesCount;
        private int _locationsCount;
        private int _charactersCount;

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

        private void OnMovingFirstListItemValidate<T>(List<T> list) where T : HubMapUIBaseEntity
        {
            if (list[0].Id != 0)
            {
                Debug.LogWarning("The first element of the list must be null and have a null ID!");
                bool isItemRemoved = true;
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i].Id == 0)
                    {
                        list[i] = list[0];
                        list[0] = null;
                        isItemRemoved = false;
                    }
                }
                if (isItemRemoved)
                {
                    list.Insert(0, null);
                }
            }
        }

        private void OnAddNewItemInListValidate<T>(List<T> list, ref int lastCount, ref int nextId) where T : HubMapUIBaseEntity
        {
            if (lastCount != list.Count)
            {
                if (lastCount < list.Count)
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        if (list[i].Id == list[i - 1].Id)
                        {
                            list[i].SetId(nextId++);
                        }
                    }
                }
                else if (lastCount > list.Count)
                {
                    nextId = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Id > nextId) nextId = list[i].Id;
                    }
                    nextId++;
                }
                lastCount = list.Count;
            }
        }

        private void OnValidate()
        {
            OnMovingFirstListItemValidate(_items);
            OnAddNewItemInListValidate(_items, ref _itemsCount, ref _nextItemId);

            if (_citizensCount != _citizens.Count)
            {
                _citizensCount = _citizens.Count;
            }

            if (_citiesCount != _cities.Count)
            {
                _citiesCount = _cities.Count;
            }

            if (_locationsCount != _locations.Count)
            {
                _locationsCount = _locations.Count;
            }

            if (_charactersCount != _characters.Count)
            {
                _charactersCount = _characters.Count;
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

            #if UNITY_EDITOR

            _itemsCount = _items.Count;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id > _nextItemId) _nextItemId = _items[i].Id;
            }
            _nextItemId++;

            _citizensCount = _citizens.Count;
            for (int i = 0; i < _citizens.Count; i++)
            {
                if (_citizens[i].Id > _nextCitizenId) _nextCitizenId = _citizens[i].Id;
            }
            _nextCitizenId++;

            _citiesCount = _cities.Count;
            for (int i = 0; i < _cities.Count; i++)
            {
                if (_cities[i].Id > _nextCityId) _nextCityId = _cities[i].Id;
            }
            _nextCityId++;

            _locationsCount = _locations.Count;
            for (int i = 0; i < _locations.Count; i++)
            {
                if (_locations[i].Id > _nextLocationId) _nextLocationId = _locations[i].Id;
            }
            _nextLocationId++;

            _charactersCount = _characters.Count;
            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i].Id > _nextCharacterId) _nextCharacterId = _characters[i].Id;
            }
            _nextCharacterId++;
            
            #endif
        }

        #endregion
    }
}
