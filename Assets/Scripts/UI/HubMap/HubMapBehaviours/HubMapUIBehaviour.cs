using System;
using System.Collections.Generic;
using System.Text;
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
            private HubMapUICharacterModel _character;

            public Action<int?> OnChanged_EquipmentSlotIndex { get; set; }
            public Action<int?> OnChanged_InventorySlotIndex { get; set; }
            public Action<int?> OnChanged_ShopSlotIndex { get; set; }
            public Action<int?> OnChanged_BuyBackSlotIndex { get; set; }
            public Action<HubMapUICharacterModel> OnChanged_Character { get; set; }

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

            public HubMapUICharacterModel Character
            {
                get
                {
                    return _character;
                }
                set
                {
                    if (value != _character)
                    {
                        HubMapUICharacterModel previousValue = _character;
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
        [SerializeField] private GameObject _tooltip;

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
        [SerializeField] private Text _playerGoldAmount;


        private List<GameObject> _rightInfoPanelObjectsForDestroy;
        private List<HubMapUISlotBehaviour> _equipmentSlotsUIBehaviours;
        private List<HubMapUISlotBehaviour> _inventorySlotsUIBehaviours;
        private List<HubMapUIShopSlotBehaviour> _shopSlotsUIBehaviours;
        private List<HubMapUISlotBehaviour> _buyBackSlotsUIBehaviours;
        private Dictionary<HubMapUICitizenModel, HubMapUICitizenBehaviour> _displayedCurrentCitizensUIBehaviours;
        private List<GameObject> _displayedDialogAnswerButtons;
        private (int? slotIndex, StorageType storageType) _draggedItemInfo;

        private HubMapUILocationData _selectedLocation;
        private HubMapUICityModel _selectedCity;
        private HubMapUIStorage _currentBuyBackStorage;
        private HubMapUIStorage _currentShopStorage;
        private SelectedElements _selected;

        private HubMapUIContext _context;
        private HubMapUIPlayerModel _player;
        private HubMapUIStorage _inventory;

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
            _buyButton.onClick.AddListener(OnClick_BuyItemButton);
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
        }

        private void Awake()
        {
            _context = Data.HubMapData.Context;
            _player = _context.Player;
            _inventory = _player.Inventory;

            _rightInfoPanelObjectsForDestroy = new List<GameObject>();
            _displayedCurrentCitizensUIBehaviours = new Dictionary<HubMapUICitizenModel, HubMapUICitizenBehaviour>();
            _displayedDialogAnswerButtons = new List<GameObject>();

            _equipmentSlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            _inventorySlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            _buyBackSlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            _shopSlotsUIBehaviours = new List<HubMapUIShopSlotBehaviour>();

            _selected = new SelectedElements();
        }

        private void Start()
        {
            for (int i = 0; i < _context.Characters.Count; i++)
            {
                InitializeCharacterUI(_context.Characters[i]);
            }

            for (int i = 0; i < _context.CharactersEquipmentSlotAmount; i++)
            {
                InitializeEquipmentSlotUI(i);
            }

            for (int i = 0; i < _inventory.GetSlotsCount(); i++)
            {
                InitializeInventorySlotUI(i);
            }

            for (int i = 0; i < _context.ShopsSlotsAmount; i++)
            {
                InitializeBuyBackSlotUI(i);
            }

            for (int i = 0; i < _context.ShopsSlotsAmount; i++)
            {
                InitializeShopSlotUI(i);
            }

            FillItemsSlots(StorageType.Inventory);
            _inventory.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Inventory);

            _selected.OnChanged_InventorySlotIndex = OnChangedSelectedInventorySlot;
            _selected.OnChanged_EquipmentSlotIndex = OnChangedSelectedEquipmentSlot;
            _selected.OnChanged_BuyBackSlotIndex = OnChangedSelectedBuyBackSlot;
            _selected.OnChanged_ShopSlotIndex = OnChangedSelectedShopSlot;
            _selected.OnChanged_Character = OnChangedSelectedCharacter;

            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
            _hikePreparePanel.SetActive(true);
            _hikeInventoryPanel.SetActive(false);
            _tradePanel.SetActive(false);
            _sellButton.interactable = false;
            _buyBackButton.interactable = false;
            _buyButton.interactable = false;
            _tooltip.SetActive(false);
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

        private void OnClick_CityButton(HubMapUICityModel city)
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
            _playerGoldAmount.text = _player.GoldAmount.ToString();
            _shopCityReputation.text = _selectedCity.PlayerReputation.ToString();

            _currentShopStorage = _selectedCity.ShopStorage;
            FillItemsSlots(StorageType.Shop);
            _currentShopStorage.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Shop);

            _currentBuyBackStorage = _selectedCity.BuyBackStorage;
            FillItemsSlots(StorageType.BuyBackStorage);
            _currentBuyBackStorage.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.BuyBackStorage);

            _tradePanel.SetActive(true);
        }

        private void OnClick_CloseTradePanelButton()
        {
            _selected.InventorySlotIndex = null;
            _selected.ShopSlotIndex = null;
            _selected.BuyBackSlotIndex = null;

            _tradePanel.SetActive(false);

            _currentBuyBackStorage.Clear();
            _currentBuyBackStorage.OnChangeItemHandler -= (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.BuyBackStorage);
            _currentBuyBackStorage = null;

            _currentShopStorage.OnChangeItemHandler -= (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Shop);
            _currentShopStorage = null;
        }

        private void OnClick_LocationButton(HubMapUILocationData location)
        {
            _selectedLocation = location;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationPanel(location);

            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _locationInfoPanel.SetActive(true);
        }

        private void OnClick_CitizenButton(HubMapUICitizenModel citizen)
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

        private void OnClick_CharacterButton(HubMapUICharacterModel character)
        {
            _selected.Character = character;
        }

        private void OnPointerDown_InventorySlot(int slotIndex)
        {
            _selected.InventorySlotIndex = slotIndex;
        }

        private void OnPointerDown_EquipmentSlot(int slotIndex)
        {
            _selected.EquipmentSlotIndex = slotIndex;

            if (!_hikeInventoryPanel.activeSelf)
            {
                SetScrollViewParentForInventoryItemsPanel(_hikeInventoryScrollView);
                _hikeInventoryPanel.SetActive(true);
            }
        }

        private void OnPointerDown_BuyBackSlot(int slotIndex)
        {
            _selected.BuyBackSlotIndex = slotIndex;
        }

        private void OnPointerDown_ShopSlot(int slotIndex)
        {
            _selected.ShopSlotIndex = slotIndex;
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

        private void OnClick_DialogButton(HubMapUICitizenModel citizen, HubMapUIDialogAnswer dialogAnswer)
        {
            dialogAnswer.SelectedByPlayer();

            for (int i = 0; i < _displayedDialogAnswerButtons.Count; i++)
            {
                Destroy(_displayedDialogAnswerButtons[i]);
            }
            _displayedDialogAnswerButtons.Clear();

            if (dialogAnswer.IsDialogEnd)
            {
                _dialogPanel.SetActive(false);
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
                    if (_currentBuyBackStorage.MovingItemToFirstEmptySlot(sellingItem))
                    {
                        ChangePlayerGoldAmount(Data.HubMapData.ShopService.CountSellPrice(sellingItem));
                    }
                    else
                    {
                        _inventory.PutItem(_selected.InventorySlotIndex.Value, sellingItem);
                        Debug.Log("BuyBack storage is full");
                    }
                }

                _selected.InventorySlotIndex = null;
            }
        }

        private void OnClick_BuyBackItemButton()
        {
            if (_selected.BuyBackSlotIndex.HasValue)
            {
                BaseItem buyingItem = _currentBuyBackStorage.TakeItem(_selected.BuyBackSlotIndex.Value);
                if (buyingItem != null)
                {
                    if (_player.GoldAmount >= Data.HubMapData.ShopService.CountSellPrice(buyingItem)) 
                    { 
                        if (_inventory.MovingItemToFirstEmptySlot(buyingItem))
                        {
                            ChangePlayerGoldAmount(-Data.HubMapData.ShopService.CountSellPrice(buyingItem));
                        }
                        else
                        {
                            _currentBuyBackStorage.PutItem(_selected.BuyBackSlotIndex.Value, buyingItem);
                            Debug.Log("Inventory storage is full");
                        }
                    }
                    else
                    {
                        Debug.Log("The player does not have enough gold ");
                    }
                }

                _selected.BuyBackSlotIndex = null;
            }
        }

        private void OnClick_BuyItemButton()
        {
            if (_selected.ShopSlotIndex.HasValue)
            {
                BaseItem buyingItem = _currentShopStorage.TakeItem(_selected.ShopSlotIndex.Value);
                if (buyingItem != null)
                {
                    if (IsPossibleToBuyShopItem(buyingItem))
                    {
                        if (_inventory.MovingItemToFirstEmptySlot(buyingItem))
                        {
                            ChangePlayerGoldAmount(-Data.HubMapData.ShopService.GetItemPrice(buyingItem));
                        }
                        else
                        {
                            _currentShopStorage.PutItem(_selected.ShopSlotIndex.Value, buyingItem);
                            Debug.Log("Inventory storage is full");
                        }
                    }
                    else
                    {
                        Debug.Log("The player does not have enough gold ");
                    }
                }

                _selected.ShopSlotIndex = null;
            }
        }

        private void OnPointerEnter_Slot(int slotIndex, StorageType storageType)
        {
            FillTooltipByItemInfo(GetStorageByType(storageType).GetItemBySlot(slotIndex), storageType);
            _tooltip.transform.position = Input.mousePosition;
            _tooltip.SetActive(true);
        }


        private void OnPointerExit_Slot(int slotIndex)
        {
            _tooltip.SetActive(false);
        }

         private void OnDragItemFromSlot(int slotIndex, StorageType storageType)
        {
            _draggedItemInfo.slotIndex = slotIndex;
            _draggedItemInfo.storageType = storageType;
        }

        private void OnEndDragItem(int slotIndex, StorageType storageType)
        {
            FillSlotUI(slotIndex, GetStorageByType(storageType).GetItemBySlot(slotIndex), storageType);
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


                switch (dropStorageType)
                {
                    case StorageType.Equipment:
                        _selected.EquipmentSlotIndex = dropSlotIndex;
                        break;

                    case StorageType.Inventory:
                        _selected.InventorySlotIndex = dropSlotIndex;
                        break;

                    case StorageType.BuyBackStorage:
                        _selected.BuyBackSlotIndex = dropSlotIndex;
                        break;

                    case StorageType.Shop:
                        _selected.ShopSlotIndex = dropSlotIndex;
                        break;

                    default:
                        Debug.LogError("Incorrect storage type");
                        break;
                }
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

                if (_tradePanel.activeSelf)
                {
                    if (_inventory.GetItemBySlot(_selected.InventorySlotIndex.Value) != null)
                    {
                        _sellingItemPrice.text = Data.HubMapData.ShopService.
                            CountSellPrice(_inventory.GetItemBySlot(_selected.InventorySlotIndex.Value)).ToString();
                        _sellButton.interactable = true;
                    }
                    else
                    {
                        _sellButton.interactable = false;
                        _sellingItemPrice.text = "0";
                    }
                }
            }
            else
            {
                if (_tradePanel.activeSelf)
                {
                    _sellButton.interactable = false;
                    _sellingItemPrice.text = "";
                }
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

                if (_tradePanel.activeSelf)
                {
                    if (_currentBuyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value) != null)
                    {
                        _buybackItemPrice.text = Data.HubMapData.ShopService.
                            CountSellPrice(_currentBuyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value)).ToString();

                        _buyBackButton.interactable = _player.GoldAmount >= Data.HubMapData.ShopService.
                            CountSellPrice(_currentBuyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value));
                    }
                    else
                    {
                        _buyBackButton.interactable = false;
                        _buybackItemPrice.text = "0";
                    }
                }
            }
            else
            {
                if (_tradePanel.activeSelf)
                {
                    _buyBackButton.interactable = false;
                    _buybackItemPrice.text = "";
                }
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

                if (_tradePanel.activeSelf)
                {
                    BaseItem item = _currentShopStorage.GetItemBySlot(_selected.ShopSlotIndex.Value);
                    if (item != null)
                    {
                        _buyingItemPrice.text = Data.HubMapData.ShopService.GetItemPrice(item).ToString();
                        _buyButton.interactable = IsPossibleToBuyShopItem(item);
                    }
                    else
                    {
                        _buyButton.interactable = false;
                        _buyingItemPrice.text = "0";
                    }
                }
            }
            else
            {
                if (_tradePanel.activeSelf)
                {
                    _buyButton.interactable = false;
                    _buyingItemPrice.text = "";
                }
            }
        }

        private void OnChangedSelectedCharacter(HubMapUICharacterModel previousCharacter)
        {
            if (previousCharacter != null)
            {
                previousCharacter.Backpack.OnChangeItemHandler = null;
            }

            if (_selected.Character != null)
            {
                FillItemsSlots(StorageType.Equipment);
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

        private void InitializeCharacterUI(HubMapUICharacterModel character)
        {
            GameObject characterUI = InstantiateUIObject(Data.HubMapData.CharacterUIPrefab, _charactersPanel);
            characterUI.GetComponent<Image>().sprite = character.Portrait;
            characterUI.GetComponent<Button>().onClick.AddListener(() => OnClick_CharacterButton(character));
        }

        private void InitializeEquipmentSlotUI(int slotIndex)
        {
            GameObject equipSlotUI = InstantiateUIObject(Data.HubMapData.EquipmentSlotUIPrefab, _equipmentPanel);

            HubMapUISlotBehaviour slotBehaviour = equipSlotUI.GetComponent<HubMapUISlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, true);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_EquipmentSlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_EquipmentSlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageType.Equipment);
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.Equipment);

            _equipmentSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeInventorySlotUI(int slotIndex)
        {
            GameObject inventorySlotUI = InstantiateUIObject(Data.HubMapData.InventorySlotUIPrefab, _inventoryItemsPanel);

            HubMapUISlotBehaviour slotBehaviour = inventorySlotUI.GetComponent<HubMapUISlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, true);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_InventorySlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_InventorySlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageType.Inventory);
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _inventorySlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeBuyBackSlotUI(int slotIndex)
        {
            GameObject buyBackSlotUI = InstantiateUIObject(Data.HubMapData.ShopSlotUIPrefab, _buyBackItemsPanel);

            HubMapUISlotBehaviour slotBehaviour = buyBackSlotUI.GetComponent<HubMapUISlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, false);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_BuyBackSlot;
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.BuyBackStorage);

            _buyBackSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeShopSlotUI(int slotIndex)
        {
            GameObject shopSlotUI = InstantiateUIObject(Data.HubMapData.ShopSlotUIPrefab, _buyingItemsPanel);

            HubMapUIShopSlotBehaviour slotBehaviour = shopSlotUI.GetComponent<HubMapUIShopSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, false);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_ShopSlot;
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.Shop);

            _shopSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeCitizenUI(HubMapUICitizenModel citizen)
        {
            GameObject citizenUI = InstantiateUIObject(Data.HubMapData.CitizenUIPrefab, _citizenPanel);

            HubMapUICitizenBehaviour citizenUIBehaviour = citizenUI.GetComponentInChildren<HubMapUICitizenBehaviour>();
            citizenUIBehaviour.FillCitizenInfo(citizen);
            citizenUIBehaviour.OnClick_CitizenButtonHandler = OnClick_CitizenButton;

            citizen.OnChangeQuestMarkerTypeHandler += citizenUIBehaviour.SetQuestMarker;

            _rightInfoPanelObjectsForDestroy.Add(citizenUI);
            _displayedCurrentCitizensUIBehaviours.Add(citizen, citizenUIBehaviour);
        }

        private void InitializeDialogAnswerButton(HubMapUICitizenModel citizen, HubMapUIDialogAnswer answer)
        {
            GameObject answerButton = InstantiateUIObject(Data.HubMapData.AnswerButtonUIPrefab, _answerButtonsPanel);
            answerButton.GetComponentInChildren<Text>().text = answer.Text;

            if (answer.IsDialogEnd)
            {
                answerButton.GetComponentInChildren<Text>().text += " (уйти)";
            }

            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnClick_DialogButton(citizen, answer));
            _displayedDialogAnswerButtons.Add(answerButton);
        }

        #endregion


        #region FillUIElementsByInfo

        private void FillTooltipByItemInfo(BaseItem item, StorageType storageType)
        {
            Text[] tooltipTexts = _tooltip.GetComponentsInChildren<Text>(true);

            tooltipTexts[0].gameObject.SetActive(false);
            tooltipTexts[1].text = item.ItemStruct.Name;
            tooltipTexts[2].text = item.ItemStruct.Description;
            tooltipTexts[3].text = "Цена: " + Data.HubMapData.ShopService.GetItemPrice(item).ToString();

            if (storageType == StorageType.Shop)
            {
                if (!IsPossibleToBuyShopItem(item))
                {
                    tooltipTexts[0].gameObject.SetActive(true);
                    tooltipTexts[0].text = ImpossibleToBuyMessage(item);
                }
            }
        }

        private void FillSlotUI(int slotIndex, BaseItem item, StorageType storageType)
        {
            Sprite sprite = item ? item.ItemStruct.Icon : null;

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
                    _buyBackSlotsUIBehaviours[slotIndex].SetInteractable(sprite != null);

                    break;
                case StorageType.Shop:

                    _shopSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    _shopSlotsUIBehaviours[slotIndex].SetInteractable(sprite != null);
                    _shopSlotsUIBehaviours[slotIndex].SetAvailability(IsPossibleToBuyShopItem(item));

                    break;
                default:

                    Debug.LogError(this + ": incorrect StorageSlotType");

                    break;
            }
        }

         private void FillItemsSlots(StorageType storageType)
        {
            HubMapUIStorage storage = GetStorageByType(storageType);

            for (int i = 0; i < storage.GetSlotsCount(); i++)
            {
                FillSlotUI(i, storage.GetItemBySlot(i), storageType);
            }
        }

        private void FillCityPanel(HubMapUICityModel city)
        {
            _cityFraction.sprite = city.Fraction.Logo;
            _cityName.text = city.Name;
            _cityDescription.text = city.Description;
            _cityReputation.text = city.PlayerReputation.ToString();

            for (int i = 0; i < city.Citizens.Count; i++)
            {
                InitializeCitizenUI(city.Citizens[i]);
            }
        }

        private void FillLocationPanel(HubMapUILocationData location)
        {
            _locationScreen.sprite = location.Screenshot;
            _locationName.text = location.Name;
            _locationDescription.text = location.Description;

            for (int i = 0; i < location.Dwellers.Length; i++)
            {
                GameObject dwellerUI = InstantiateUIObject(Data.HubMapData.LocationTextUIPrefab, _dwellersPanel);
                _rightInfoPanelObjectsForDestroy.Add(dwellerUI);
                dwellerUI.GetComponentInChildren<Text>().text = location.Dwellers[i].Name;
            }

            for (int i = 0; i < location.Ingredients.Length; i++)
            {
                GameObject ingredientUI = InstantiateUIObject(Data.HubMapData.LocationTextUIPrefab, _ingredientsPanel);
                _rightInfoPanelObjectsForDestroy.Add(ingredientUI);
                ingredientUI.GetComponentInChildren<Text>().text = location.Ingredients[i].Name;
            }
        }

        private void FillDialogPanel(HubMapUICitizenModel citizen)
        {
            _citizenName.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _dialogText.text = citizen.CurrentDialog.Text;

            List<HubMapUIDialogAnswer> answers = citizen.GetAllCurentAnswers();
            for (int i = 0; i < answers.Count; i++)
            {
                InitializeDialogAnswerButton(citizen, answers[i]);
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
            foreach (KeyValuePair<HubMapUICitizenModel, HubMapUICitizenBehaviour> kvp in _displayedCurrentCitizensUIBehaviours)
            {
                kvp.Key.OnChangeQuestMarkerTypeHandler -= kvp.Value.SetQuestMarker;
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
                    storage = _currentBuyBackStorage;
                    break;

                case StorageType.Shop:
                    storage = _currentShopStorage;
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

        private GameObject InstantiateUIObject(GameObject prefab, GameObject parent)
        {
            GameObject objectUI = GameObject.Instantiate(prefab);
            objectUI.transform.SetParent(parent.transform, false);
            objectUI.transform.localScale = new Vector3(1, 1, 1);
            return objectUI;
        }

        private void ChangePlayerGoldAmount(int goldAmount)
        {
            _player.GoldAmount += goldAmount;
            _playerGoldAmount.text = _player.GoldAmount.ToString();

            for (int i = 0; i < _shopSlotsUIBehaviours.Count; i++)
            {
                _shopSlotsUIBehaviours[i].SetAvailability(IsPossibleToBuyShopItem(_currentShopStorage.GetItemBySlot(i)));
            }
        }

        private bool IsPossibleToBuyShopItem(BaseItem item)
        {
            if (item != null)
            {
                return
                    item.ItemStruct.RequiredReputationForSaleInShop <= _selectedCity.PlayerReputation &&
                    _player.GoldAmount >= Data.HubMapData.ShopService.GetItemPrice(item);
            }
            else
            {
                return true;
            }
        }

        private string ImpossibleToBuyMessage(BaseItem item)
        {
            StringBuilder message = new StringBuilder(); 

            if (item.ItemStruct.RequiredReputationForSaleInShop <= _selectedCity.PlayerReputation)
            {
                message.AppendLine("Недостаточно репутации для покупки.");
                message.AppendLine($"Необходимая репутация: {item.ItemStruct.RequiredReputationForSaleInShop}.");
            }

            if (_player.GoldAmount >= Data.HubMapData.ShopService.GetItemPrice(item))
            {
                message.AppendLine($"Недостаточно денег.");
            }

            Debug.Log(message.ToString());
            return message.ToString();
        }

        private void LocationLoad()
        {
            Debug.Log("Load location. Location: " + _selectedLocation);
        }

        #endregion

    }
}
