using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUIBehaviour : MonoBehaviour
    {
        #region PrivateData

        private enum StorageType
        {
            None = 0,
            Equipment = 1,
            Inventory = 2,
            Shop = 3,
            BuyBackStorage = 4,
        }

        private class SelectedElements
        {
            private int? _equipmentSlotIndex;
            private int? _inventorySlotIndex;
            private int? _shopSlotIndex;
            private int? _buyBackSlotIndex;
            private HubMapUICharacter _character;

            public Action<int?> OnChanged_EquipmentSlotIndex { get; set; }
            public Action<int?> OnChanged_InventorySlotIndex { get; set; }
            public Action<int?> OnChanged_ShopSlotIndex { get; set; }
            public Action<int?> OnChanged_BuyBackSlotIndex { get; set; }
            public Action<HubMapUICharacter> OnChanged_Character { get; set; }

            public int? EquipmentSlotIndex
            {
                get
                {
                    return _equipmentSlotIndex;
                }
                set
                {
                    if (value != _equipmentSlotIndex)
                    {
                        int? previousValue = _equipmentSlotIndex;
                        _equipmentSlotIndex = value;
                        OnChanged_EquipmentSlotIndex?.Invoke(previousValue);
                    }
                }
            }

            public int? InventorySlotIndex
            {
                get
                {
                    return _inventorySlotIndex;
                }
                set
                {
                    if (value != _inventorySlotIndex)
                    {
                        int? previousValue = _inventorySlotIndex;
                        _inventorySlotIndex = value;
                        OnChanged_InventorySlotIndex?.Invoke(previousValue);
                    }
                }
            }

            public int? ShopSlotIndex
            {
                get
                {
                    return _shopSlotIndex;
                }
                set
                {
                    if (value != _shopSlotIndex)
                    {
                        int? previousValue = _shopSlotIndex;
                        _shopSlotIndex = value;
                        OnChanged_ShopSlotIndex?.Invoke(previousValue);
                    }
                }
            }

            public int? BuyBackSlotIndex
            {
                get
                {
                    return _buyBackSlotIndex;
                }
                set
                {
                    if (value != _buyBackSlotIndex)
                    {
                        int? previousValue = _buyBackSlotIndex;
                        _buyBackSlotIndex = value;
                        OnChanged_BuyBackSlotIndex?.Invoke(previousValue);
                    }
                }
            }

            public HubMapUICharacter Character
            {
                get
                {
                    return _character;
                }
                set
                {
                    if (value != _character)
                    {
                        HubMapUICharacter previousValue = _character;
                        _character = value;
                        OnChanged_Character?.Invoke(previousValue);
                    }
                }
            }

            public void RemoveAllListeners()
            {
                OnChanged_EquipmentSlotIndex = null;
                OnChanged_InventorySlotIndex = null;
                OnChanged_ShopSlotIndex = null;
                OnChanged_BuyBackSlotIndex = null;
                OnChanged_Character = null;
            }
        }

        #endregion


        #region Constants

        private const float CHARACTERS_PANEL_SCROLLBAR_STEP = 1.0f;

        #endregion


        #region Fields

        [Header("Map objects")]
        [SerializeField] private Button _cityButton;
        [SerializeField] private Button _locationButton_1;
        [SerializeField] private Button _locationButton_2;
        [SerializeField] private Button _locationButton_3;
        [SerializeField] private Button _locationButton_4;

        [Header("Hub map")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;
        [SerializeField] private Button _hideInfoPanelButton;
        [SerializeField] private Button _hubButton;
        [SerializeField] private Button _mapButton;
        [SerializeField] private GameObject _inventoryItemsPanel;

        [Header("City info panel")]
        [SerializeField] private GameObject _cityInfoPanel;
        [SerializeField] private GameObject _citizenPanel;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Image _cityFraction;
        [SerializeField] private Text _cityName;
        [SerializeField] private Text _cityDescription;
        [SerializeField] private Text _cityReputation;

        [Header("Location info panel")]
        [SerializeField] private GameObject _locationInfoPanel;
        [SerializeField] private GameObject _dwellersPanel;
        [SerializeField] private GameObject _ingredientsPanel;
        [SerializeField] private Text _locationName;
        [SerializeField] private Text _locationDescription;
        [SerializeField] private Image _locationScreen;
        [SerializeField] private Button _hikeButton;

        [Header("Dialog panel")]
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private Image _citizenPortrait;
        [SerializeField] private Text _citizenName;
        [SerializeField] private Text _dialogText;
        [SerializeField] private GameObject _answerButtonsPanel;

        [Header("Hike panel")]
        [SerializeField] private Button _hikePanelButton;
        [SerializeField] private Button _closeHikePanelButton;
        [SerializeField] private Button _hikeAcceptButton;
        [SerializeField] private GameObject _hikePanel;
        [SerializeField] private GameObject _hikePreparePanel;
        [SerializeField] private GameObject _charactersPanel;
        [SerializeField] private GameObject _equipmentPanel;
        [SerializeField] private GameObject _hikeInventoryPanel;
        [SerializeField] private GameObject _hikeInventoryScrollView;
        [SerializeField] private Button _closeInventoryButton;
        [SerializeField] private Scrollbar _charactersPanelScrollbar;
        [SerializeField] private Button _charactersPanelNextButton;
        [SerializeField] private Button _charactersPanelPreviousButton;
        [SerializeField] private Button _perkTreeButton;

        [Header("Trade panel")]
        [SerializeField] private GameObject _tradePanel;
        [SerializeField] private GameObject _shopInventoryScrollView;
        [SerializeField] private GameObject _buyingItemsPanel;
        [SerializeField] private GameObject _buyBackItemsPanel;
        [SerializeField] private Button _closeTradePanelButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _buyBackButton;
        [SerializeField] private Text _sellingItemPrice;
        [SerializeField] private Text _buyingItemPrice;
        [SerializeField] private Text _buybackItemPrice;
        [SerializeField] private Text _shopCityReputation;


        private List<GameObject> _rightInfoPanelObjectsForDestroy;
        private List<HubMapUISlotBehaviour> _equipmentSlotsUIBehaviours;
        private List<HubMapUISlotBehaviour> _inventorySlotsUIBehaviours;
        private List<HubMapUISlotBehaviour> _shopSlotsUIBehaviours;
        private List<HubMapUISlotBehaviour> _buyBackSlotsUIBehaviours;
        private List<HubMapUICharacterBehaviour> _charactersUIBehaviours;
        private Dictionary<HubMapUICitizen, HubMapUICitizenBehaviour> _displayedCurrentCitizensUIBehaviours;
        private List<GameObject> _displayedDialogAnswerButtons;
        private (int? slotIndex, StorageType storageType) _draggedItemInfo;
        private HubMapUILocation _selectedLocation;
        private HubMapUICity _selectedCity;
        private HubMapUIStorage _inventory;
        private HubMapUIStorage _buyBackStorage;
        private SelectedElements _selected;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _cityButton.onClick.AddListener(() => OnClick_CityButton(Data.HubMapData.City));
            _locationButton_1.onClick.AddListener(() => OnClick_LocationButton(Data.HubMapData.Location_1));
            _locationButton_2.onClick.AddListener(() => OnClick_LocationButton(Data.HubMapData.Location_2));
            _locationButton_3.onClick.AddListener(() => OnClick_LocationButton(Data.HubMapData.Location_3));
            _locationButton_4.onClick.AddListener(() => OnClick_LocationButton(Data.HubMapData.Location_4));

            _hubButton.onClick.AddListener(OnClick_HubButton);
            _mapButton.onClick.AddListener(OnClick_MapButton);
            _hideInfoPanelButton.onClick.AddListener(OnClick_HideInfoPanelButton);
            _hikeAcceptButton.onClick.AddListener(OnClick_HikeAcceptButton);
            _closeHikePanelButton.onClick.AddListener(OnClick_CloseHikePanelButton);
            _hikePanelButton.onClick.AddListener(OnClick_HikePanelButton);
            _charactersPanelNextButton.onClick.AddListener(()=> OnClick_CharactersPanelNavigationButton(CHARACTERS_PANEL_SCROLLBAR_STEP));
            _charactersPanelPreviousButton.onClick.AddListener(() => OnClick_CharactersPanelNavigationButton(-CHARACTERS_PANEL_SCROLLBAR_STEP));
            _closeInventoryButton.onClick.AddListener(OnClick_CloseInventoryButton);
            _perkTreeButton.onClick.AddListener(OnClick_PerkTreeButton);
            _shopButton.onClick.AddListener(OnClick_OpenTradePanelButton);
            _closeTradePanelButton.onClick.AddListener(OnClick_CloseTradePanelButton);
            _sellButton.onClick.AddListener(OnClick_SellItemButton);
            _buyBackButton.onClick.AddListener(OnClick_BuyBackItemButton);

            _charactersUIBehaviours = new List<HubMapUICharacterBehaviour>();
            for (int i = 0; i < Data.HubMapData.Characters.Count; i++)
            {
                InitializeCharacterUI(Data.HubMapData.Characters[i]);
            }

            _equipmentSlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            for (int i = 0; i < Data.HubMapData.CharactersEquipmentSlotsAmount; i++)
            {
                InitializeEquipmentSlotUI(i);
            }

            _inventorySlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            for (int i = 0; i < Data.HubMapData.InventorySlotsAmount; i++)
            {
                InitializeInventorySlotUI(i);
            }

            _buyBackSlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            for (int i = 0; i < Data.HubMapData.BuyBackStorageSlotsAmount; i++)
            {
                InitializeBuyBackSlotUI(i);
            }

            _shopSlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            //todo: initialize shop slots

            _selected = new SelectedElements();
            _selected.OnChanged_InventorySlotIndex = OnChangedSelectedInventorySlot;
            _selected.OnChanged_EquipmentSlotIndex = OnChangedSelectedEquipmentSlot;
            _selected.OnChanged_BuyBackSlotIndex = OnChangedSelectedBuyBackSlot;
            _selected.OnChanged_ShopSlotIndex = OnChangedSelectedShopSlot;
            _selected.OnChanged_Character = OnChangedSelectedCharacter;
        }

        private void OnDisable()
        {
            _cityButton.onClick.RemoveAllListeners();
            _locationButton_1.onClick.RemoveAllListeners();
            _locationButton_2.onClick.RemoveAllListeners();
            _locationButton_3.onClick.RemoveAllListeners();
            _locationButton_4.onClick.RemoveAllListeners();

            _hubButton.onClick.RemoveAllListeners();
            _mapButton.onClick.RemoveAllListeners();
            _hideInfoPanelButton.onClick.RemoveAllListeners();
            _hikeAcceptButton.onClick.RemoveAllListeners();
            _closeHikePanelButton.onClick.RemoveAllListeners();
            _hikePanelButton.onClick.RemoveAllListeners();
            _charactersPanelNextButton.onClick.RemoveAllListeners();
            _charactersPanelPreviousButton.onClick.RemoveAllListeners();
            _closeInventoryButton.onClick.RemoveAllListeners();
            _perkTreeButton.onClick.RemoveAllListeners();
            _shopButton.onClick.RemoveAllListeners();

            for (int i = 0; i < _charactersUIBehaviours.Count; i++)
            {
                _charactersUIBehaviours[i].OnClick_CharacterButtonHandler = null;
            }

            for (int i = 0; i < _equipmentSlotsUIBehaviours.Count; i++)
            {
                _equipmentSlotsUIBehaviours[i].RemoveAllListeners();
            }

            for (int i = 0; i < _inventorySlotsUIBehaviours.Count; i++)
            {
                _inventorySlotsUIBehaviours[i].RemoveAllListeners();
            }

            for (int i = 0; i < _buyBackSlotsUIBehaviours.Count; i++)
            {
                _buyBackSlotsUIBehaviours[i].RemoveAllListeners();
            }

            for (int i = 0; i < _shopSlotsUIBehaviours.Count; i++)
            {
                _shopSlotsUIBehaviours[i].RemoveAllListeners();
            }

            _selected.RemoveAllListeners();
        }

        private void Start()
        {
            _buyBackStorage = new HubMapUIStorage(Data.HubMapData.BuyBackStorageSlotsAmount);
            _buyBackStorage.OnChangeItemHandler = (slotIndex, sprite) =>
            {
                FillSlotUI(slotIndex, sprite, StorageType.BuyBackStorage);
                _buyBackSlotsUIBehaviours[slotIndex].SetInteractable(sprite != null);
            };

            _inventory = new HubMapUIStorage(Data.HubMapData.InventorySlotsAmount);
            for (int i = 0; i < Data.HubMapData.StartInventoryItems.Length; i++)
            {
                _inventory.PutItem(i, Data.HubMapData.StartInventoryItems[i]);
            }
            FillItemsSlots(_inventory.GetItemsOnly(), StorageType.Inventory);
            _inventory.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Inventory);

            _displayedCurrentCitizensUIBehaviours = new Dictionary<HubMapUICitizen, HubMapUICitizenBehaviour>();
            _rightInfoPanelObjectsForDestroy = new List<GameObject>();
            _displayedDialogAnswerButtons = new List<GameObject>();

            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
            _hikePreparePanel.SetActive(true);
            _hikeInventoryPanel.SetActive(false);
            _tradePanel.SetActive(false);
        }

        #endregion


        #region Methods


        #region EventMethods(OnClick, OnDrag etc.)

        private void OnClick_MapButton()
        {
            _mainPanel.SetActive(true);
        }

        private void OnClick_HikePanelButton()
        {
            _hikePanel.SetActive(true);
        }

        private void OnClick_HubButton()
        {
            _mainPanel.SetActive(false);
        }

        private void OnClick_HideInfoPanelButton()
        {
            HideRightInfoPanels();
        }

        private void OnClick_HikeAcceptButton()
        {
            LocationLoad();
        }

        private void OnClick_CloseHikePanelButton()
        {
            _selected.Character = null;
            _selected.EquipmentSlotIndex = null;
            _selected.InventorySlotIndex = null;

            _hikeInventoryPanel.SetActive(false);
            _hikePanel.SetActive(false);
        }

        private void OnClick_CityButton(HubMapUICity city)
        {
            _selectedCity = city;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillCityPanel(city);

            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _cityInfoPanel.SetActive(true);
        }

        private void OnClick_OpenTradePanelButton()
        {
            SetScrollViewParentForInventoryItemsPanel(_shopInventoryScrollView);
            _shopCityReputation.text = Data.HubMapData.ReputationController.GetReputation(_selectedCity).ToString();
            _tradePanel.SetActive(true);
        }

        private void OnClick_CloseTradePanelButton()
        {
            _tradePanel.SetActive(false);
            _buyBackStorage.Clear();

            _selected.InventorySlotIndex = null;
            _selected.ShopSlotIndex = null;
            _selected.BuyBackSlotIndex = null;

            _sellingItemPrice.text = "";
            _buyingItemPrice.text = "";
            _buybackItemPrice.text = "";
        }

        private void OnClick_LocationButton(HubMapUILocation location)
        {
            _selectedLocation = location;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationPanel(location);

            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _locationInfoPanel.SetActive(true);
        }

        private void OnClick_CitizenButton(HubMapUICitizen citizen)
        {
            FillDialogPanel(citizen);
            _dialogPanel.SetActive(true);
        }

        private void OnClick_CharactersPanelNavigationButton(float step)
        {
            _charactersPanelScrollbar.value += step;
        }

        private void OnClick_CloseInventoryButton()
        {
            _hikeInventoryPanel.SetActive(false);
            _selected.InventorySlotIndex = null;
        }

        private void OnClick_PerkTreeButton()
        {
            //todo: show perk tree UI
            Debug.Log("OnClick_PerkTreeButton");
        }

        private void OnClick_CharacterButton(HubMapUICharacter character)
        {
            _selected.Character = character;
        }

        private void OnClick_InventorySlot(int slotIndex)
        {
            _selected.InventorySlotIndex = slotIndex;
        }

        private void OnClick_EquipmentSlot(int slotIndex)
        {
            _selected.EquipmentSlotIndex = slotIndex;

            if (!_hikeInventoryPanel.activeSelf)
            {
                SetScrollViewParentForInventoryItemsPanel(_hikeInventoryScrollView);
                _hikeInventoryPanel.SetActive(true);
            }
        }

        private void OnClick_BuyBackSlot(int slotIndex)
        {
            _selected.BuyBackSlotIndex = slotIndex;
        }

        private void OnDoubleClick_InventorySlot(int slotIndex)
        {
            if (!_tradePanel.activeSelf)
            {
                BaseItem inventoryItem = _inventory.TakeItem(slotIndex);

                if (_selected.EquipmentSlotIndex.HasValue)
                {
                    BaseItem equipmentItem = _selected.Character.Backpack.TakeItem(_selected.EquipmentSlotIndex.Value);

                    _inventory.PutItem(slotIndex, equipmentItem);
                    _selected.Character.Backpack.PutItem(_selected.EquipmentSlotIndex.Value, inventoryItem);
                }
                else
                {
                    if (!_selected.Character.Backpack.MovingItemToFirstEmptySlot(inventoryItem))
                    {
                        _inventory.PutItem(slotIndex, inventoryItem);
                        Debug.Log("Equipment is full");
                    }
                }
            }
        }

        private void OnDoubleClick_EquipmentSlot(int slotIndex)
        {
            BaseItem equipmentItem = _selected.Character.Backpack.TakeItem(slotIndex);

            if (_hikeInventoryPanel.activeSelf)
            {
                if (_selected.InventorySlotIndex.HasValue)
                {
                    BaseItem inventoryItem = _inventory.TakeItem(_selected.InventorySlotIndex.Value);

                    _inventory.PutItem(_selected.InventorySlotIndex.Value, equipmentItem);
                    _selected.Character.Backpack.PutItem(slotIndex, inventoryItem);
                }
                else
                {
                    bool isFoundEmptySlot = false;

                    for (int i = 0; i < _inventory.GetSlotsCount(); i++)
                    {
                        if (_inventory.GetItemBySlot(i) == null)
                        {
                            _inventory.PutItem(i, equipmentItem);
                            isFoundEmptySlot = true;
                            break;
                        }
                    }

                    if (!isFoundEmptySlot)
                    {
                        _selected.Character.Backpack.PutItem(slotIndex, equipmentItem);
                        Debug.Log("Inventory is full");
                    }
                }
            }
        }

        private void OnClick_DialogButton(HubMapUICitizen citizen, HubMapUIDialogAnswer dialogAnswer)
        {
            for (int i = 0; i < _displayedDialogAnswerButtons.Count; i++)
            {
                Destroy(_displayedDialogAnswerButtons[i]);
            }
            _displayedDialogAnswerButtons.Clear();

            Data.HubMapData.DialogsController.SetNewDialogId(citizen, dialogAnswer.NextDialogNodeId);

            if (dialogAnswer.IsProgressQuest)
            {
                Data.HubMapData.QuestService.QuestProgressed(dialogAnswer.ProgressQuest, citizen);
            }

            if (dialogAnswer.IsDialogEnd)
            {
                _dialogPanel.SetActive(false);
                foreach (KeyValuePair<HubMapUICitizen, HubMapUICitizenBehaviour> kvp in _displayedCurrentCitizensUIBehaviours)
                {
                    kvp.Value.UpdateInfo(kvp.Key);
                }
            }
            else
            {
                FillDialogPanel(citizen);
            }
        }

        private void OnClick_SellItemButton()
        {
            if (_selected.InventorySlotIndex.HasValue)
            {
                BaseItem sellingItem = _inventory.TakeItem(_selected.InventorySlotIndex.Value);

                if (sellingItem != null)
                {
                    if (!_buyBackStorage.MovingItemToFirstEmptySlot(sellingItem))
                    {
                        _inventory.PutItem(_selected.InventorySlotIndex.Value, sellingItem);
                        Debug.Log("BuyBack storage is full");
                    }
                }
            }
        }

        private void OnClick_BuyBackItemButton()
        {
            if (_selected.BuyBackSlotIndex.HasValue)
            {
                BaseItem buyingItem = _buyBackStorage.TakeItem(_selected.BuyBackSlotIndex.Value);

                if (buyingItem != null)
                {
                    if (!_inventory.MovingItemToFirstEmptySlot(buyingItem))
                    {
                        _buyBackStorage.PutItem(_selected.InventorySlotIndex.Value, buyingItem);
                        Debug.Log("Inventory storage is full");
                    }
                }

                _selected.BuyBackSlotIndex = null;
            }
        }

        private void OnDragItemFromSlot(int slotIndex, StorageType storageType)
        {
            _draggedItemInfo.slotIndex = slotIndex;
            _draggedItemInfo.storageType = storageType;
        }

        private void OnEndDragItem(int slotIndex, StorageType storageType)
        {
            FillSlotUI(slotIndex, GetStorageByType(storageType).GetItemIconBySlot(slotIndex), storageType);
            _draggedItemInfo.slotIndex = null;
        }

        private void OnDropItemToSlot(int dropSlotIndex, StorageType dropStorageType)
        {
            if (_draggedItemInfo.slotIndex.HasValue)
            {
                HubMapUIStorage dropItemStorage = GetStorageByType(dropStorageType); ;
                HubMapUIStorage takeItemStorage = GetStorageByType(_draggedItemInfo.storageType);

                BaseItem draggedItem = takeItemStorage.TakeItem(_draggedItemInfo.slotIndex.Value);
                BaseItem droppedSlotItem = dropItemStorage.TakeItem(dropSlotIndex);

                dropItemStorage.PutItem(dropSlotIndex, draggedItem);
                takeItemStorage.PutItem(_draggedItemInfo.slotIndex.Value, droppedSlotItem);
            }
        }

        private void OnChangedSelectedInventorySlot(int? previousSlotIndex)
        {
            if (previousSlotIndex.HasValue)
            {
                _inventorySlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.InventorySlotIndex.HasValue)
            {
                _inventorySlotsUIBehaviours[_selected.InventorySlotIndex.Value].SelectFrameSwitcher(true);

                if (_inventory.GetItemBySlot(_selected.InventorySlotIndex.Value) != null)
                {
                    _sellingItemPrice.text = _inventory.GetItemBySlot(_selected.InventorySlotIndex.Value).ItemStruct.ShopPrice.ToString();
                }
                else
                {
                    _sellingItemPrice.text = "0";
                }
            }
            else
            {
                _sellingItemPrice.text = "";
            }
        }

        private void OnChangedSelectedEquipmentSlot(int? previousSlotIndex)
        {
            if (previousSlotIndex.HasValue)
            {
                _equipmentSlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.EquipmentSlotIndex.HasValue)
            {
                _equipmentSlotsUIBehaviours[_selected.EquipmentSlotIndex.Value].SelectFrameSwitcher(true);
            }
        }

        private void OnChangedSelectedBuyBackSlot(int? previousSlotIndex)
        {
            if (previousSlotIndex.HasValue)
            {
                _buyBackSlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.BuyBackSlotIndex.HasValue)
            {
                _buyBackSlotsUIBehaviours[_selected.BuyBackSlotIndex.Value].SelectFrameSwitcher(true);

                if (_buyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value) != null)
                {
                    _buybackItemPrice.text = _buyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value).ItemStruct.ShopPrice.ToString();
                }
                else
                {
                    _buybackItemPrice.text = "0";
                }
            }
            else
            {
                _buybackItemPrice.text = "";
            }
        }

        private void OnChangedSelectedShopSlot(int? previousSlotIndex)
        {
            if (previousSlotIndex.HasValue)
            {
                _shopSlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.ShopSlotIndex.HasValue)
            {
                _shopSlotsUIBehaviours[_selected.ShopSlotIndex.Value].SelectFrameSwitcher(true);
            }
        }

        private void OnChangedSelectedCharacter(HubMapUICharacter previousCharacter)
        {
            if (previousCharacter != null)
            {
                previousCharacter.Backpack.OnChangeItemHandler = null;
            }

            if (_selected.Character != null)
            {
                FillItemsSlots(_selected.Character.Backpack.GetAll(), StorageType.Equipment);
                SetEquipmentSlotsInteractable(true);
                _selected.Character.Backpack.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Equipment);
            }
            else
            {
                SetEquipmentSlotsInteractable(false);
            }
        }

        #endregion


        #region InitializeUIElements

        private void InitializeCharacterUI(HubMapUICharacter character)
        {
            character.SetStartEquipment();
            GameObject characterUI = GameObject.Instantiate(Data.HubMapData.CharacterUIPrefab);
            characterUI.transform.SetParent(_charactersPanel.transform, false);
            characterUI.transform.localScale = new Vector3(1, 1, 1);

            HubMapUICharacterBehaviour characterUIBehaviour = characterUI.GetComponentInChildren<HubMapUICharacterBehaviour>();
            characterUIBehaviour.FillCharacterInfo(character);
            characterUIBehaviour.OnClick_CharacterButtonHandler = OnClick_CharacterButton;
            _charactersUIBehaviours.Add(characterUIBehaviour);
        }

        private void InitializeEquipmentSlotUI(int slotIndex)
        {
            GameObject equipSlotUI = GameObject.Instantiate(Data.HubMapData.EquipmentSlotUIPrefab);
            equipSlotUI.transform.SetParent(_equipmentPanel.transform, false);
            equipSlotUI.transform.localScale = new Vector3(1, 1, 1);

            HubMapUISlotBehaviour slotBehaviour = equipSlotUI.GetComponent<HubMapUISlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnClick_SlotButtonHandler = OnClick_EquipmentSlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_EquipmentSlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageType.Equipment);
            _equipmentSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeInventorySlotUI(int slotIndex)
        {
            GameObject inventorySlotUI = GameObject.Instantiate(Data.HubMapData.InventorySlotUIPrefab);
            inventorySlotUI.transform.SetParent(_inventoryItemsPanel.transform, false);
            inventorySlotUI.transform.localScale = new Vector3(1, 1, 1);

            HubMapUISlotBehaviour slotBehaviour = inventorySlotUI.GetComponent<HubMapUISlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex);
            slotBehaviour.OnClick_SlotButtonHandler = OnClick_InventorySlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_InventorySlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageType.Inventory);
            _inventorySlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeBuyBackSlotUI(int slotIndex)
        {
            GameObject buyBackSlotUI = GameObject.Instantiate(Data.HubMapData.ShopSlotUIPrefab);
            buyBackSlotUI.transform.SetParent(_buyBackItemsPanel.transform, false);
            buyBackSlotUI.transform.localScale = new Vector3(1, 1, 1);

            HubMapUISlotBehaviour slotBehaviour = buyBackSlotUI.GetComponent<HubMapUISlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnClick_SlotButtonHandler = OnClick_BuyBackSlot;
            _buyBackSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeCitizenUI(HubMapUICitizen citizen)
        {
            GameObject citizenUI = GameObject.Instantiate(Data.HubMapData.CitizenUIPrefab);
            citizenUI.transform.SetParent(_citizenPanel.transform, false);
            citizenUI.transform.localScale = new Vector3(1, 1, 1);

            HubMapUICitizenBehaviour citizenUIBehaviour = citizenUI.GetComponentInChildren<HubMapUICitizenBehaviour>();
            citizenUIBehaviour.FillCitizenInfo(citizen);
            citizenUIBehaviour.OnClick_CitizenButtonHandler = OnClick_CitizenButton;
            Data.HubMapData.QuestService.OnQuestIsActiveHandler += () => citizenUIBehaviour.UpdateInfo(citizen);

            _rightInfoPanelObjectsForDestroy.Add(citizenUI);
            _displayedCurrentCitizensUIBehaviours.Add(citizen, citizenUIBehaviour);
        }

        private void InitializeDialogAnswerButton(HubMapUICitizen citizen, HubMapUIDialogAnswer answer)
        {
            GameObject answerButton = GameObject.Instantiate(Data.HubMapData.AnswerButtonUIPrefab);
            answerButton.transform.SetParent(_answerButtonsPanel.transform, false);
            answerButton.transform.localScale = new Vector3(1, 1, 1);
            answerButton.GetComponentInChildren<Text>().text = answer.Text;

            if (answer.IsDialogEnd)
            {
                answerButton.GetComponentInChildren<Text>().text += " (����)";
            }

            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnClick_DialogButton(citizen, answer));
            _displayedDialogAnswerButtons.Add(answerButton);
        }

        #endregion


        #region FillUIElementsByInfo

        private void FillSlotUI(int slotIndex, Sprite sprite, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Equipment:
                    _equipmentSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    break;

                case StorageType.Inventory:
                    _inventorySlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    break;

                case StorageType.BuyBackStorage:
                    _buyBackSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    break;

                case StorageType.Shop:
                    _shopSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    break;

                default:
                    Debug.LogError(this + ": incorrect StorageSlotType");
                    break;
            }
        }

         private void FillItemsSlots(IEnumerable<BaseItem> items, StorageType storageType)
        {
            int i = 0;
            foreach (BaseItem item in items)
            {
                if (item != null)
                {
                    FillSlotUI(i, item.ItemStruct.Icon, storageType);
                }
                else
                {
                    FillSlotUI(i, null, storageType);
                }
                i++;
                if (i >= GetStorageByType(storageType).GetSlotsCount())
                {
                    break;
                }
            }
        }

        private void FillCityPanel(HubMapUICity city)
        {
            _cityFraction.sprite = city.Fraction;
            _cityName.text = city.Name;
            _cityDescription.text = city.Description;

            float cityReputation = Data.HubMapData.ReputationController.GetReputation(city);
            _cityReputation.text = cityReputation.ToString();

            for (int i = 0; i < city.Citizens.Length; i++)
            {
                InitializeCitizenUI(city.Citizens[i]);
            }
        }

        private void FillLocationPanel(HubMapUILocation location)
        {
            _locationScreen.sprite = location.Screenshot;
            _locationName.text = location.Name;
            _locationDescription.text = location.Description;

            for (int i = 0; i < location.Dwellers.Length; i++)
            {
                GameObject dwellerUI = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _rightInfoPanelObjectsForDestroy.Add(dwellerUI);
                dwellerUI.transform.SetParent(_dwellersPanel.transform, false);
                dwellerUI.transform.localScale = new Vector3(1, 1, 1);
                dwellerUI.GetComponentInChildren<Text>().text = location.Dwellers[i].Name;
            }

            for (int i = 0; i < location.Ingredients.Length; i++)
            {
                GameObject ingredientUI = GameObject.Instantiate(Data.HubMapData.LocationTextUIPrefab);
                _rightInfoPanelObjectsForDestroy.Add(ingredientUI);
                ingredientUI.transform.SetParent(_ingredientsPanel.transform, false);
                ingredientUI.transform.localScale = new Vector3(1, 1, 1);
                ingredientUI.GetComponentInChildren<Text>().text = location.Ingredients[i].Name;
            }
        }

        private void FillDialogPanel(HubMapUICitizen citizen)
        {
            HubMapUIDialogNode currentDialog = Data.HubMapData.Dialogs.Find(dialog => dialog.Id == Data.HubMapData.DialogsController.GetCurrentDialogId(citizen));
            _citizenName.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _dialogText.text = currentDialog.Text;

            for (int i = 0; i < currentDialog.Answers.Length; i++)
            {
                InitializeDialogAnswerButton(citizen, currentDialog.Answers[i]);
            }

            HubMapUIDialogAnswer additionalQuestAnswer = Data.HubMapData.QuestService.GetAdditionalQuestAnswer(currentDialog.Id);
            if (additionalQuestAnswer != null)
            {
                InitializeDialogAnswerButton(citizen, additionalQuestAnswer);
            }
        }

        #endregion


        private void HideRightInfoPanels()
        {
            _cityInfoPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }

        private void ClearRightInfoPanel()
        {
            foreach (KeyValuePair<HubMapUICitizen, HubMapUICitizenBehaviour> kvp in _displayedCurrentCitizensUIBehaviours)
            {
                Data.HubMapData.QuestService.OnQuestIsActiveHandler -=
                    () => kvp.Value.UpdateInfo(kvp.Key);
            }

            for (int i=0; i< _rightInfoPanelObjectsForDestroy.Count; i++)
            {
                Destroy(_rightInfoPanelObjectsForDestroy[i]);
            }

            _rightInfoPanelObjectsForDestroy.Clear();
            _displayedCurrentCitizensUIBehaviours.Clear();
        }

        private void SetEquipmentSlotsInteractable(bool flag)
        {
            for (int i = 0; i < _equipmentSlotsUIBehaviours.Count; i++)
            {
                if (!flag)
                { 
                    _equipmentSlotsUIBehaviours[i].FillSlot(null);
                }
                _equipmentSlotsUIBehaviours[i].SetInteractable(flag);
            }
        }

        private HubMapUIStorage GetStorageByType(StorageType storageType)
        {
            HubMapUIStorage storage = null;
            switch (storageType)
            {
                case StorageType.Equipment:
                    storage = _selected.Character.Backpack;
                    break;

                case StorageType.Inventory:
                    storage = _inventory;
                    break;

                case StorageType.BuyBackStorage:
                    storage = _buyBackStorage;
                    break;

                default:
                    Debug.LogError(this + ": incorrect StorageSlotType");
                    break;
            }
            return storage;
        }

        private void SetScrollViewParentForInventoryItemsPanel(GameObject parentPanel)
        {
            _inventoryItemsPanel.transform.SetParent(parentPanel.transform.Find("Viewport"), false);
            parentPanel.GetComponent<ScrollRect>().content = _inventoryItemsPanel.GetComponent<RectTransform>();
        }

        private void LocationLoad()
        {
            Debug.Log("Load location. Location: " + _selectedLocation);
        }

        #endregion

    }
}
