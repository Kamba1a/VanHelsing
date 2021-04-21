using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


namespace BeastHunterHubUI
{
    public class MapBehaviour : MonoBehaviour, IStart, IDestroy
    {
        #region PrivateData

        private class SelectedElements
        {
            private int? _generalInventorySlotIndex;
            private int? _shopSlotIndex;
            private int? _buyBackSlotIndex;
            private CharacterModel _character;
            private MapObjectModel _mapObject;

            public Action<int?> OnChanged_GeneralInventorySlotIndex { get; set; }
            public Action<int?> OnChanged_ShopSlotIndex { get; set; }
            public Action<int?> OnChanged_BuyBackSlotIndex { get; set; }
            public Action<CharacterModel> OnChanged_Character { get; set; }
            public Action<MapObjectModel> OnChanged_MapObject { get; set; }

            public int? GeneralInventorySlotIndex
            {
                get
                {
                    return _generalInventorySlotIndex;
                }
                set
                {
                    if (value != _generalInventorySlotIndex)
                    {
                        int? previousValue = _generalInventorySlotIndex;
                        _generalInventorySlotIndex = value;
                        OnChanged_GeneralInventorySlotIndex?.Invoke(previousValue);
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

            public CharacterModel Character
            {
                get
                {
                    return _character;
                }
                set
                {
                    if (value != _character)
                    {
                        CharacterModel previousValue = _character;
                        _character = value;
                        OnChanged_Character?.Invoke(previousValue);
                    }
                }
            }

            public MapObjectModel MapObject
            {
                get
                {
                    return _mapObject;
                }
                set
                {
                    if (value != _mapObject)
                    {
                        MapObjectModel previousValue = _mapObject;
                        _mapObject = value;
                        OnChanged_MapObject?.Invoke(previousValue);
                    }
                }
            }

            public void RemoveAllListeners()
            {
                OnChanged_GeneralInventorySlotIndex = null;
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
        [SerializeField] private GameObject _hikeCharactersPanel;
        [SerializeField] private GameObject _characterInventoryPanel;
        [SerializeField] private GameObject _hikeInventoryScrollView;
        [SerializeField] private Button _perkTreeButton;
        [SerializeField] private Text _travelTimeText;
        [SerializeField] private RawImage _character3DViewModelRawImage;
        [SerializeField] private GameObject _characterClothPanel;
        [SerializeField] private GameObject _pocketSlotsPanel;
        [SerializeField] private GameObject _weaponSetsPanel;

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


        private HubUIContext _context;
        private HubUIMapData _data;
        private PlayerModel _player;
        private ItemStorage _generalInventory;

        private List<GameObject> _rightInfoPanelObjectsForDestroy;
        private List<MapStorageSlotBehaviour> _characterBackpuckSlotsUIBehaviours;
        private List<MapStorageSlotBehaviour> _generalInventorySlotsUIBehaviours;
        private List<MapShopSlotBehaviour> _shopSlotsUIBehaviours;
        private List<MapStorageSlotBehaviour> _buyBackSlotsUIBehaviours;
        private MapEquipmentSlotBehaviour[] _characterClothesSlotsUIBehaviours;
        private MapEquipmentSlotBehaviour[] _characterWeaponSlotsUIBehaviours;
        private List<MapEquipmentSlotBehaviour> _characterPocketsSlotsBehaviours;
        private Dictionary<CitizenModel, MapCitizenBehaviour> _displayedCurrentCitizensUIBehaviours;
        private List<GameObject> _displayedDialogAnswerButtons;
        private (int? slotIndex, ItemStorageType storageType) _draggedItemInfo;

        private SelectedElements _selected;

        private GameObject _character3DViewModelRendering;
        private MapCharacterView3DModelBehaviour _character3DViewModelRawImageBehaviour;

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
            _perkTreeButton.onClick.RemoveAllListeners();
            _shopButton.onClick.RemoveAllListeners();
            _closeTradePanelButton.onClick.RemoveAllListeners();
            _sellButton.onClick.RemoveAllListeners();
            _buyBackButton.onClick.RemoveAllListeners();
            _buyButton.onClick.RemoveAllListeners();
            _closePerksPanelButton.onClick.RemoveAllListeners();
        }

        #endregion


        #region IHubMapUIStart

        public void Starting(HubUIContext context)
        {
            _data = BeastHunter.Data.HubMapData;
            _context = context;
            _player = context.Player;
            _generalInventory = _player.Inventory;

            _rightInfoPanelObjectsForDestroy = new List<GameObject>();
            _displayedCurrentCitizensUIBehaviours = new Dictionary<CitizenModel, MapCitizenBehaviour>();
            _displayedDialogAnswerButtons = new List<GameObject>();

            _characterBackpuckSlotsUIBehaviours = new List<MapStorageSlotBehaviour>();
            _generalInventorySlotsUIBehaviours = new List<MapStorageSlotBehaviour>();
            _buyBackSlotsUIBehaviours = new List<MapStorageSlotBehaviour>();
            _shopSlotsUIBehaviours = new List<MapShopSlotBehaviour>();
            _characterClothesSlotsUIBehaviours = _characterClothPanel.GetComponentsInChildren<MapEquipmentSlotBehaviour>();
            _characterPocketsSlotsBehaviours = new List<MapEquipmentSlotBehaviour>();
            _characterWeaponSlotsUIBehaviours = _weaponSetsPanel.GetComponentsInChildren<MapEquipmentSlotBehaviour>();

            _character3DViewModelRawImageBehaviour = _character3DViewModelRawImage.GetComponent<MapCharacterView3DModelBehaviour>();

            _selected = new SelectedElements();


            if (_characterClothesSlotsUIBehaviours.Length != context.CharactersClothEquipment.Length)
            {
                Debug.LogError("The number of cloth UI slots does not match the equipment of the characters!");
            }

            for (int i = 0; i < _characterClothesSlotsUIBehaviours.Length; i++)
            {
                FillCharacterClothesSlot(i);
            }

            if (_characterWeaponSlotsUIBehaviours.Length != context.CharactersWeaponSetsAmount * 2)
            {
                Debug.LogError("The number of weapon UI slots does not match the equipment of the characters!");
            }

            for (int i = 0; i < _characterWeaponSlotsUIBehaviours.Length; i++)
            {
                FillWeaponSlot(i);
            }

            for (int i = 0; i < _mapObjects.Length; i++)
            {
                FillMapObject(_mapObjects[i], _data.MapObjects[i]);
            }

            for (int i = 0; i < context.CharactersEquipmentSlotAmount; i++)
            {
                InitializeCharacterBackpuckSlotUI(i);
            }

            for (int i = 0; i < _generalInventory.GetSlotsCount(); i++)
            {
                InitializeGeneralInventorySlotUI(i);
            }

            for (int i = 0; i < context.ShopsSlotsAmount; i++)
            {
                InitializeBuyBackSlotUI(i);
            }

            for (int i = 0; i < context.ShopsSlotsAmount; i++)
            {
                InitializeShopSlotUI(i);
            }

            _character3DViewModelRendering =
                Instantiate(_data.Characters3DViewRenderingPrefab, _data.Characters3DViewRenderingObjectPosition, Quaternion.identity);
            for (int i = 0; i < context.Characters.Count; i++)
            {
                InitializeCharacterUI(context.Characters[i]);
            }

            FillItemStorageSlots(ItemStorageType.GeneralInventory);
            _generalInventory.OnPutItemToSlotHandler += FillSlotUI;
            _generalInventory.OnTakeItemFromSlotHandler += FillSlotUI;

            _selected.OnChanged_GeneralInventorySlotIndex = OnSelectedInventorySlot;
            _selected.OnChanged_BuyBackSlotIndex = OnSelectedBuyBackSlot;
            _selected.OnChanged_ShopSlotIndex = OnSelectedShopSlot;
            _selected.OnChanged_Character = OnSelectedCharacter;
            _selected.OnChanged_MapObject = OnSelectedMapObject;

            _character3DViewModelRawImageBehaviour.OnDropHandler += OnDropItemOn3DViewModelRawImage;

            _mainPanel.SetActive(_data.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
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


        #region IHubMapUIDestroy

        public void Destroying()
        {
            _generalInventory.OnPutItemToSlotHandler -= FillSlotUI;
            _generalInventory.OnTakeItemFromSlotHandler -= FillSlotUI;
            _character3DViewModelRawImageBehaviour.OnDropHandler -= OnDropItemOn3DViewModelRawImage;
        }

        #endregion

  
        #region Methods

        #region OnClick

        private void OnClick_MapButton()
        {
            _mainPanel.SetActive(true);
        }

        private void OnClick_HikePanelButton()
        {
            SetScrollViewParentForPanel(_inventoryItemsPanel, _hikeInventoryScrollView);

            _travelTimeText.text = HubUIServices.SharedInstance.TravelTimeService.
                GetFullPhraseAboutTravelTime(_selected.MapObject as LocationModel);
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
            _selected.GeneralInventorySlotIndex = null;

            _hikePanel.SetActive(false);
        }

        private void OnClick_CityButton(MapObjectModel mapObjectModel)
        {
            _selected.MapObject = mapObjectModel;
            CityModel cityModel = mapObjectModel as CityModel;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillCityPanel(cityModel);

            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _cityInfoPanel.SetActive(true);
        }

        private void OnClick_LocationButton(MapObjectModel mapObjectModel)
        {
            _selected.MapObject = mapObjectModel;
            LocationModel locationModel = mapObjectModel as LocationModel;

            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationPanel(locationModel);

            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);

            _locationInfoPanel.SetActive(true);
        }

        private void OnClick_OpenTradePanelButton()
        {
            CityModel city = _selected.MapObject as CityModel;

            SetScrollViewParentForPanel(_inventoryItemsPanel, _shopInventoryScrollView);
            _playerGoldAmount.text = _player.GoldAmount.ToString();
            _shopCityReputation.text = city.PlayerReputation.ToString();

            FillItemStorageSlots(ItemStorageType.ShopStorage);
            city.ShopStorage.OnPutItemToSlotHandler += FillSlotUI;
            city.ShopStorage.OnTakeItemFromSlotHandler += FillSlotUI;

            FillItemStorageSlots(ItemStorageType.BuyBackStorage);
            city.BuyBackStorage.OnPutItemToSlotHandler += FillSlotUI;
            city.BuyBackStorage.OnTakeItemFromSlotHandler += FillSlotUI;

            _tradePanel.SetActive(true);
        }

        private void OnClick_CloseTradePanelButton()
        {
            _selected.GeneralInventorySlotIndex = null;
            _selected.ShopSlotIndex = null;
            _selected.BuyBackSlotIndex = null;

            _tradePanel.SetActive(false);

            CityModel selectedCity = _selected.MapObject as CityModel;
            selectedCity.BuyBackStorage.Clear();
            selectedCity.BuyBackStorage.OnPutItemToSlotHandler -= FillSlotUI;
            selectedCity.BuyBackStorage.OnTakeItemFromSlotHandler -= FillSlotUI;
            selectedCity.ShopStorage.OnPutItemToSlotHandler -= FillSlotUI;
            selectedCity.ShopStorage.OnTakeItemFromSlotHandler -= FillSlotUI;
        }

        private void OnClick_CitizenButton(CitizenModel citizen)
        {
            FillDialogPanel(citizen);
            _dialogPanel.SetActive(true);
        }

        private void OnClick_CharactersPanelNavigationButton(float step)
        {
            _charactersPanelScrollbar.value += step;
        }

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

        private void OnClick_CharacterButton(CharacterModel character)
        {
            _selected.Character = character;
        }

        private void OnClick_AnswerButton(CitizenModel citizen, DialogAnswer dialogAnswer)
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
            if (_selected.GeneralInventorySlotIndex.HasValue)
            {
                BaseItemModel sellingItem = _generalInventory.GetItemBySlot(_selected.GeneralInventorySlotIndex.Value);

                if (sellingItem != null)
                {
                    if ((_selected.MapObject as CityModel).BuyBackStorage.PutItemToFirstEmptySlot(sellingItem))
                    {
                        _generalInventory.RemoveItem(_selected.GeneralInventorySlotIndex.Value);
                        ChangePlayerGoldAmount(HubUIServices.SharedInstance.ShopService.CountSellPrice(sellingItem));
                    }
                    else
                    {
                        Debug.Log("BuyBack storage is full");
                    }
                }

                _selected.GeneralInventorySlotIndex = null;
            }
        }

        private void OnClick_BuyBackItemButton()
        {
            if (_selected.BuyBackSlotIndex.HasValue)
            {
                ItemStorage buyBackStorage = (_selected.MapObject as CityModel).BuyBackStorage;
                BaseItemModel buyingItem = buyBackStorage.GetItemBySlot(_selected.BuyBackSlotIndex.Value);
                if (buyingItem != null)
                {
                    if (_player.GoldAmount >= HubUIServices.SharedInstance.ShopService.CountSellPrice(buyingItem))
                    {
                        if (_generalInventory.PutItemToFirstEmptySlot(buyingItem))
                        {
                            buyBackStorage.RemoveItem(_selected.BuyBackSlotIndex.Value);
                            ChangePlayerGoldAmount(-HubUIServices.SharedInstance.ShopService.CountSellPrice(buyingItem));
                        }
                        else
                        {
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
                ItemStorage shopStorage = (_selected.MapObject as CityModel).ShopStorage;
                BaseItemModel buyingItem = shopStorage.GetItemBySlot(_selected.ShopSlotIndex.Value);

                if (buyingItem != null)
                {
                    if (HubUIServices.SharedInstance.ShopService.IsPossibleToBuyShopItem(buyingItem, _selected.MapObject as CityModel))
                    {
                        if (_generalInventory.PutItemToFirstEmptySlot(buyingItem))
                        {
                            shopStorage.RemoveItem(_selected.ShopSlotIndex.Value);
                            ChangePlayerGoldAmount(-HubUIServices.SharedInstance.ShopService.GetItemPrice(buyingItem));
                        }
                        else
                        {
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

        #endregion

        #region OnPointer

        private void OnPointerDown_InventorySlot(int slotIndex)
        {
            _selected.GeneralInventorySlotIndex = slotIndex;
        }

        private void OnPointerDown_BuyBackSlot(int slotIndex)
        {
            _selected.BuyBackSlotIndex = slotIndex;
        }

        private void OnPointerDown_ShopSlot(int slotIndex)
        {
            _selected.ShopSlotIndex = slotIndex;
        }

        private void OnPointerEnter_Slot(int slotIndex, ItemStorageType storageType)
        {
            FillTooltipByItemInfo(slotIndex, storageType);

            RectTransform tooltipRectTransform = _tooltip.GetComponent<RectTransform>();

            if (storageType == ItemStorageType.GeneralInventory && _hikePanel.activeSelf)
            {
                tooltipRectTransform.pivot = new Vector2(0, 1);
            }
            else if (storageType == ItemStorageType.ShopStorage || storageType == ItemStorageType.BuyBackStorage)
            {
                tooltipRectTransform.pivot = new Vector2(1, 0);
            }
            else
            {
                tooltipRectTransform.pivot = new Vector2(0, 0);
            }

            _tooltip.transform.position = Mouse.current.position.ReadValue();
            _tooltip.SetActive(true);
        }

        private void OnPointerExit_Slot(int slotIndex)
        {
            _tooltip.SetActive(false);
        }

        #endregion

        #region OnDoubleClick

        private void OnDoubleClick_CharacterEquipmentSlot(int slotIndex, ItemStorageType storageType)
        {
            if (_selected.Character != null)
            {
                MoveItemToGeneralInventoryStorage(GetStorageByType(storageType), slotIndex);
            }
        }

        private void OnDoubleClick_StorageSlot(int slotIndex, ItemStorageType storageType)
        {
            BaseItemStorage storage = GetStorageByType(storageType);
            BaseItemModel item = storage.GetItemBySlot(slotIndex);
            if (_hikePanel.activeSelf && item != null && _selected.Character != null)
            {
                if(item.ItemType == ItemType.Clothes)
                {
                    EquipSelectedCharacterWithClothes(storage, slotIndex);
                }
                else if (item.ItemType == ItemType.Weapon)
                {
                    EquipSelectedCharacterWithWeapon(storage, slotIndex);
                }
            }
        }

        #endregion

        #region OnDragAndDrop

        private void OnBeginDragItemFromSlot(int slotIndex, ItemStorageType storageType)
        {
            _tooltip.SetActive(false);
            _draggedItemInfo.slotIndex = slotIndex;
            _draggedItemInfo.storageType = storageType;
        }

        private void OnEndDragItem(int slotIndex, ItemStorageType storageType)
        {
            FillSlotUI(storageType, slotIndex, GetStorageByType(storageType).GetItemBySlot(slotIndex));
            _draggedItemInfo.slotIndex = null;
        }

        private void OnDropItemToSlot(int dropSlotIndex, ItemStorageType dropStorageType)
        {
            if (_draggedItemInfo.slotIndex.HasValue)
            {
                SwapItems
                    (GetStorageByType(dropStorageType), dropSlotIndex,
                    GetStorageByType(_draggedItemInfo.storageType), _draggedItemInfo.slotIndex.Value);

                switch (dropStorageType)
                {
                    case ItemStorageType.GeneralInventory:
                        _selected.GeneralInventorySlotIndex = dropSlotIndex;
                        break;

                    default:
                        break;
                }
            }
        }

         private void OnDropItemOn3DViewModelRawImage()
        {

            if (_draggedItemInfo.slotIndex.HasValue)
            {
                EquipSelectedCharacterWithClothes(GetStorageByType(_draggedItemInfo.storageType), _draggedItemInfo.slotIndex.Value);
            }
        }

        #endregion

        #region OnSelected

        private void OnSelectedInventorySlot(int? previousSlotIndex)
        {
            if (previousSlotIndex.HasValue)
            {
                _generalInventorySlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }

            if (_selected.GeneralInventorySlotIndex.HasValue)
            {
                _generalInventorySlotsUIBehaviours[_selected.GeneralInventorySlotIndex.Value].SelectFrameSwitcher(true);

                if (_tradePanel.activeSelf)
                {
                    if (_generalInventory.GetItemBySlot(_selected.GeneralInventorySlotIndex.Value) != null)
                    {
                        _sellingItemPrice.text = HubUIServices.SharedInstance.ShopService.
                            CountSellPrice(_generalInventory.GetItemBySlot(_selected.GeneralInventorySlotIndex.Value)).ToString();
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

        private void OnSelectedBuyBackSlot(int? previousSlotIndex)
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
                    BaseItemModel item = (_selected.MapObject as CityModel).BuyBackStorage.
                        GetItemBySlot(_selected.BuyBackSlotIndex.Value);

                    if (item != null)
                    {
                        _buybackItemPrice.text = HubUIServices.SharedInstance.ShopService.CountSellPrice(item).ToString();
                        _buyBackButton.interactable = _player.GoldAmount >= HubUIServices.SharedInstance.ShopService.CountSellPrice(item);
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

        private void OnSelectedShopSlot(int? previousSlotIndex)
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
                    BaseItemModel item = (_selected.MapObject as CityModel).ShopStorage.
                        GetItemBySlot(_selected.ShopSlotIndex.Value);

                    if (item != null)
                    {
                        _buyingItemPrice.text = HubUIServices.SharedInstance.ShopService.GetItemPrice(item).ToString();
                        _buyButton.interactable = HubUIServices.SharedInstance.ShopService.IsPossibleToBuyShopItem(item, _selected.MapObject as CityModel);
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

        private void OnSelectedCharacter(CharacterModel previousCharacter)
        {
            if (previousCharacter != null)
            {
                previousCharacter.Behaviour.SelectFrameSwitch(false);

                previousCharacter.Backpack.OnPutItemToSlotHandler -= FillSlotUI;
                previousCharacter.Backpack.OnTakeItemFromSlotHandler -= FillSlotUI;

                previousCharacter.ClothesEquipment.OnPutItemToSlotHandler -= FillSlotUI;
                previousCharacter.ClothesEquipment.OnTakeItemFromSlotHandler -= FillSlotUI;

                previousCharacter.WeaponEquipment.OnPutItemToSlotHandler -= FillSlotUI;
                previousCharacter.WeaponEquipment.OnTakeItemFromSlotHandler -= FillSlotUI;
                previousCharacter.WeaponEquipment.OnTwoHandedWeaponHandler -= FillWeaponSlotUIAsSecondary;

                previousCharacter.Pockets.OnPutItemToSlotHandler -= FillSlotUI;
                previousCharacter.Pockets.OnTakeItemFromSlotHandler -= FillSlotUI;
                previousCharacter.Pockets.OnChangeSlotsAmountHandler -= OnChangedPocketsSlotsAmount;

                DestroyPocketsSlotsUI();

                previousCharacter.View3DModelObjectOnScene.SetActive(false);
                previousCharacter.View3DModelObjectOnScene.transform.rotation = Quaternion.identity;
            }
            else
            {
                if (_selected.Character != null)
                {
                    SetStorageSlotsInteractable(true, _characterBackpuckSlotsUIBehaviours);
                    SetStorageSlotsInteractable(true, _characterClothesSlotsUIBehaviours);
                    SetStorageSlotsInteractable(true, _characterWeaponSlotsUIBehaviours);
                }
            }

            if (_selected.Character != null)
            {
                _selected.Character.Behaviour.SelectFrameSwitch(true);
                _hikeAcceptButton.interactable = true;

                FillItemStorageSlots(ItemStorageType.CharacterBackpuck);
                _selected.Character.Backpack.OnPutItemToSlotHandler += FillSlotUI;
                _selected.Character.Backpack.OnTakeItemFromSlotHandler += FillSlotUI;

                FillItemStorageSlots(ItemStorageType.ClothesEquipment);
                _selected.Character.ClothesEquipment.OnPutItemToSlotHandler += FillSlotUI;
                _selected.Character.ClothesEquipment.OnTakeItemFromSlotHandler += FillSlotUI;

                FillItemStorageSlots(ItemStorageType.WeaponEquipment);
                _selected.Character.WeaponEquipment.OnPutItemToSlotHandler += FillSlotUI;
                _selected.Character.WeaponEquipment.OnTakeItemFromSlotHandler += FillSlotUI;
                _selected.Character.WeaponEquipment.OnTwoHandedWeaponHandler += FillWeaponSlotUIAsSecondary;

                InitializePocketsSlotsUI(_selected.Character.Pockets.GetSlotsCount());
                _selected.Character.Pockets.OnPutItemToSlotHandler += FillSlotUI;
                _selected.Character.Pockets.OnTakeItemFromSlotHandler += FillSlotUI;
                _selected.Character.Pockets.OnChangeSlotsAmountHandler += OnChangedPocketsSlotsAmount;

                _selected.Character.View3DModelObjectOnScene.SetActive(true);
                _character3DViewModelRawImage.enabled = true;
                _character3DViewModelRawImageBehaviour.RotateObject = _selected.Character.View3DModelObjectOnScene;
            }
            else
            {
                _character3DViewModelRawImage.enabled = false;
                _hikeAcceptButton.interactable = false;
                SetStorageSlotsInteractable(false, _characterBackpuckSlotsUIBehaviours);
                SetStorageSlotsInteractable(false, _characterClothesSlotsUIBehaviours);
                SetStorageSlotsInteractable(false, _characterWeaponSlotsUIBehaviours);
            }
        }

        private void OnSelectedMapObject(MapObjectModel previousMapObject)
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

        #region OnChanged

        private void OnChangedPocketsSlotsAmount()
        {
            DestroyPocketsSlotsUI();
            InitializePocketsSlotsUI(_selected.Character.Pockets.GetSlotsCount());
        }

        #endregion

        #region InitializeUIElements

        private void InitializePocketsSlotsUI(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject slotUI = InstantiateUIObject(_data.CharacterBackpuckSlotUIPrefab, _pocketSlotsPanel);
                MapEquipmentSlotBehaviour slotBehaviour = slotUI.GetComponent<MapEquipmentSlotBehaviour>();
                slotBehaviour.FillSlotInfo(i, true, _data.PocketItemSlotIcon);
                slotBehaviour.FillSlot(_selected.Character.Pockets.GetItemIconBySlot(i));
                slotBehaviour.OnBeginDragItemHandler = (slotIndex) => OnBeginDragItemFromSlot(slotIndex, ItemStorageType.PocketsStorage);
                slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, ItemStorageType.PocketsStorage);
                slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, ItemStorageType.PocketsStorage);
                slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.PocketsStorage);
                slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;
                _characterPocketsSlotsBehaviours.Add(slotBehaviour);
            }
        }

        private void DestroyPocketsSlotsUI()
        {
            for (int i = 0; i < _characterPocketsSlotsBehaviours.Count; i++)
            {
                Destroy(_characterPocketsSlotsBehaviours[i].gameObject);
            }
            _characterPocketsSlotsBehaviours.Clear();
        }

        private void InitializeCharacterUI(CharacterModel character)
        {
            GameObject characterUI = InstantiateUIObject(_data.CharacterUIPrefab, _charactersPanelFillable);
            MapCharacterBehaviour behaviourUI = characterUI.GetComponentInChildren<MapCharacterBehaviour>();
            behaviourUI.Initialize(character);
            behaviourUI.OnClick_ButtonHandler += OnClick_CharacterButton;

            character.InitializeView3DModel(_character3DViewModelRendering.transform);
        }

        private void InitializeCharacterBackpuckSlotUI(int slotIndex)
        {
            GameObject equipSlotUI = InstantiateUIObject(_data.EquipmentSlotUIPrefab, _characterInventoryPanel);

            MapStorageSlotBehaviour slotBehaviour = equipSlotUI.GetComponent<MapStorageSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, true);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnDoubleClickButtonHandler = (slotIndex) => OnDoubleClick_StorageSlot(slotIndex, ItemStorageType.CharacterBackpuck);
            slotBehaviour.OnBeginDragItemHandler = (slotIndex) => OnBeginDragItemFromSlot(slotIndex, ItemStorageType.CharacterBackpuck);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, ItemStorageType.CharacterBackpuck);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, ItemStorageType.CharacterBackpuck);
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.CharacterBackpuck);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _characterBackpuckSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeGeneralInventorySlotUI(int slotIndex)
        {
            GameObject inventorySlotUI = InstantiateUIObject(_data.InventorySlotUIPrefab, _inventoryItemsPanel);

            MapStorageSlotBehaviour slotBehaviour = inventorySlotUI.GetComponent<MapStorageSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, true);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_InventorySlot;
            slotBehaviour.OnDoubleClickButtonHandler = (slotIndex) => OnDoubleClick_StorageSlot(slotIndex, ItemStorageType.GeneralInventory);
            slotBehaviour.OnBeginDragItemHandler = (slotIndex) => OnBeginDragItemFromSlot(slotIndex, ItemStorageType.GeneralInventory);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, ItemStorageType.GeneralInventory);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, ItemStorageType.GeneralInventory);
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.GeneralInventory);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _generalInventorySlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeBuyBackSlotUI(int slotIndex)
        {
            GameObject buyBackSlotUI = InstantiateUIObject(_data.ShopSlotUIPrefab, _buyBackItemsPanel);

            MapStorageSlotBehaviour slotBehaviour = buyBackSlotUI.GetComponent<MapStorageSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, false);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_BuyBackSlot;
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.BuyBackStorage);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _buyBackSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeShopSlotUI(int slotIndex)
        {
            GameObject shopSlotUI = InstantiateUIObject(_data.ShopSlotUIPrefab, _buyingItemsPanel);

            MapShopSlotBehaviour slotBehaviour = shopSlotUI.GetComponent<MapShopSlotBehaviour>();
            slotBehaviour.FillSlotInfo(slotIndex, false);
            slotBehaviour.SetInteractable(false);
            slotBehaviour.OnPointerDownHandler = OnPointerDown_ShopSlot;
            slotBehaviour.OnPointerEnterHandler = (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.ShopStorage);
            slotBehaviour.OnPointerExitHandler = OnPointerExit_Slot;

            _shopSlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeCitizenUI(CitizenModel citizen)
        {
            GameObject citizenUI = InstantiateUIObject(_data.CitizenUIPrefab, _citizenPanel);

            MapCitizenBehaviour citizenUIBehaviour = citizenUI.GetComponentInChildren<MapCitizenBehaviour>();
            citizenUIBehaviour.FillCitizenInfo(citizen);
            citizenUIBehaviour.OnClick_CitizenButtonHandler = OnClick_CitizenButton;

            citizen.OnChangeQuestMarkerTypeHandler += citizenUIBehaviour.SetQuestMarker;

            _rightInfoPanelObjectsForDestroy.Add(citizenUI);
            _displayedCurrentCitizensUIBehaviours.Add(citizen, citizenUIBehaviour);
        }

        private void InitializeDialogAnswerButton(CitizenModel citizen, DialogAnswer answer)
        {
            GameObject answerButton = InstantiateUIObject(_data.AnswerButtonUIPrefab, _answerButtonsPanel);
            answerButton.GetComponentInChildren<Text>().text = answer.Text;

            if (answer.IsDialogEnd)
            {
                answerButton.GetComponentInChildren<Text>().text += " (уйти)";
            }

            answerButton.GetComponentInChildren<Button>().interactable = answer.IsInteractable;
            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnClick_AnswerButton(citizen, answer));
            _displayedDialogAnswerButtons.Add(answerButton);
        }

        #endregion

        #region FillUIElementsByInfo

        private void FillCharacterClothesSlot(int slotIndex)
        {
            Sprite slotSprite = _data.GetClothSlotSpriteByType(_context.CharactersClothEquipment[slotIndex]);
            _characterClothesSlotsUIBehaviours[slotIndex].FillSlotInfo(slotIndex, true, slotSprite);
            _characterClothesSlotsUIBehaviours[slotIndex].SetInteractable(false);
            _characterClothesSlotsUIBehaviours[slotIndex].OnDoubleClickButtonHandler = (slotIndex) => OnDoubleClick_CharacterEquipmentSlot(slotIndex, ItemStorageType.ClothesEquipment);
            _characterClothesSlotsUIBehaviours[slotIndex].OnBeginDragItemHandler += (slotIndex) => OnBeginDragItemFromSlot(slotIndex, ItemStorageType.ClothesEquipment);
            _characterClothesSlotsUIBehaviours[slotIndex].OnEndDragItemHandler += (slotIndex) => OnEndDragItem(slotIndex, ItemStorageType.ClothesEquipment);
            _characterClothesSlotsUIBehaviours[slotIndex].OnDroppedItemHandler += (slotIndex) => OnDropItemToSlot(slotIndex, ItemStorageType.ClothesEquipment);
            _characterClothesSlotsUIBehaviours[slotIndex].OnPointerEnterHandler += (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.ClothesEquipment);
            _characterClothesSlotsUIBehaviours[slotIndex].OnPointerExitHandler += OnPointerExit_Slot;
        }

        private void FillWeaponSlot(int slotIndex)
        {
            Sprite slotSprite = _data.WeaponSlotIcon;
            _characterWeaponSlotsUIBehaviours[slotIndex].FillSlotInfo(slotIndex, true, slotSprite);
            _characterWeaponSlotsUIBehaviours[slotIndex].SetInteractable(false);
            _characterWeaponSlotsUIBehaviours[slotIndex].OnDoubleClickButtonHandler = (slotIndex) => OnDoubleClick_CharacterEquipmentSlot(slotIndex, ItemStorageType.WeaponEquipment);
            _characterWeaponSlotsUIBehaviours[slotIndex].OnBeginDragItemHandler += (slotIndex) => OnBeginDragItemFromSlot(slotIndex, ItemStorageType.WeaponEquipment);
            _characterWeaponSlotsUIBehaviours[slotIndex].OnEndDragItemHandler += (slotIndex) => OnEndDragItem(slotIndex, ItemStorageType.WeaponEquipment);
            _characterWeaponSlotsUIBehaviours[slotIndex].OnDroppedItemHandler += (slotIndex) => OnDropItemToSlot(slotIndex, ItemStorageType.WeaponEquipment);
            _characterWeaponSlotsUIBehaviours[slotIndex].OnPointerEnterHandler += (slotIndex) => OnPointerEnter_Slot(slotIndex, ItemStorageType.WeaponEquipment);
            _characterWeaponSlotsUIBehaviours[slotIndex].OnPointerExitHandler += OnPointerExit_Slot;
        }

        private void FillMapObject(GameObject mapObject, MapObjectData mapObjectdata)
        {
            MapObjectModel mapObjectModel = _context.GetMapObjectModel(mapObjectdata);

            if (mapObject != null)
            {
                MapObjectBehaviour mapObjectBehaviour = mapObject.GetComponent<MapObjectBehaviour>();
                mapObjectModel.Behaviour = mapObjectBehaviour;

                switch (mapObjectdata.GetMapObjectType())
                {
                    case MapObjectType.Location:

                        mapObjectBehaviour.FillInfo(mapObjectModel as LocationModel);
                        mapObjectBehaviour.OnClick_ButtonHandler += OnClick_LocationButton;

                        break;
                    case MapObjectType.City:

                        mapObjectBehaviour.FillInfo(mapObjectModel as CityModel);
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

        private void FillTooltipByItemInfo(int slotIndex, ItemStorageType storageType)
        {
            BaseItemModel item = GetStorageByType(storageType).GetItemBySlot(slotIndex);

            if (item != null)
            {
                Text[] tooltipTexts = _tooltip.GetComponentsInChildren<Text>(true);

                tooltipTexts[0].gameObject.SetActive(false);
                tooltipTexts[1].text = item.Name;
                tooltipTexts[2].text = item.Description;
                tooltipTexts[3].text = "Цена: " + HubUIServices.SharedInstance.ShopService.GetItemPrice(item).ToString();

                if (storageType == ItemStorageType.ShopStorage)
                {
                    if(!HubUIServices.SharedInstance.ShopService.IsPossibleToBuyShopItem(item, _selected.MapObject as CityModel, out string message))
                    {
                        tooltipTexts[0].gameObject.SetActive(true);
                        tooltipTexts[0].text = message;
                    }
                }
            }
        }

        private void FillSlotUI(ItemStorageType storageType, int slotIndex, BaseItemModel item)
        {
            BaseItemStorage storage = GetStorageByType(storageType);
            Sprite sprite = storage.GetItemIconBySlot(slotIndex);

            switch (storageType)
            {
                case ItemStorageType.CharacterBackpuck:

                    _characterBackpuckSlotsUIBehaviours[slotIndex].FillSlot(sprite);

                    break;
                case ItemStorageType.GeneralInventory:

                    _generalInventorySlotsUIBehaviours[slotIndex].FillSlot(sprite);

                    break;
                case ItemStorageType.BuyBackStorage:

                    _buyBackSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    _buyBackSlotsUIBehaviours[slotIndex].SetInteractable(sprite != null);

                    break;
                case ItemStorageType.ShopStorage:

                    _shopSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    _shopSlotsUIBehaviours[slotIndex].SetInteractable(sprite != null);
                    _shopSlotsUIBehaviours[slotIndex].SetAvailability(HubUIServices.SharedInstance.ShopService.IsPossibleToBuyShopItem
                        (storage.GetItemBySlot(slotIndex), _selected.MapObject as CityModel));

                    break;
                case ItemStorageType.ClothesEquipment:

                    _characterClothesSlotsUIBehaviours[slotIndex].FillSlot(sprite);

                    break;
                case ItemStorageType.PocketsStorage:

                    _characterPocketsSlotsBehaviours[slotIndex].FillSlot(sprite);

                    break;
                case ItemStorageType.WeaponEquipment:

                    _characterWeaponSlotsUIBehaviours[slotIndex].FillSlot(sprite);

                    break;
                default:

                    Debug.LogError(this + ": incorrect StorageSlotType");

                    break;
            }
        }

        private void FillWeaponSlotUIAsSecondary(int slotIndex, Sprite sprite)
        {
            _characterWeaponSlotsUIBehaviours[slotIndex].FillSlotAsSecondary(sprite);
        }

        private void FillItemStorageSlots(ItemStorageType storageType)
        {
            BaseItemStorage storage = GetStorageByType(storageType);

            for (int i = 0; i < storage.GetSlotsCount(); i++)
            {
                FillSlotUI(storageType, i, storage.GetItemBySlot(i));
            }

            if (storageType == ItemStorageType.WeaponEquipment)
            {
                for (int i = 1; i < storage.GetSlotsCount(); i += 2)
                {
                    WeaponItemModel adjacentWeapon = (storage as EquippedWeaponStorage).AdjacentWeapon(i);
                    if (adjacentWeapon != null && adjacentWeapon.IsTwoHanded)
                    {
                        FillWeaponSlotUIAsSecondary(i, adjacentWeapon.Icon);
                    }
                }
            }
        }

        private void FillCityPanel(CityModel city)
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

        private void FillLocationPanel(LocationModel location)
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

        private void FillDialogPanel(CitizenModel citizen)
        {
            _citizenName.text = citizen.Name;
            _citizenPortrait.sprite = citizen.Portrait;
            _dialogText.text = citizen.CurrentDialog.Text;

            List<DialogAnswer> answers = citizen.GetAllCurrentAnswers();
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
            foreach (KeyValuePair<CitizenModel, MapCitizenBehaviour> kvp in _displayedCurrentCitizensUIBehaviours)
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

        private void MoveItemToGeneralInventoryStorage(BaseItemStorage outStorage, int outStorageSlotIndex)
        {
            BaseItemModel item = outStorage.GetItemBySlot(outStorageSlotIndex);
            if (item != null)
            {
                if (_generalInventory.PutItemToFirstEmptySlot(item))
                {
                    outStorage.RemoveItem(outStorageSlotIndex);
                }
                else
                {
                    HubUIServices.SharedInstance.GameMessages.Notice("Inventory is full");
                }
            }
        }

        private void EquipSelectedCharacterWithWeapon(BaseItemStorage outStorage, int outStorageSlotIndex)
        {
            BaseItemModel itemInClickedSlot = outStorage.GetItemBySlot(outStorageSlotIndex);
            if (itemInClickedSlot != null &&
                itemInClickedSlot.ItemType == ItemType.Weapon &&
                _selected.Character != null)
            {
                EquippedWeaponStorage weaponEquipmentStorage = _selected.Character.WeaponEquipment;
                if (!weaponEquipmentStorage.PutItemToFirstEmptySlot(itemInClickedSlot))
                {
                    HubUIServices.SharedInstance.GameMessages.Notice("There is no free slots for this weapon");
                }
                else
                {
                    outStorage.RemoveItem(outStorageSlotIndex);
                }

            }
        }

        private void EquipSelectedCharacterWithClothes(BaseItemStorage outStorage, int outStorageSlotIndex)
        {
            BaseItemModel itemInClickedSlot = outStorage.GetItemBySlot(outStorageSlotIndex);
            if (itemInClickedSlot != null &&
                itemInClickedSlot.ItemType == ItemType.Clothes &&
                _selected.Character != null)
            {
                EquippedClothesStorage clothesEquipmentStorage = _selected.Character.ClothesEquipment;
                if (!clothesEquipmentStorage.PutItemToFirstEmptySlot(itemInClickedSlot))
                {
                    int? firstSlot = clothesEquipmentStorage.GetFirstSlotIndexForItem(itemInClickedSlot as ClothesItemModel);
                    SwapItems(clothesEquipmentStorage, firstSlot.Value, outStorage, outStorageSlotIndex);
                }
                else
                {
                    outStorage.RemoveItem(outStorageSlotIndex);
                }

            }
        }

        private void SetStorageSlotsInteractable<T>(bool flag, IEnumerable<T> slotBehaviours) where T : MapBaseSlotBehaviour
        {
            foreach (MapBaseSlotBehaviour slot in slotBehaviours)
            {
                slot.SetInteractable(flag);
            }
        }

        private BaseItemStorage GetStorageByType(ItemStorageType storageType)
        {
            BaseItemStorage storage = null;
            switch (storageType)
            {
                case ItemStorageType.CharacterBackpuck:
                    storage = _selected.Character.Backpack;
                    break;

                case ItemStorageType.GeneralInventory:
                    storage = _generalInventory;
                    break;

                case ItemStorageType.BuyBackStorage:
                    storage = (_selected.MapObject as CityModel).BuyBackStorage;
                    break;

                case ItemStorageType.ShopStorage:
                    storage = (_selected.MapObject as CityModel).ShopStorage;
                    break;

                case ItemStorageType.ClothesEquipment:
                    storage = _selected.Character.ClothesEquipment;
                    break;

                case ItemStorageType.PocketsStorage:
                    storage = _selected.Character.Pockets;
                    break;

                case ItemStorageType.WeaponEquipment:
                    storage = _selected.Character.WeaponEquipment;
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

        private void SwapItems(BaseItemStorage inStorage, int inStorageSlotIndex, BaseItemStorage outStorage, int outStorageSlotIndex)
        {
            BaseItemModel outStorageItem = outStorage.GetItemBySlot(outStorageSlotIndex);
            BaseItemModel inStorageItem = inStorage.GetItemBySlot(inStorageSlotIndex);
            bool isSuccefulTakeItems = false;
            bool isSuccefullPutItems = false;


            if (inStorage.RemoveItem(inStorageSlotIndex))
            {
                if (outStorage.RemoveItem(outStorageSlotIndex))
                {
                    isSuccefulTakeItems = true;
                }
                else
                {
                    inStorage.PutItem(inStorageSlotIndex, inStorageItem);
                }
            }

            if (isSuccefulTakeItems)
            {
                isSuccefullPutItems =
                    inStorage.PutItem(inStorageSlotIndex, outStorageItem) &&
                    outStorage.PutItem(outStorageSlotIndex, inStorageItem);

                if (!isSuccefullPutItems)
                {
                    inStorage.RemoveItem(inStorageSlotIndex);
                    inStorage.PutItem(inStorageSlotIndex, inStorageItem);

                    outStorage.RemoveItem(outStorageSlotIndex);
                    outStorage.PutItem(outStorageSlotIndex, outStorageItem);
                }
            }
           
            if (!(isSuccefulTakeItems && isSuccefullPutItems))
            {
                Debug.LogWarning("Drag and drop swap items operation was not successful");
            }
        }

        private void ChangePlayerGoldAmount(int goldAmount)
        {
            _player.GoldAmount += goldAmount;
            _playerGoldAmount.text = _player.GoldAmount.ToString();

            for (int i = 0; i < _shopSlotsUIBehaviours.Count; i++)
            {
                CityModel selectedCity = _selected.MapObject as CityModel;
                _shopSlotsUIBehaviours[i].SetAvailability
                    (HubUIServices.SharedInstance.ShopService.IsPossibleToBuyShopItem(selectedCity.ShopStorage.GetItemBySlot(i), selectedCity));
            }
        }

         private void LocationLoad()
         {
            Debug.Log("Load location. ID: " + (_selected.MapObject as LocationModel).LoadSceneId);
            SceneManager.LoadScene((_selected.MapObject as LocationModel).LoadSceneId);
         }

        #endregion
    }
}
