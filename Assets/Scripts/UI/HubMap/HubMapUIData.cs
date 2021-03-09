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
        private HubMapUIItem[] _items;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignCitizensListIdsInOrder")]
        private HubMapUICitizen[] _citizens;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignCitiesListIdsInOrder")]
        private HubMapUICity[] _cities;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignLocationsListIdsInOrder")]
        private HubMapUILocation[] _locations;
        [SerializeField, ContextMenuItem("Reassign list ids in order", "ReassignCharactersListIdsInOrder")]
        private HubMapUICharacter[] _characters;
        [SerializeField] 
        private List<int> _inventoryItemsId;

        public HubMapUIItem[] Items => _items;
        public HubMapUICitizen[] Citizens => _citizens;
        public HubMapUICity[] Cities => _cities;
        public HubMapUILocation[] Locations => _locations;
        public HubMapUICharacter[] Characters => _characters;
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
            for (int i = 0; i < _items.Length; i++) _items[i].SetId(i);
            _nextItemId = _items.Length;
        }

        private void ReassignCitizensListIdsInOrder()
        {
            for (int i = 0; i < _citizens.Length; i++) _citizens[i].SetId(i);
            _nextCitizenId = _citizens.Length;
        }

        private void ReassignCitiesListIdsInOrder()
        {
            for (int i = 0; i < _cities.Length; i++) _cities[i].SetId(i);
            _nextCityId = _cities.Length;
        }

        private void ReassignLocationsListIdsInOrder()
        {
            for (int i = 0; i < _locations.Length; i++) _locations[i].SetId(i);
            _nextLocationId = _locations.Length;
        }

        private void ReassignCharactersListIdsInOrder()
        {
            for (int i = 0; i < _characters.Length; i++) _characters[i].SetId(i);
            _nextCharacterId = _characters.Length;
        }

        private void OnValidate()
        {
            if (_items[0].Id != 0)
            {
                Debug.LogWarning("The first element of the list must be null and have a null ID!");
                bool isItemRemoved = true;
                for (int i = 1; i < _items.Length; i++)
                {
                    if (_items[i].Id == 0)
                    {
                        _items[i] = _items[0];
                        _items[0] = new HubMapUIItem();
                        isItemRemoved = false;
                    }
                    if (isItemRemoved)
                    {
                        //TODO: arrays to collections
                        //_items.Insert(new HubMapUIItem());
                    }
                }
            }

            if (_itemsLength != _items.Length)
            {
                if (_itemsLength < _items.Length)
                {
                    for (int i = 1; i < _items.Length; i++)
                    {
                        if (_items[i].Id == _items[i - 1].Id)
                        {
                            _items[i].SetId(_nextItemId++);
                        }
                    }
                }
                _itemsLength = _items.Length;
            }

            if (_citizensLength != _citizens.Length)
            {
                _citizensLength = _citizens.Length;
            }

            if (_citiesLength != _cities.Length)
            {
                _citiesLength = _cities.Length;
            }

            if (_locationsLength != _locations.Length)
            {
                _locationsLength = _locations.Length;
            }

            if (_charactersLength != _characters.Length)
            {
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

        public Dictionary<IHubMapUICitizen, int> CurrentDialogsNumbers { get; set; }

        private void OnEnable()
        {
            CurrentDialogsNumbers = new Dictionary<IHubMapUICitizen, int>();
            for (int i = 0; i< _citizens.Length; i++)
            {
                CurrentDialogsNumbers.Add(_citizens[i], 0);
            }
        }

        #endregion
    }
}
