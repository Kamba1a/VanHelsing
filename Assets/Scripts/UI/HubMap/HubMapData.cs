using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapData", menuName = "CreateData/HubMapData", order = 0)]
    public class HubMapData: ScriptableObject
    {
        #region SerializeFields

        [SerializeField] private GameObject _citizenUIPrefab;
        [SerializeField] private GameObject _sellingItemUIPrefab;
        [SerializeField] private GameObject _locationTextUIPrefab;
        [SerializeField] private GameObject _characterUIPrefab;
        [SerializeField] private GameObject _equipmentItemUIPrefab;
        [SerializeField] private GameObject _inventoryItemUIPrefab;
        [SerializeField] private int _hikeEquipmentPanelSellAmount;
        [SerializeField] private bool _mapOnStartEnabled;

        #endregion


        # region TEMPORARY DATA FOR TESTING HUB MAP

        [Header("TEMPORARY FOR DEBUG")]
        [SerializeField] private TemporaryItemModel[] _items;
        [SerializeField] private TemporaryDialogModel[] _dialogs;
        [SerializeField] private TemporaryCitizenInfoModel[] _citizens;
        [SerializeField] private TemporaryCityInfoModel[] _cities;
        [SerializeField] private TemporaryLocationInfoModel[] _locations;
        [SerializeField] private TemporaryCharacterModel[] _characters;
        [SerializeField] private List<int> _inventoryItemsId;
        private TemporaryCitizenInfoModel[] _citizensCopy;

        public TemporaryItemModel[] Items => _items;
        public TemporaryDialogModel[] Dialogs => _dialogs;
        public TemporaryCitizenInfoModel[] Citizens => _citizensCopy;
        public TemporaryCityInfoModel[] Cities => _cities;
        public TemporaryLocationInfoModel[] Locations => _locations;
        public TemporaryCharacterModel[] Characters => _characters;
        public List<int> InventoryItemsId => _inventoryItemsId;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _citizensCopy = new TemporaryCitizenInfoModel[_citizens.Length];
            for (int i = 0; i < _citizensCopy.Length; i++) //for debug only
            {
                _citizensCopy[i] = new TemporaryCitizenInfoModel(_citizens[i]);
            }

            for (int i = 0; i < _characters.Length; i++) //for debug only
            {
                int?[] items = new int?[_hikeEquipmentPanelSellAmount];
                for (int j = 0; j < items.Length; j++)
                {
                    if (Random.Range(1, 100) <= 55) items[j] = Random.Range(0, _items.Length);
                    else items[i] = null;
                }
                _characters[i].SetItems(items);
            }
        }

        #endregion


        #region Properties

        public GameObject CitizenUIPrefab => _citizenUIPrefab;
        public GameObject SellingItemUIPrefab => _sellingItemUIPrefab;
        public GameObject LocationTextUIPrefab => _locationTextUIPrefab;
        public GameObject CharacterUIPrefab => _characterUIPrefab;
        public GameObject EquipmentItemUIPrefab => _equipmentItemUIPrefab;
        public GameObject InventoryItemUIPrefab => _inventoryItemUIPrefab;
        public bool MapOnStartEnabled => _mapOnStartEnabled;
        public int HikeEquipmentPanelSellAmount => _hikeEquipmentPanelSellAmount;

        #endregion
    }
}
