using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _sellingItemUIPrefab;
        [SerializeField] private GameObject _locationTextUIPrefab;
        [SerializeField] private GameObject _characterUIPrefab;
        [SerializeField] private GameObject _equipmentItemUIPrefab;
        [SerializeField] private GameObject _inventoryItemUIPrefab;
        [SerializeField] private int _hikeEquipmentPanelCellAmount;
        [SerializeField] private bool _mapOnStartEnabled;

        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public GameObject LocationTextUIPrefab => _locationTextUIPrefab;
        public GameObject CharacterUIPrefab => _characterUIPrefab;
        public GameObject EquipmentItemUIPrefab => _equipmentItemUIPrefab;
        public GameObject InventoryItemUIPrefab => _inventoryItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public int HikeEquipmentPanelCellAmount => _hikeEquipmentPanelCellAmount;


        #region TEMPORARY CONTENT FOR TESTING HUB MAP

        [Header("TEMPORARY CONTENT FOR TESTING HUB MAP")]
        [SerializeField] private TemporaryItemModel[] _items;
        [SerializeField] private TemporaryDialogModel[] _dialogs;
        [SerializeField] private TemporaryCitizenInfoModel[] _citizens;
        [SerializeField] private TemporaryCityInfoModel[] _cities;
        [SerializeField] private TemporaryLocationInfoModel[] _locations;
        [SerializeField] private TemporaryCharacterModel[] _characters;
        [SerializeField] private List<int> _inventoryItemsId;

        private TemporaryCitizenInfoModel[] _citizensCopy;  //TODO: изменить переход по диалогам так, чтобы не затрагивать данные самих жителей в SO

        public TemporaryItemModel[] Items => _items;
        public TemporaryDialogModel[] Dialogs => _dialogs;
        public TemporaryCitizenInfoModel[] Citizens => _citizensCopy;
        public TemporaryCityInfoModel[] Cities => _cities;
        public TemporaryLocationInfoModel[] Locations => _locations;
        public TemporaryCharacterModel[] Characters => _characters;
        public List<int> InventoryItemsId => _inventoryItemsId;

        #if UNITY_EDITOR
        private int _itemsLength;
        private int _dialogsLength;
        private int _citizensLength;
        private int _citiesLength;
        private int _locationsLength;
        private int _charactersLength;

        private void OnValidate()
        {
            if (_itemsLength != _items.Length)
            {
                if (_itemsLength < _items.Length)
                for (int i = 0; i < _items.Length; i++) _items[i].SetId(i);
                _itemsLength = _items.Length;
            }

            if (_dialogsLength != _dialogs.Length)
            {
                for (int i = 0; i < _dialogs.Length; i++) _dialogs[i].SetId(i);
                _dialogsLength = _dialogs.Length;
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

        private void OnEnable()
        {
            //временная заглушка, чтобы не менялись данные массива _citizens в SO
            _citizensCopy = new TemporaryCitizenInfoModel[_citizens.Length];
            for (int i = 0; i < _citizensCopy.Length; i++)
            {
                _citizensCopy[i] = new TemporaryCitizenInfoModel(_citizens[i]);
            }
        }

        #endregion
    }
}
