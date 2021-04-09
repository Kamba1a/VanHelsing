using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace BeastHunter
{
    public class HubMapUIMainBehaviour : MonoBehaviour
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
            private HubMapUIMapObjectModel _mapObject;

            public Action<int?> OnChanged_EquipmentSlotIndex { get; set; }
            public Action<int?> OnChanged_InventorySlotIndex { get; set; }
            public Action<int?> OnChanged_ShopSlotIndex { get; set; }
            public Action<int?> OnChanged_BuyBackSlotIndex { get; set; }
            public Action<HubMapUICharacterModel> OnChanged_Character { get; set; }
            public Action<HubMapUIMapObjectModel> OnChanged_MapObject { get; set; }

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

            public HubMapUIMapObjectModel MapObject
            {
                get
                {
                    return _mapObject;
                }
                set
                {
                    if (value != _mapObject)
                    {
                        HubMapUIMapObjectModel previousValue = _mapObject;
                        _mapObject = value;
                        OnChanged_MapObject?.Invoke(previousValue);
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
                OnChanged_MapObject = null;
            }
        }

        #endregion


        #region Constants

        private const float CHARACTERS_PANEL_SCROLLBAR_STEP = 1.0f;

        #endregion


        #region Fields

        [Header("Map objects")]
        [SerializeField] private GameObject[] _mapObjects;

        [Header("Hub map")]
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;
        [SerializeField] private Button _hideInfoPanelButton;
        [SerializeField] private Button _hubButton;
        [SerializeField] private Button _mapButton;
        [SerializeField] private GameObject _inventoryItemsPanel;
        [SerializeField] private GameObject _tooltip;
        [SerializeField] private GameObject _loadingPanel;

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
        //[SerializeField] private GameObject _hikePreparePanel;
        [SerializeField] private GameObject _hikeCharactersPanel;
        [SerializeField] private GameObject _characterInventoryPanel;
        //[SerializeField] private GameObject _hikeInventoryPanel;
        [SerializeField] private GameObject _hikeInventoryScrollView;
        //[SerializeField] private Button _closeInventoryButton;
        [SerializeField] private Button _perkTreeButton;
        [SerializeField] private Text _travelTimeText;
        [SerializeField] private RawImage _character3DViewModelRawImage;
        [SerializeField] private GameObject _characterClothPanel;

        [Header("Characters panel relocatable")]
        [SerializeField] private GameObject _charactersPanelRelocatable;
        [SerializeField] private GameObject _charactersPanelFillable;
        [SerializeField] private Scrollbar _charactersPanelScrollbar;
        [SerializeField] private Button _charactersPanelNextButton;
        [SerializeField] private Button _charactersPanelPreviousButton;

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

        [Header("Perks panel")]
        [SerializeField] private GameObject _perksPanel;
        [SerializeField] private Button _closePerksPanelButton;
        [SerializeField] private GameObject _perksCharactersPanel;


        private HubMapUIContext _context;
        private HubMapUIQuestController _questController;

        private HubMapUIData _data;
        private HubMapUIPlayerModel _player;
        private HubMapUIStorage _generalInventory;

        private List<GameObject> _rightInfoPanelObjectsForDestroy;
        private List<HubMapUIStorageSlotBehaviour> _characterInventorySlotsUIBehaviours;
        private List<HubMapUIStorageSlotBehaviour> _generalInventorySlotsUIBehaviours;
        private List<HubMapUIShopSlotBehaviour> _shopSlotsUIBehaviours;
        private List<HubMapUIStorageSlotBehaviour> _buyBackSlotsUIBehaviours;
        private HubMapUIEquipmentSlotBehaviour[] _characterClothSlotsUIBehaviours;
        private Dictionary<HubMapUICitizenModel, HubMapUICitizenBehaviour> _displayedCurrentCitizensUIBehaviours;
        private List<GameObject> _displayedDialogAnswerButtons;
        private (int? slotIndex, StorageType storageType) _draggedItemInfo;

        private HubMapUIStorage _currentBuyBackStorage;
        private HubMapUIStorage _currentShopStorage;
        private SelectedElements _selected;
        private GameObject Character3DViewModelRendering;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _hubButton.onClick.AddListener(OnClick_HubButton);
            _mapButton.onClick.AddListener(OnClick_MapButton);
            _hideInfoPanelButton.onClick.AddListener(OnClick_HideInfoPanelButton);
            _hikeAcceptButton.onClick.AddListener(OnClick_HikeAcceptButton);
            _closeHikePanelButton.onClick.AddListener(OnClick_CloseHikePanelButton);
            _hikePanelButton.onClick.AddListener(OnClick_HikePanelButton);
            _charactersPanelNextButton.onClick.AddListener(()=> OnClick_CharactersPanelNavigationButton(CHARACTERS_PANEL_SCROLLBAR_STEP));
            _charactersPanelPreviousButton.onClick.AddListener(() => OnClick_CharactersPanelNavigationButton(-CHARACTERS_PANEL_SCROLLBAR_STEP));
            //_closeInventoryButton.onClick.AddListener(OnClick_CloseInventoryButton);
            _perkTreeButton.onClick.AddListener(OnClick_PerkTreeButton);
            _shopButton.onClick.AddListener(OnClick_OpenTradePanelButton);
            _closeTradePanelButton.onClick.AddListener(OnClick_CloseTradePanelButton);
            _sellButton.onClick.AddListener(OnClick_SellItemButton);
            _buyBackButton.onClick.AddListener(OnClick_BuyBackItemButton);
            _buyButton.onClick.AddListener(OnClick_BuyItemButton);
            _closePerksPanelButton.onClick.AddListener(OnClick_ClosePerksButton);
        }

        private void OnDisable()
        {
            _hubButton.onClick.RemoveAllListeners();
            _mapButton.onClick.RemoveAllListeners();
            _hideInfoPanelButton.onClick.RemoveAllListeners();
            _hikeAcceptButton.onClick.RemoveAllListeners();
            _closeHikePanelButton.onClick.RemoveAllListeners();
            _hikePanelButton.onClick.RemoveAllListeners();
            _charactersPanelNextButton.onClick.RemoveAllListeners();
            _charactersPanelPreviousButton.onClick.RemoveAllListeners();
            //_closeInventoryButton.onClick.RemoveAllListeners();
            _perkTreeButton.onClick.RemoveAllListeners();
            _shopButton.onClick.RemoveAllListeners();
            _closeTradePanelButton.onClick.RemoveAllListeners();
            _sellButton.onClick.RemoveAllListeners();
            _buyBackButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
            _closePerksPanelButton.onClick.RemoveAllListeners();
        }

        private void Awake()
        {
            _data = Data.HubMapData;

            HubMapUIServices.SharedInstance.InitializeServices();
            _context = new HubMapUIContext(_data.ContextData);
            _questController = new HubMapUIQuestController(_context);

            _player = _context.Player;
            _generalInventory = _player.Inventory;

            _rightInfoPanelObjectsForDestroy = new List<GameObject>();
            _displayedCurrentCitizensUIBehaviours = new Dictionary<HubMapUICitizenModel, HubMapUICitizenBehaviour>();
            _displayedDialogAnswerButtons = new List<GameObject>();

            _characterInventorySlotsUIBehaviours = new List<HubMapUIStorageSlotBehaviour>();
            _generalInventorySlotsUIBehaviours = new List<HubMapUIStorageSlotBehaviour>();
            _buyBackSlotsUIBehaviours = new List<HubMapUIStorageSlotBehaviour>();
            _shopSlotsUIBehaviours = new List<HubMapUIShopSlotBehaviour>();
            _characterClothSlotsUIBehaviours = _characterClothPanel.GetComponentsInChildren<HubMapUIEquipmentSlotBehaviour>();

            _selected = new SelectedElements();
        }

        private void Start()
        {
            if (_characterClothSlotsUIBehaviours.Length != _context.CharactersClothEquipment.Length)
            {
                Debug.LogError("The number of cloth UI slots does not match the equipment of the characters!");
            }

            for (int i = 0; i < _characterClothSlotsUIBehaviours.Length; i++)
            {
                Sprite slotSprite = Data.HubMapData.GetClothSlotSpriteByType(_context.CharactersClothEquipment[i]);
                _characterClothSlotsUIBehaviours[i].FillSlotInfo(i, true, slotSprite);
                //todo: drag and drop deactivate when character not select with visual effect
            }

            Character3DViewModelRendering =
                Instantiate(_data.Characters3DViewRenderingPrefab, _data.Characters3DViewRenderingObjectPosition, Quaternion.identity);

            for (int i = 0; i < _mapObjects.Length; i++)
            {
                FillMapObject(_mapObjects[i], _data.MapObjects[i]);
            }

            for (int i = 0; i < _context.Characters.Count; i++)
            {
                InitializeCharacterUI(_context.Characters[i]);
            }

            for (int i = 0; i < _context.CharactersEquipmentSlotAmount; i++)
            {
                InitializeEquipmentSlotUI(i);
            }

            for (int i = 0; i < _generalInventory.GetSlotsCount(); i++)
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
            _generalInventory.OnChangeItemHandler = (slotIndex, item) => FillSlotUI(slotIndex, item, StorageType.Inventory);

            _selected.OnChanged_InventorySlotIndex = OnChangedSelectedInventorySlot;
            _selected.OnChanged_EquipmentSlotIndex = OnChangedSelectedEquipmentSlot;
            _selected.OnChanged_BuyBackSlotIndex = OnChangedSelectedBuyBackSlot;
            _selected.OnChanged_ShopSlotIndex = OnChangedSelectedShopSlot;
            _selected.OnChanged_Character = OnChangedSelectedCharacter;
            _selected.OnChanged_MapObject = OnChangedSelectedMapObject;

            _mainPanel.SetActive(_data.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
            //_hikePreparePanel.SetActive(true);
            //_hikeInventoryPanel.SetActive(true);
            _tradePanel.SetActive(false);
            _sellButton.interactable = false;
            _buyBackButton.interactable = false;
            _buyButton.interactable = false;
            _tooltip.SetActive(false);
            _perksPanel.SetActive(false);
            _hikeAcceptButton.interactable = false;
            _loadingPanel.SetActive(false);
            _character3DViewModelRawImage.enabled = false;
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
            _travelTimeText.text = HubMapUIServices.SharedInstance.TravelTimeService.
                GetFullPhraseAboutTravelTime(_selected.MapObject as HubMapUILocationModel);
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
            _loadingPanel.SetActive(true);
            _mainPanel.SetActive(false);
            LocationLoad();
        }

        private void OnClick_CloseHikePanelButton()
        {
            _selected.Character = null;
            _selected.EquipmentSlotIndex = null;
            _selected.InventorySlotIndex = null;

            //_hikeInventoryPanel.SetActive(false);
            _hikePanel.SetActive(false);
        }

        private void OnClick_CityButton(HubMapUIMapObjectModel mapObjectModel)
        {
            _selected.MapObject = mapObjectModel;
            HubMapUICityModel cityModel = mapObjectModel as HubMapUICityModel;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillCityPanel(cityModel);

            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _cityInfoPanel.SetActive(true);
        }

        private void OnClick_LocationButton(HubMapUIMapObjectModel mapObjectModel)
        {
            _selected.MapObject = mapObjectModel;
            HubMapUILocationModel locationModel = mapObjectModel as HubMapUILocationModel;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationPanel(locationModel);

            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _locationInfoPanel.SetActive(true);
        }

        private void OnClick_OpenTradePanelButton()
        {
            HubMapUICityModel city = _selected.MapObject as HubMapUICityModel;

            SetScrollViewParentForPanel(_inventoryItemsPanel, _shopInventoryScrollView);
            _playerGoldAmount.text = _player.GoldAmount.ToString();
            _shopCityReputation.text = city.PlayerReputation.ToString();

            _currentShopStorage = city.ShopStorage;
            FillItemsSlots(StorageType.Shop);
            _currentShopStorage.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Shop);

            _currentBuyBackStorage = city.BuyBackStorage;
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

        private void OnClick_CitizenButton(HubMapUICitizenModel citizen)
        {
            FillDialogPanel(citizen);
            _dialogPanel.SetActive(true);
        }

        private void OnClick_CharactersPanelNavigationButton(float step)
        {
            _charactersPanelScrollbar.value += step;
        }

        //private void OnClick_CloseInventoryButton()
        //{
        //    _hikeInventoryPanel.SetActive(false);
        //    _selected.InventorySlotIndex = null;
        //}

        private void OnClick_PerkTreeButton()
        {
            _hikePanel.SetActive(false);

            SetOtherParentForPanel(_charactersPanelRelocatable, _perksCharactersPanel);

            _perksPanel.SetActive(true);
        }

        private void OnClick_ClosePerksButton()
        {
            _perksPanel.SetActive(false);

            SetOtherParentForPanel(_charactersPanelRelocatable, _hikeCharactersPanel);

            _hikePanel.SetActive(true);
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

            //if (!_hikeInventoryPanel.activeSelf)
            //{
            //    SetScrollViewParentForPanel(_inventoryItemsPanel, _hikeInventoryScrollView);
            //    _hikeInventoryPanel.SetActive(true);
            //}
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
                HubMapUIBaseItemModel inventoryItem = _generalInventory.TakeItem(slotIndex);

                if (_selected.EquipmentSlotIndex.HasValue)
                {
                    HubMapUIBaseItemModel equipmentItem = _selected.Character.Backpack.TakeItem(_selected.EquipmentSlotIndex.Value);

                    _generalInventory.PutItem(slotIndex, equipmentItem);
                    _selected.Character.Backpack.PutItem(_selected.EquipmentSlotIndex.Value, inventoryItem);
                }
                else
                {
                    if (!_selected.Character.Backpack.PutItemToFirstEmptySlot(inventoryItem))
                    {
                        _generalInventory.PutItem(slotIndex, inventoryItem);
                        Debug.Log("Equipment is full");
                    }
                }
            }
        }

        private void OnDoubleClick_EquipmentSlot(int slotIndex)
        {
            HubMapUIBaseItemModel equipmentItem = _selected.Character.Backpack.TakeItem(slotIndex);

            if (_selected.InventorySlotIndex.HasValue)
            {
                HubMapUIBaseItemModel inventoryItem = _generalInventory.TakeItem(_selected.InventorySlotIndex.Value);

                _generalInventory.PutItem(_selected.InventorySlotIndex.Value, equipmentItem);
                _selected.Character.Backpack.PutItem(slotIndex, inventoryItem);
            }
            else
            {
                bool isFoundEmptySlot = false;

                for (int i = 0; i < _generalInventory.GetSlotsCount(); i++)
                {
                    if (_generalInventory.GetItemBySlot(i) == null)
                    {
                        _generalInventory.PutItem(i, equipmentItem);
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

        private void OnClick_AnswerButton(HubMapUICitizenModel citizen, HubMapUIDialogAnswer dialogAnswer)
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
                HubMapUIBaseItemModel sellingItem = _generalInventory.TakeItem(_selected.InventorySlotIndex.Value);

                if (sellingItem != null)
                {
                    if (_currentBuyBackStorage.PutItemToFirstEmptySlot(sellingItem))
                    {
                        ChangePlayerGoldAmount(HubMapUIServices.SharedInstance.ShopService.CountSellPrice(sellingItem));
                    }
                    else
                    {
                        _generalInventory.PutItem(_selected.InventorySlotIndex.Value, sellingItem);
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
                HubMapUIBaseItemModel buyingItem = _currentBuyBackStorage.TakeItem(_selected.BuyBackSlotIndex.Value);
                if (buyingItem != null)
                {
                    if (_player.GoldAmount >= HubMapUIServices.SharedInstance.ShopService.CountSellPrice(buyingItem)) 
                    { 
                        if (_generalInventory.PutItemToFirstEmptySlot(buyingItem))
                        {
                            ChangePlayerGoldAmount(-HubMapUIServices.SharedInstance.ShopService.CountSellPrice(buyingItem));
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
                HubMapUIBaseItemModel buyingItem = _currentShopStorage.TakeItem(_selected.ShopSlotIndex.Value);
                if (buyingItem != null)
                {
                    if (IsPossibleToBuyShopItem(buyingItem))
                    {
                        if (_generalInventory.PutItemToFirstEmptySlot(buyingItem))
                        {
                            ChangePlayerGoldAmount(-HubMapUIServices.SharedInstance.ShopService.GetItemPrice(buyingItem));
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

            _tooltip.GetComponent<RectTransform>().pivot =
                storageType == StorageType.BuyBackStorage || storageType == StorageType.Shop ?
                new Vector2(1, 0) :
                new Vector2(0, 0);

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

                HubMapUIBaseItemModel draggedItem = takeItemStorage.TakeItem(_draggedItemInfo.slotIndex.Value);
                HubMapUIBaseItemModel droppedSlotItem = dropItemStorage.TakeItem(dropSlotIndex);

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
                _generalInventorySlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.InventorySlotIndex.HasValue)
            {
                _generalInventorySlotsUIBehaviours[_selected.InventorySlotIndex.Value].SelectFrameSwitcher(true);

                if (_tradePanel.activeSelf)
                {
                    if (_generalInventory.GetItemBySlot(_selected.InventorySlotIndex.Value) != null)
                    {
                        _sellingItemPrice.text = HubMapUIServices.SharedInstance.ShopService.
                            CountSellPrice(_generalInventory.GetItemBySlot(_selected.InventorySlotIndex.Value)).ToString();
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
                _characterInventorySlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.EquipmentSlotIndex.HasValue)
            {
                _characterInventorySlotsUIBehaviours[_selected.EquipmentSlotIndex.Value].SelectFrameSwitcher(true);
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
                        _buybackItemPrice.text = HubMapUIServices.SharedInstance.ShopService.
                            CountSellPrice(_currentBuyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value)).ToString();

                        _buyBackButton.interactable = _player.GoldAmount >= HubMapUIServices.SharedInstance.ShopService.
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
                    HubMapUIBaseItemModel item = _currentShopStorage.GetItemBySlot(_selected.ShopSlotIndex.Value);
                    if (item != null)
                    {
                        _buyingItemPrice.text = HubMapUIServices.SharedInstance.ShopService.GetItemPrice(item).ToString();
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
                previousCharacter.Behaviour.SelectFrameSwitch(false);
                previousCharacter.Backpack.OnChangeItemHandler = null;
                previousCharacter.View3DModelObjectOnScene.SetActive(false);
                previousCharacter.View3DModelObjectOnScene.transform.rotation = Quaternion.identity;
            }

            if (_selected.Character != null)
            {
                _selected.Character.Behaviour.SelectFrameSwitch(true);
                _hikeAcceptButton.interactable = true;
                FillItemsSlots(StorageType.Equipment);
                SetEquipmentSlotsInteractable(true);
                _selected.Character.Backpack.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageType.Equipment);

                _selected.Character.View3DModelObjectOnScene.SetActive(true);
                _character3DViewModelRawImage.enabled = true;
                _character3DViewModelRawImage.GetComponent<HubMapUIView3DModelBehaviour>().RotateObject = _selected.Character.View3DModelObjectOnScene;
            }
            else
            {
                _character3DViewModelRawImage.enabled = false;
                _hikeAcceptButton.interactable = false;
                SetEquipmentSlotsInteractable(false);
            }
        }

        private void OnChangedSelectedMapObject(HubMapUIMapObjectModel previousMapObject)
        {
            if (previousMapObject != null)
            {
                previousMapObject.Behaviour.SelectFrameSwitch(false);
            }

            if (_selected.MapObject != null)
            {
                _selected.MapObject.Behaviour.SelectFrameSwitch(true);
            }
        }

        #endregion


        #region InitializeUIElements

        private void InitializeCharacterUI(HubMapUICharacterModel character)
        {
            GameObject characterUI = InstantiateUIObject(_data.CharacterUIPrefab, _charactersPanelFillable);
            HubMapUICharacterBehaviour behaviourUI = characterUI.GetComponentInChildren<HubMapUICharacterBehaviour>();
            behaviourUI.Initialize(character);
            behaviourUI.OnClick_ButtonHandler += OnClick_CharacterButton;

            character.InitializeView3DModel(Character3DViewModelRendering.transform);
            //character.View3DModelObjectOnScene = Instantiate(character.View3DModelPrefab, Character3DViewModelRendering.transform);
            //character.View3DModelObjectOnScene.SetActive(false);
        }

        private void InitializeEquipmentSlotUI(int slotIndex)
        {
            GameObject equipSlotUI = InstantiateUIObject(_data.EquipmentSlotUIPrefab, _characterInventoryPanel);

            HubMapUIStorageSlotBehaviour slotBehaviour = equipSlotUI.GetComponent<HubMapUIStorageSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, true);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_EquipmentSlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_EquipmentSlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageType.Equipment);
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.Equipment);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _characterInventorySlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeInventorySlotUI(int slotIndex)
        {
            GameObject inventorySlotUI = InstantiateUIObject(_data.InventorySlotUIPrefab, _inventoryItemsPanel);

            HubMapUIStorageSlotBehaviour slotBehaviour = inventorySlotUI.GetComponent<HubMapUIStorageSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, true);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_InventorySlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_InventorySlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageType.Inventory);
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.Inventory);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _generalInventorySlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeBuyBackSlotUI(int slotIndex)
        {
            GameObject buyBackSlotUI = InstantiateUIObject(_data.ShopSlotUIPrefab, _buyBackItemsPanel);

            HubMapUIStorageSlotBehaviour slotBehaviour = buyBackSlotUI.GetComponent<HubMapUIStorageSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, false);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_BuyBackSlot;
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.BuyBackStorage);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _buyBackSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeShopSlotUI(int slotIndex)
        {
            GameObject shopSlotUI = InstantiateUIObject(_data.ShopSlotUIPrefab, _buyingItemsPanel);

            HubMapUIShopSlotBehaviour slotBehaviour = shopSlotUI.GetComponent<HubMapUIShopSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, false);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_ShopSlot;
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, StorageType.Shop);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _shopSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeCitizenUI(HubMapUICitizenModel citizen)
        {
            GameObject citizenUI = InstantiateUIObject(_data.CitizenUIPrefab, _citizenPanel);

            HubMapUICitizenBehaviour citizenUIBehaviour = citizenUI.GetComponentInChildren<HubMapUICitizenBehaviour>();
            citizenUIBehaviour.FillCitizenInfo(citizen);
            citizenUIBehaviour.OnClick_CitizenButtonHandler = OnClick_CitizenButton;

            citizen.OnChangeQuestMarkerTypeHandler += citizenUIBehaviour.SetQuestMarker;

            _rightInfoPanelObjectsForDestroy.Add(citizenUI);
            _displayedCurrentCitizensUIBehaviours.Add(citizen, citizenUIBehaviour);
        }

        private void InitializeDialogAnswerButton(HubMapUICitizenModel citizen, HubMapUIDialogAnswer answer)
        {
            GameObject answerButton = InstantiateUIObject(_data.AnswerButtonUIPrefab, _answerButtonsPanel);
            answerButton.GetComponentInChildren<Text>().text = answer.Text;

            if (answer.IsDialogEnd)
            {
                answerButton.GetComponentInChildren<Text>().text += " (����)";
            }

            answerButton.GetComponentInChildren<Button>().interactable = answer.IsInteractable;
            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnClick_AnswerButton(citizen, answer));
            _displayedDialogAnswerButtons.Add(answerButton);
        }

        #endregion


        #region FillUIElementsByInfo

        private void FillMapObject(GameObject mapObject, HubMapUIMapObjectData mapObjectdata)
        {
            HubMapUIMapObjectModel mapObjectModel = _context.GetMapObjectModel(mapObjectdata);

            if (mapObject != null)
            {
                HubMapUIMapObjectBehaviour mapObjectBehaviour = mapObject.GetComponent<HubMapUIMapObjectBehaviour>();
                mapObjectModel.Behaviour = mapObjectBehaviour;

                switch (mapObjectdata.GetMapObjectType())
                {
                    case HubMapUIMapObjectType.Location:

                        mapObjectBehaviour.FillInfo(mapObjectModel as HubMapUILocationModel);
                        mapObjectBehaviour.OnClick_ButtonHandler += OnClick_LocationButton;

                        break;
                    case HubMapUIMapObjectType.City:

                        mapObjectBehaviour.FillInfo(mapObjectModel as HubMapUICityModel);
                        mapObjectBehaviour.OnClick_ButtonHandler += OnClick_CityButton;

                        break;
                    default:

                        Debug.LogError(this + " incorrect HubMapUIMapObjectType value");

                        break;
                }
            }
            else
            {
                Debug.LogError(this + " HubMapUIContext not contain requested HubMapUIMapObjectModel: " + mapObjectModel.Name);
            }
        }

        private void FillTooltipByItemInfo(HubMapUIBaseItemModel item, StorageType storageType)
        {
            Text[] tooltipTexts = _tooltip.GetComponentsInChildren<Text>(true);

            tooltipTexts[0].gameObject.SetActive(false);
            tooltipTexts[1].text = item.Name;
            tooltipTexts[2].text = item.Description;
            tooltipTexts[3].text = "����: " + HubMapUIServices.SharedInstance.ShopService.GetItemPrice(item).ToString();

            if (storageType == StorageType.Shop)
            {
                if (!IsPossibleToBuyShopItem(item, out string message))
                {
                    tooltipTexts[0].gameObject.SetActive(true);
                    tooltipTexts[0].text = message;
                }
            }
        }

        private void FillSlotUI(int slotIndex, HubMapUIBaseItemModel item, StorageType storageType)
        {
            Sprite sprite = item != null ? item.Icon : null;

            switch (storageType)
            {
                case StorageType.Equipment:

                    _characterInventorySlotsUIBehaviours[slotIndex].FillSlot(sprite);

                    break;
                case StorageType.Inventory:

                    _generalInventorySlotsUIBehaviours[slotIndex].FillSlot(sprite);

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

        private void FillLocationPanel(HubMapUILocationModel location)
        {
            _locationScreen.sprite = location.Screenshot;
            _locationName.text = location.Name;
            _locationDescription.text = location.Description;

            if (location.Dwellers.Length > 0)
            {
                _dwellersPanel.transform.GetChild(0).gameObject.SetActive(false);

                for (int i = 0; i < location.Dwellers.Length; i++)
                {
                    GameObject dwellerUI = InstantiateUIObject(_data.LocationTextUIPrefab, _dwellersPanel);
                    _rightInfoPanelObjectsForDestroy.Add(dwellerUI);
                    dwellerUI.GetComponentInChildren<Text>().text = location.Dwellers[i].Name;
                }
            }
            else
            {
                _dwellersPanel.transform.GetChild(0).gameObject.SetActive(true);
            }


            if (location.Ingredients.Length > 0)
            {
                _ingredientsPanel.transform.GetChild(0).gameObject.SetActive(false);

                for (int i = 0; i < location.Ingredients.Length; i++)
                {
                    GameObject ingredientUI = InstantiateUIObject(_data.LocationTextUIPrefab, _ingredientsPanel);
                    _rightInfoPanelObjectsForDestroy.Add(ingredientUI);
                    ingredientUI.GetComponentInChildren<Text>().text = location.Ingredients[i].Name;
                }
            }
            else
            {
                _ingredientsPanel.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        private void FillDialogPanel(HubMapUICitizenModel citizen)
        {
            _citizenName.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _dialogText.text = citizen.CurrentDialog.Text;

            List<HubMapUIDialogAnswer> answers = citizen.GetAllCurrentAnswers();
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
            for (int i = 0; i < _characterInventorySlotsUIBehaviours.Count; i++)
            {
                if (!flag)
                { 
                    _characterInventorySlotsUIBehaviours[i].FillSlot(null);
                }
                _characterInventorySlotsUIBehaviours[i].SetInteractable(flag);
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
                    storage = _generalInventory;
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

        private void SetScrollViewParentForPanel(GameObject panel, GameObject parentPanel)
        {
            panel.transform.SetParent(parentPanel.transform.Find("Viewport"), false);
            parentPanel.GetComponent<ScrollRect>().content = panel.GetComponent<RectTransform>();
        }

        private void SetOtherParentForPanel(GameObject panel, GameObject parentPanel)
        {
            panel.transform.SetParent(parentPanel.transform, false);
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

        private bool IsPossibleToBuyShopItem(HubMapUIBaseItemModel item)
        {
            if (item != null)
            {
                return
                    item.RequiredReputationForSaleInShop <= (_selected.MapObject as HubMapUICityModel).PlayerReputation &&
                    _player.GoldAmount >= HubMapUIServices.SharedInstance.ShopService.GetItemPrice(item);
            }
            else
            {
                return true;
            }
        }

        private bool IsPossibleToBuyShopItem(HubMapUIBaseItemModel item, out string message)
        {
            bool flag = true;
            StringBuilder sb = new StringBuilder();

            if (item != null)
            {
                if ((_selected.MapObject as HubMapUICityModel).PlayerReputation < item.RequiredReputationForSaleInShop)
                {
                    flag = false;

                    sb.AppendFormat("������������ ��������� ��� �������");
                    sb.AppendLine();
                    sb.AppendFormat($"����������� ���������: {item.RequiredReputationForSaleInShop}");
                }
                if (_player.GoldAmount < HubMapUIServices.SharedInstance.ShopService.GetItemPrice(item))
                {
                    flag = false;

                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }
                    sb.AppendFormat("������������ �����");
                }
            }

            message = sb.ToString();
            return flag;
        }

         private void LocationLoad()
        {
            Debug.Log("Load location. ID: " + (_selected.MapObject as HubMapUILocationModel).LoadSceneId);
            SceneManager.LoadScene((_selected.MapObject as HubMapUILocationModel).LoadSceneId);
        }

        #endregion

    }
}
