using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUIBehaviour : MonoBehaviour
    {
        #region PrivateData

        private enum StorageSlotType
        {
            None = 0,
            Equipment = 1,
            Inventory = 2,
        }

        private class SelectedElements
        {
            private int? _equipmentSlotIndex;
            private int? _inventorySlotIndex;
            private HubMapUICharacter _character;

            public Action<int?> OnChanged_EquipmentSlotIndex { get; set; }
            public Action<int?> OnChanged_InventorySlotIndex { get; set; }
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

        [Header("City info panel")]
        [SerializeField] private GameObject _cityInfoPanel;
        [SerializeField] private GameObject _citizenPanel;
        [SerializeField] private GameObject _citySellingPanel;
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
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _inventoryItemsPanel;
        [SerializeField] private Button _closeInventoryButton;
        [SerializeField] private Scrollbar _charactersPanelScrollbar;
        [SerializeField] private Button _charactersPanelNextButton;
        [SerializeField] private Button _charactersPanelPreviousButton;
        [SerializeField] private Button _perkTreeButton;

        private List<GameObject> _rightInfoPanelObjectsForDestroy;
        private Dictionary<HubMapUICitizen, GameObject> _displayedCurrentCitizensUI;
        private List<HubMapUISlotBehaviour> _equipmentSlotsUIBehaviours;
        private List<HubMapUISlotBehaviour> _inventorySlotsUIBehaviours;
        private List<HubMapUICharacterBehaviour> _charactersUIBehaviours;
        private List<GameObject> _displayedDialogAnswerButtons;
        private (int? slotIndex, StorageSlotType slotType) _draggedItemSlot;
        private HubMapUILocation _selectedLocation;
        private HubMapUIStorage _inventory;
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

            _selected = new SelectedElements();
            _selected.OnChanged_InventorySlotIndex = OnChangedSelectedInventorySlotIndex;
            _selected.OnChanged_EquipmentSlotIndex = OnChangedSelectedEquipmentSlotIndex;
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

            _selected.RemoveAllListeners();
        }

        private void Start()
        {
            _inventory = new HubMapUIStorage(Data.HubMapData.InventorySlotsAmount);
            for (int i = 0; i < Data.HubMapData.StartInventoryItems.Length; i++)
            {
                _inventory.PutItem(i, Data.HubMapData.StartInventoryItems[i]);
            }
            FillItemsSlots(_inventory.GetItemsOnly(), StorageSlotType.Inventory);
            _inventory.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageSlotType.Inventory);

            _displayedCurrentCitizensUI = new Dictionary<HubMapUICitizen, GameObject>();
            _rightInfoPanelObjectsForDestroy = new List<GameObject>();
            _displayedDialogAnswerButtons = new List<GameObject>();

            _mainPanel.SetActive(Data.HubMapData.MapOnStartEnabled);
            _infoPanel.SetActive(false);
            _cityInfoPanel.SetActive(false);
            _dialogPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _hikePanel.SetActive(false);
            _hikePreparePanel.SetActive(true);
            _inventoryPanel.SetActive(false);
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
            SetEquipmentSlotsInteractable(false);
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

            _inventoryPanel.SetActive(false);
            _hikePanel.SetActive(false);
        }

        private void OnClick_CityButton(HubMapUICity city)
        {
            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillCityPanel(city);
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
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
            _inventoryPanel.SetActive(false);
        }

        private void OnClick_PerkTreeButton()
        {
            //todo: show perk tree UI
        }

        private void OnClick_CharacterButton(HubMapUICharacter character)
        {
            _selected.Character = character;
            FillItemsSlots(character.Backpack.GetAll(), StorageSlotType.Equipment);
            SetEquipmentSlotsInteractable(true);
        }

        private void OnClick_InventorySlot(int slotIndex)
        {
            _selected.InventorySlotIndex = slotIndex;
        }

        private void OnClick_EquipmentSlot(int slotIndex)
        {
            _selected.EquipmentSlotIndex = slotIndex;

            if (!_inventoryPanel.activeSelf)
            {
                _inventoryPanel.SetActive(true);
            }
        }

        private void OnDoubleClick_InventorySlot(int slotIndex)
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
                bool isFoundEmptySlot = false;

                for (int i = 0; i < _selected.Character.Backpack.GetSlotsCount(); i++)
                {
                    if (_selected.Character.Backpack.GetItemBySlot(i) == null)
                    {
                        _selected.Character.Backpack.PutItem(i, inventoryItem);
                        isFoundEmptySlot = true;
                        break;
                    }
                }

                if (!isFoundEmptySlot)
                {
                    _inventory.PutItem(slotIndex, inventoryItem);
                    Debug.Log("Equipment is full");
                }
            }
        }

        private void OnDoubleClick_EquipmentSlot(int slotIndex)
        {
            BaseItem equipmentItem = _selected.Character.Backpack.TakeItem(slotIndex);

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
                foreach (KeyValuePair<HubMapUICitizen, GameObject> kvp in _displayedCurrentCitizensUI)
                {
                    UpdateCitizenInfo(kvp.Key);
                }
            }
            else
            {
                FillDialogPanel(citizen);
            }
        }

        private void OnDragItemFromSlot(int slotIndex, StorageSlotType slotType)
        {
            _draggedItemSlot.slotIndex = slotIndex;
            _draggedItemSlot.slotType = slotType;
        }

        private void OnEndDragItem(int slotIndex, StorageSlotType slotType)
        {
            FillSlotUI(slotIndex, GetStorageByType(slotType).GetItemIconBySlot(slotIndex), slotType);
            _draggedItemSlot.slotIndex = null;
        }

        private void OnDropItemToSlot(int dropSlotIndex, StorageSlotType dropSlotType)
        {
            if (_draggedItemSlot.slotIndex.HasValue)
            {
                HubMapUIStorage dropItemStorage = GetStorageByType(dropSlotType); ;
                HubMapUIStorage takeItemStorage = GetStorageByType(_draggedItemSlot.slotType);

                BaseItem draggedItem = takeItemStorage.TakeItem(_draggedItemSlot.slotIndex.Value);
                BaseItem droppedSlotItem = dropItemStorage.TakeItem(dropSlotIndex);

                dropItemStorage.PutItem(dropSlotIndex, draggedItem);
                takeItemStorage.PutItem(_draggedItemSlot.slotIndex.Value, droppedSlotItem);
            }
        }

        private void OnChangedSelectedInventorySlotIndex(int? previousSlotIndex)
        {
            if (previousSlotIndex.HasValue)
            {
                _inventorySlotsUIBehaviours[previousSlotIndex.Value].SelectFrameSwitcher(false);
            }
            if (_selected.InventorySlotIndex.HasValue)
            {
                _inventorySlotsUIBehaviours[_selected.InventorySlotIndex.Value].SelectFrameSwitcher(true);
            }
        }

        private void OnChangedSelectedEquipmentSlotIndex(int? previousSlotIndex)
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

        private void OnChangedSelectedCharacter(HubMapUICharacter previousCharacter)
        {
            if (previousCharacter != null)
            {
                previousCharacter.Backpack.OnChangeItemHandler = null;
            }
            if (_selected.Character != null)
            {
                _selected.Character.Backpack.OnChangeItemHandler = (slotIndex, sprite) => FillSlotUI(slotIndex, sprite, StorageSlotType.Equipment);
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
            slotBehaviour.OnClick_SlotButtonHandler = OnClick_EquipmentSlot;
            slotBehaviour.OnDoubleClickButtonHandler = OnDoubleClick_EquipmentSlot;
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageSlotType.Equipment);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageSlotType.Equipment);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageSlotType.Equipment);
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
            slotBehaviour.OnDraggedItemHandler = (slotIndex) => OnDragItemFromSlot(slotIndex, StorageSlotType.Inventory);
            slotBehaviour.OnDroppedItemHandler = (slotIndex) => OnDropItemToSlot(slotIndex, StorageSlotType.Inventory);
            slotBehaviour.OnEndDragItemHandler = (slotIndex) => OnEndDragItem(slotIndex, StorageSlotType.Inventory);
            _inventorySlotsUIBehaviours.Add(slotBehaviour);
        }

        private void InitializeSellingItemUI(BaseItem item, float playerCityReputation)
        {
            GameObject itemUI = GameObject.Instantiate(Data.HubMapData.SellingItemUIPrefab);
            itemUI.transform.SetParent(_citySellingPanel.transform, false);
            itemUI.transform.localScale = new Vector3(1, 1, 1);

            HubMapUISellingItemBehaviour sellingItemUIBehaviour = itemUI.GetComponentInChildren<HubMapUISellingItemBehaviour>();
            sellingItemUIBehaviour.FillSellingItemInfo(item, playerCityReputation >= item.ItemStruct.RequiredReputationForSale);

            _rightInfoPanelObjectsForDestroy.Add(itemUI);
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
            _displayedCurrentCitizensUI.Add(citizen, citizenUI);
        }

        private void InitializeDialogAnswerButton(HubMapUICitizen citizen, HubMapUIDialogAnswer answer)
        {
            GameObject answerButton = GameObject.Instantiate(Data.HubMapData.AnswerButtonUIPrefab);
            answerButton.transform.SetParent(_answerButtonsPanel.transform, false);
            answerButton.transform.localScale = new Vector3(1, 1, 1);
            answerButton.GetComponentInChildren<Text>().text = answer.Text;

            answerButton.GetComponentInChildren<Button>().onClick.AddListener(() => OnClick_DialogButton(citizen, answer));
            _displayedDialogAnswerButtons.Add(answerButton);
        }

        #endregion


        #region FillUIElementsByInfo

        private void FillSlotUI(int slotIndex, Sprite sprite, StorageSlotType slotType)
        {
            switch (slotType)
            {
                case StorageSlotType.Equipment:
                    _equipmentSlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    break;
                case StorageSlotType.Inventory:
                    _inventorySlotsUIBehaviours[slotIndex].FillSlot(sprite);
                    break;
                default:
                    Debug.LogError(this + ": incorrect StorageSlotType");
                    break;
            }
        }

         private void FillItemsSlots(IEnumerable<BaseItem> items, StorageSlotType slotType)
        {
            int i = 0;
            foreach (BaseItem item in items)
            {
                if (item != null)
                {
                    FillSlotUI(i, item.ItemStruct.Icon, slotType);
                }
                else
                {
                    FillSlotUI(i, null, slotType);
                }
                i++;
                if (i >= GetStorageByType(slotType).GetSlotsCount())
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

            for (int i = 0; i < city.SellingItems.Length; i++)
            {
                InitializeSellingItemUI(city.SellingItems[i], cityReputation);
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
            foreach (KeyValuePair<HubMapUICitizen,GameObject> kvp in _displayedCurrentCitizensUI)
            {
                Data.HubMapData.QuestService.OnQuestIsActiveHandler -=
                    () => kvp.Value.GetComponentInChildren<HubMapUICitizenBehaviour>().UpdateInfo(kvp.Key);
            }

            for (int i=0; i< _rightInfoPanelObjectsForDestroy.Count; i++)
            {
                Destroy(_rightInfoPanelObjectsForDestroy[i]);
            }

            _rightInfoPanelObjectsForDestroy.Clear();
            _displayedCurrentCitizensUI.Clear();
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

        private HubMapUIStorage GetStorageByType(StorageSlotType slotType)
        {
            HubMapUIStorage storage = null;
            switch (slotType)
            {
                case StorageSlotType.Equipment:
                    storage = _selected.Character.Backpack;
                    break;
                case StorageSlotType.Inventory:
                    storage = _inventory;
                    break;
                default:
                    Debug.LogError(this + ": incorrect StorageSlotType");
                    break;
            }
            return storage;
        }

        private void UpdateCitizenInfo(HubMapUICitizen citizen)
        {
            _displayedCurrentCitizensUI[citizen].GetComponentInChildren<HubMapUICitizenBehaviour>().UpdateInfo(citizen);
        }

        private void LocationLoad()
        {
            Debug.Log("Load location. Location: " + _selectedLocation);
        }

        #endregion

    }
}
