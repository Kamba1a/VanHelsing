using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapUIData : ScriptableObject
    {
        #region Fields

        [Header("UI prefabs")]
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _sellingItemUIPrefab;
        [SerializeField] private GameObject _locationTextUIPrefab;
        [SerializeField] private GameObject _characterUIPrefab;
        [SerializeField] private GameObject _equipmentCellUIPrefab;
        [SerializeField] private GameObject _inventoryItemUIPrefab;

        [Header("UI prefs")]
        [SerializeField] private int _hikeEquipmentPanelCellAmount;
        [SerializeField] private bool _mapOnStartEnabled;

        [Header("Objects on map ids")]
        [SerializeField] private int _cityId;
        [SerializeField] private int _locationId_1;
        [SerializeField] private int _locationId_2;
        [SerializeField] private int _locationId_3;
        [SerializeField] private int _locationId_4;

        [Header("Content for UI")]
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

#endif

        #endregion


        #region Properties

        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public GameObject LocationTextUIPrefab => _locationTextUIPrefab;
        public GameObject CharacterUIPrefab => _characterUIPrefab;
        public GameObject EquipmentCellUIPrefab => _equipmentCellUIPrefab;
        public GameObject InventoryItemUIPrefab => _inventoryItemUIPrefab;

        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public int HikeEquipmentPanelCellAmount => _hikeEquipmentPanelCellAmount;

        public int CityId => _cityId;
        public int LocationId_1 => _locationId_1;
        public int LocationId_2 => _locationId_2;
        public int LocationId_3 => _locationId_3;
        public int LocationId_4 => _locationId_4;

        public List<HubMapUIItem> Items => _items;
        public List<HubMapUICitizen> Citizens => _citizens;
        public List<HubMapUICity> Cities => _cities;
        public List<HubMapUILocation> Locations => _locations;
        public List<HubMapUICharacter> Characters => _characters;
        public List<int> InventoryItemsId => _inventoryItemsId;
        public Dictionary<IHubMapUICitizen, int> CurrentDialogsNumbers { get; set; }

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            CurrentDialogsNumbersDictionaryInitialize();

#if UNITY_EDITOR

            _itemsCount = _items.Count;
            _nextItemId = NextId(_items);

            _citizensCount = _citizens.Count;
            _nextCitizenId = NextId(_citizens);

            _citiesCount = _cities.Count;
            _nextCityId = NextId(_cities);

            _locationsCount = _locations.Count;
            _nextLocationId = NextId(_locations);

            _charactersCount = _characters.Count;
            _nextCharacterId = NextId(_characters);

#endif
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            OnMovingFirstListItemValidate(_items);
            OnAddNewItemInListValidate(_items, ref _itemsCount, ref _nextItemId);
            OnAddNewItemInListValidate(_citizens, ref _citizensCount, ref _nextCitizenId);
            OnAddNewItemInListValidate(_cities, ref _citiesCount, ref _nextCityId);
            OnAddNewItemInListValidate(_locations, ref _locationsCount, ref _nextLocationId);
            OnAddNewItemInListValidate(_characters, ref _charactersCount, ref _nextCharacterId);

            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i].ItemsId.Length != _hikeEquipmentPanelCellAmount)
                {
                    _characters[i].SetEquipmentsSize(_hikeEquipmentPanelCellAmount);
                }
            }
        }

#endif

        #endregion


        #region Methods

        private void CurrentDialogsNumbersDictionaryInitialize()
        {
            CurrentDialogsNumbers = new Dictionary<IHubMapUICitizen, int>();
            for (int i = 0; i < _citizens.Count; i++)
            {
                CurrentDialogsNumbers.Add(_citizens[i], 0);
            }
        }

#if UNITY_EDITOR

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

        private void ReassignItemsListIdsInOrder() => SeIdsToAllListElements(_items, ref _nextItemId);
        private void ReassignCitizensListIdsInOrder() => SeIdsToAllListElements(_citizens, ref _nextCitizenId);
        private void ReassignCitiesListIdsInOrder() => SeIdsToAllListElements(_cities, ref _nextCityId);
        private void ReassignLocationsListIdsInOrder() => SeIdsToAllListElements(_locations, ref _nextLocationId);
        private void ReassignCharactersListIdsInOrder() => SeIdsToAllListElements(_characters, ref _nextCharacterId);

        private void SeIdsToAllListElements<T>(List<T> list, ref int nextId) where T : HubMapUIBaseEntity
        {
            for (int i = 0; i < list.Count; i++) list[i].SetId(i);
            nextId = list.Count;
        }

        private void OnMovingFirstListItemValidate<T>(List<T> list) where T : HubMapUIBaseEntity
        {
            if (list.Count > 0)
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
            else
            {
                list.Add(null);
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
                    nextId = NextId(list);
                }
                lastCount = list.Count;
            }
        }

        private int NextId<T>(List<T> list) where T : HubMapUIBaseEntity
        {
            int nextId = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id > nextId) nextId = list[i].Id;
            }
            return ++nextId;
        }

#endif

        #endregion
    }
}
