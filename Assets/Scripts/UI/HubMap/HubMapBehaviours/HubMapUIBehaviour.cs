using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    public class HubMapUIBehaviour : MonoBehaviour
    {
        private const float CHARACTERS_PANEL_SCROLLBAR_SPEED = 1.0f;

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
        private List<GameObject> _displayedDialogAnswerButtons = new List<GameObject>();
        private int? _selectedEquipmentSlotIndex;
        private int? _selectedInventorySlotIndex;
        private (int? slotIndex, StorageSlotType slotType) _draggedItemSlot;
        private HubMapUICharacter _selectedCharacter;
        private HubMapUILocation _selectedLocation;
        private HubMapUIStorage _inventory;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _cityButton.onClick.AddListener(() => ShowCityInfoPanel(Data.HubMapData.City));
            _locationButton_1.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.Location_1));
            _locationButton_2.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.Location_2));
            _locationButton_3.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.Location_3));
            _locationButton_4.onClick.AddListener(() => ShowLocationInfoPanel(Data.HubMapData.Location_4));

            _hubButton.onClick.AddListener(HideMap);
            _mapButton.onClick.AddListener(ShowMap);
            _hideInfoPanelButton.onClick.AddListener(HideRightInfoPanels);
            _hikeAcceptButton.onClick.AddListener(LocationLoad);
            _closeHikePanelButton.onClick.AddListener(CloseHikePanel);
            _hikePanelButton.onClick.AddListener(ShowHikePanel);
            _charactersPanelNextButton.onClick.AddListener(()=>MoveCharactersPanelScrollbar(CHARACTERS_PANEL_SCROLLBAR_SPEED));
            _charactersPanelPreviousButton.onClick.AddListener(() => MoveCharactersPanelScrollbar(-CHARACTERS_PANEL_SCROLLBAR_SPEED));
            _closeInventoryButton.onClick.AddListener(CloseInventoryPanel);
            _perkTreeButton.onClick.AddListener(ShowPerkTreePanel);

            _charactersUIBehaviours = new List<HubMapUICharacterBehaviour>();
            for (int i = 0; i < Data.HubMapData.Characters.Count; i++)
            {
                CharacterUIInitialize(Data.HubMapData.Characters[i]);
            }

            _equipmentSlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            for (int i = 0; i < Data.HubMapData.CharactersEquipmentSlotsAmount; i++)
            {
                EquipmentSlotUIInitialize(i);
            }

            _inventorySlotsUIBehaviours = new List<HubMapUISlotBehaviour>();
            for (int i = 0; i < Data.HubMapData.InventorySlotsAmount; i++)
            {
                InventorySlotUIInitialize(i);
            }
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
        }

        private void Start()
        {
            _inventory = new HubMapUIStorage(Data.HubMapData.InventorySlotsAmount);
            for (int i = 0; i < Data.HubMapData.StartInventoryItems.Length; i++)
            {
                _inventory.PutItem(i, Data.HubMapData.StartInventoryItems[i]);
            }
            _inventory.OnChangeItemHandler = FillInventorySlotUI;
            FillInventorySlotsByItems(_inventory.GetItemsOnly());

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

        void FillInventorySlotUI(int slotIndex, Sprite sprite)
        {
            _inventorySlotsUIBehaviours[slotIndex].FillSlot(sprite);
        }

        void FillEquipmentSlotUI(int slotIndex, Sprite sprite)
        {
            _equipmentSlotsUIBehaviours[slotIndex].FillSlot(sprite);
        }

        private void InventorySlotUIInitialize(int slotIndex)
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

        private void CharacterUIInitialize(HubMapUICharacter character)
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

        private void EquipmentSlotUIInitialize(int slotIndex)
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

        private void ShowMap()
        {
            _mainPanel.SetActive(true);
        }

        private void HideMap()
        {
            _mainPanel.SetActive(false);
        }

        private void ShowInventoryPanel()
        {
            _inventoryPanel.SetActive(true);
        }

        private void CloseInventoryPanel()
        {
            _inventoryPanel.SetActive(false);
        }

        private void ShowHikePanel()
        {
            for (int i = 0; i < _equipmentSlotsUIBehaviours.Count; i++)
            {
                _equipmentSlotsUIBehaviours[i].FillSlot(null);
                _equipmentSlotsUIBehaviours[i].SetInteractable(false);
            }
            _hikePanel.SetActive(true);
        }

        private void CloseHikePanel()
        {
            _inventoryPanel.SetActive(false);
            _hikePanel.SetActive(false);
        }

        private void ShowCityInfoPanel(HubMapUICity city)
        {
            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillCityInfo(city);
            _infoPanel.GetComponent<ScrollRect>().content = _cityInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _cityInfoPanel.SetActive(true);
        }

        private void ShowLocationInfoPanel(HubMapUILocation location)
        {
            _selectedLocation = location;
            HideRightInfoPanels();
            ClearRightInfoPanel();
            FillLocationInfo(location);
            _infoPanel.GetComponent<ScrollRect>().content = _locationInfoPanel.GetComponent<RectTransform>();
            _infoPanel.SetActive(true);
            _locationInfoPanel.SetActive(true);
        }

        private void HideRightInfoPanels()
        {
            _cityInfoPanel.SetActive(false);
            _locationInfoPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }

        private void ShowDialogPanel(HubMapUICitizen citizen)
        {
            FillDialogInfo(citizen);
            _dialogPanel.SetActive(true);
        }

        private void HideDialogPanel()
        {
            _dialogPanel.SetActive(false);
        }

        private void ShowPerkTreePanel()
        {
            //todo: perk tree UI
        }

        private void FillInventorySlotsByItems(IEnumerable<BaseItem> items)
        {
            int i = 0;
            foreach (BaseItem item in items)
            {
                FillInventorySlotUI(i, item.ItemStruct.Icon);
                i++;
                if (i >= _inventorySlotsUIBehaviours.Count)
                { 
                    break;
                }
            }
        }

        private void OnClick_InventorySlot(int slotIndex)
        {
            if (_selectedInventorySlotIndex.HasValue)
            {
                _inventorySlotsUIBehaviours[_selectedInventorySlotIndex.Value].SelectFrameSwitcher(false);
            }
            _inventorySlotsUIBehaviours[slotIndex].SelectFrameSwitcher(true);
            _selectedInventorySlotIndex = slotIndex;
        }

        private void OnDoubleClick_InventorySlot(int slotIndex)
        {
            BaseItem inventoryItem = _inventory.TakeItem(slotIndex);

            if (_selectedEquipmentSlotIndex.HasValue)
            {
                BaseItem equipmentItem = _selectedCharacter.Backpack.TakeItem(_selectedEquipmentSlotIndex.Value);

                _inventory.PutItem(slotIndex, equipmentItem);
                _selectedCharacter.Backpack.PutItem(_selectedEquipmentSlotIndex.Value, inventoryItem);
            }
            else
            {
                bool isFoundEmptySlot = false;

                for (int i = 0; i < _selectedCharacter.Backpack.GetSlotsCount(); i++)
                {
                    if (_selectedCharacter.Backpack.GetItemBySlot(i) == null)
                    {
                        _selectedCharacter.Backpack.PutItem(i, inventoryItem);
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

        private void OnClick_EquipmentSlot(int slotIndex)
        {
            if (_selectedEquipmentSlotIndex.HasValue)
            {
                _equipmentSlotsUIBehaviours[_selectedEquipmentSlotIndex.Value].SelectFrameSwitcher(false);
            }
            _equipmentSlotsUIBehaviours[slotIndex].SelectFrameSwitcher(true);
            _selectedEquipmentSlotIndex = slotIndex;

            if (!_inventoryPanel.activeSelf)
            {
                ShowInventoryPanel();
            }
        }

        private void OnDoubleClick_EquipmentSlot(int slotIndex)
        {
            BaseItem equipmentItem = _selectedCharacter.Backpack.TakeItem(slotIndex);

            if (_selectedInventorySlotIndex.HasValue)
            {
                BaseItem inventoryItem = _inventory.TakeItem(_selectedInventorySlotIndex.Value);

                _inventory.PutItem(_selectedInventorySlotIndex.Value, equipmentItem);
                _selectedCharacter.Backpack.PutItem(slotIndex, inventoryItem);
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
                    _selectedCharacter.Backpack.PutItem(slotIndex, equipmentItem);
                    Debug.Log("Inventory is full");
                }
            }
        }

        private void OnDragItemFromSlot(int slotIndex, StorageSlotType slotType)
        {
            _draggedItemSlot.slotIndex = slotIndex;
            _draggedItemSlot.slotType = slotType;
        }

        private void OnEndDragItem(int slotIndex, StorageSlotType slotType)
        {
            switch (slotType)
            {
                case StorageSlotType.Equipment:
                    FillEquipmentSlotUI(slotIndex, _selectedCharacter.Backpack.GetItemIconBySlot(slotIndex));
                    break;
                case StorageSlotType.Inventory:
                    FillInventorySlotUI(slotIndex, _inventory.GetItemIconBySlot(slotIndex)); ;
                    break;
                default:
                    Debug.LogError(this + ": incorrect StorageSlotType");
                    break;
            }
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

        private HubMapUIStorage GetStorageByType(StorageSlotType slotType)
        {
            HubMapUIStorage storage = null;
            switch (slotType)
            {
                case StorageSlotType.Equipment:
                    storage = _selectedCharacter.Backpack;
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

        private void OnClick_CharacterButton(HubMapUICharacter character)
        {
            if (_selectedCharacter != null)
            {
                _selectedCharacter.Backpack.OnChangeItemHandler = null;
            }
            _selectedCharacter = character;
            FillCharacterEquipmentByItems(character.Backpack.GetAll());
            character.Backpack.OnChangeItemHandler = FillEquipmentSlotUI;
        }

        private void FillCharacterEquipmentByItems(BaseItem[] items)
        {
            int i = 0;
            foreach (BaseItem item in items)
            {
                FillEquipmentSlotUI(i, item?.ItemStruct.Icon);
                i++;
                if (i >= _equipmentSlotsUIBehaviours.Count)
                {
                    break;
                }
            }

            for (int j = 0; j < _equipmentSlotsUIBehaviours.Count; j++)
            {
                _equipmentSlotsUIBehaviours[j].SetInteractable(true);
            }
        }

        private void FillCityInfo(HubMapUICity city)
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
            citizenUIBehaviour.OnClick_CitizenButtonHandler = ShowDialogPanel;
            Data.HubMapData.QuestService.OnQuestIsActiveHandler += () => citizenUIBehaviour.UpdateInfo(citizen);

            _rightInfoPanelObjectsForDestroy.Add(citizenUI);
            _displayedCurrentCitizensUI.Add(citizen, citizenUI);
        }

        private void FillLocationInfo(HubMapUILocation location)
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

        private void UpdateCitizenInfo(HubMapUICitizen citizen)
        {
            _displayedCurrentCitizensUI[citizen].GetComponentInChildren<HubMapUICitizenBehaviour>().UpdateInfo(citizen);
        }

        private void FillDialogInfo(HubMapUICitizen citizen)
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
            if(additionalQuestAnswer != null)
            {
                InitializeDialogAnswerButton(citizen, additionalQuestAnswer);
            }
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
                HideDialogPanel();
                foreach (KeyValuePair<HubMapUICitizen,GameObject> kvp in _displayedCurrentCitizensUI)
                {
                    UpdateCitizenInfo(kvp.Key);
                }
            }
            else
            {
                FillDialogInfo(citizen);
            }
        }

        private void MoveCharactersPanelScrollbar(float step)
        {
            _charactersPanelScrollbar.value += step;
        }

        private void LocationLoad()
        {
            Debug.Log("Load location. Location: " + _selectedLocation);
        }

        #endregion


        private enum StorageSlotType
        {
            None = 0,
            Equipment = 1,
            Inventory = 2,
        }
    }
}
