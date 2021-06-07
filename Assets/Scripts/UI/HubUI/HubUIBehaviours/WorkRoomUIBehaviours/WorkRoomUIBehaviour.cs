using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomUIBehaviour : MonoBehaviour, IStart, IDestroy
    {
        #region Fields

        [SerializeField] private GameObject _roomButtonsPanel;
        [SerializeField] private GameObject _roomButtonsFillablePanel;
        [SerializeField] private GameObject _roomPanel;
        [SerializeField] private Text _roomNameText;
        [SerializeField] private Button _roomUpgradeButton;
        [SerializeField] private Button _roomCloseButton;
        [SerializeField] private WorkRoomWorkerSlotBehaviour _chiefSlotBehaviour;
        [SerializeField] private Image _chiefSkillLevelImage;
        [SerializeField] private Text _chiefSkillLevelText;
        [SerializeField] private GameObject _assistantsSlotsFillablePanel;
        [SerializeField] private Image _assistantsSkillLevelImage;
        [SerializeField] private Text _assistantsSkillLevelText;
        [SerializeField] private GameObject _ordersSlotsFillablePanel;
        [SerializeField] private GameObject _charactersFillablePanel;
        [SerializeField] private Button _createOrderButton;
        [SerializeField] private Button _takeMakedItemsButton;
        [SerializeField] private WorkRoomCharacterPanelBehaviour _characterPanelBehaviour;

        //TEMPORARY! Remove after debug!
        [SerializeField] private ItemRecipeData _recipeForDebug;


        private HubUIContext _context;
        private HubUIData _data;
        private (int? slotIndex, CharacterStorageType storageType) _draggedCharacterInfo;
        WorkRoomModel _selectedRoom;
        List<WorkRoomWorkerSlotBehaviour> _assistantsSlotsBehaviours;
        List<WorkRoomOrderSlotBehaviour> _orderSlotsBehaviours;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            _roomCloseButton.onClick.AddListener(OnClick_CloseRoomButton);
            _roomUpgradeButton.onClick.AddListener(OnClick_UpgradeRoomButton);
            _createOrderButton.onClick.AddListener(OnClick_CreateOrderButton);
            _takeMakedItemsButton.onClick.AddListener(OnClick_TakeMakedItemsButton);
        }

        private void OnDisable()
        {
            _roomCloseButton.onClick.RemoveAllListeners();
            _roomUpgradeButton.onClick.RemoveAllListeners();
            _createOrderButton.onClick.RemoveAllListeners();
            _takeMakedItemsButton.onClick.RemoveAllListeners();
        }

        #endregion


        #region IStart

        public void Starting(HubUIContext context)
        {
            _context = context;
            _data = BeastHunter.Data.HubUIData;

            _assistantsSlotsBehaviours = new List<WorkRoomWorkerSlotBehaviour>();
            _orderSlotsBehaviours = new List<WorkRoomOrderSlotBehaviour>();

            for (int i = 0; i < _context.WorkRooms.Count; i++)
            {
                InitializeWorkRoomButton(_context.WorkRooms[i]);
            }

            _chiefSlotBehaviour.Initialize(0, CharacterStorageType.ChiefWorkplace, true);
            _chiefSlotBehaviour.OnBeginDragItemHandler += OnBeginDragCharacterFromSlot;
            _chiefSlotBehaviour.OnEndDragItemHandler += OnEndDragCharacter;
            _chiefSlotBehaviour.OnDroppedItemHandler += OnDropCharacterToSlot;

            for (int i = 0; i < _context.Player.AvailableCharacters.GetSlotsCount(); i++)
            {
                InitializeCharacterListItemUI(i, _context.Player.AvailableCharacters.GetElementBySlot(i));
            }
            _context.Player.AvailableCharacters.OnAddCharacterHandler += InitializeCharacterListItemUI;
            _context.Player.AvailableCharacters.OnRemoveCharacterHandler += RemoveCharacterListItemUI;
            _context.Player.AvailableCharacters.OnReplaceCharacterHandler += UpdateCharacterListItemUI;

            _characterPanelBehaviour.OnDropHandler += OnDropToCharacterPanel;

            gameObject.SetActive(true);
            _roomButtonsFillablePanel.SetActive(true);
            _roomPanel.SetActive(false);
        }

        #endregion


        #region IDestroy

        public void Destroying()
        {
            _context.Player.AvailableCharacters.OnAddCharacterHandler -= InitializeCharacterListItemUI;
            _context.Player.AvailableCharacters.OnRemoveCharacterHandler -= RemoveCharacterListItemUI;
            _context.Player.AvailableCharacters.OnReplaceCharacterHandler -= UpdateCharacterListItemUI;
        }

        #endregion


        #region Methods

        #region PublicMethods

        public void ShowRoomButtonsPanel()
        {
            _roomButtonsPanel.SetActive(true);
        }

        public void HideRoomButtonsPanel()
        {
            _roomButtonsPanel.SetActive(false);
        }

        #endregion

        #region InstantiateAndDestroyMethods

        private void InitializeWorkRoomButton(WorkRoomModel model)
        {
            GameObject buttonUI = InstantiateUIObject(_data.WorkRoomDataStruct.WorkRoomButtonPrefab, _roomButtonsFillablePanel);
            WorkRoomButtonBehaviour buttonBehaviour = buttonUI.GetComponentInChildren<WorkRoomButtonBehaviour>();
            buttonBehaviour.Initialize(model);
            buttonBehaviour.OnClickButtonHandler += OnClick_RoomButton;
            model.OnChangeMinOrderCompleteTimeHandler += buttonBehaviour.UpdateOrderTime;
        }

        private void InitializeAssistantSlotUI(int slotIndex)
        {
            GameObject slotUI = InstantiateUIObject(_data.WorkRoomDataStruct.WorkerSlotPrefab, _assistantsSlotsFillablePanel);
            WorkRoomWorkerSlotBehaviour slotBehaviour = slotUI.GetComponent<WorkRoomWorkerSlotBehaviour>();
            _assistantsSlotsBehaviours.Add(slotBehaviour);
            slotBehaviour.Initialize(slotIndex, CharacterStorageType.AssistantWorkplaces, true);
            slotBehaviour.OnBeginDragItemHandler += OnBeginDragCharacterFromSlot;
            slotBehaviour.OnEndDragItemHandler += OnEndDragCharacter;
            slotBehaviour.OnDroppedItemHandler += OnDropCharacterToSlot;
            _selectedRoom.AssistantWorkplaces.OnPutElementToSlotHandler += FillCharacterSlotUI;
            _selectedRoom.AssistantWorkplaces.OnTakeElementFromSlotHandler += FillCharacterSlotUI;
        }

        private void InitializeOrderSlotUI(int slotIndex)
        {
            GameObject slotUI = InstantiateUIObject(_data.WorkRoomDataStruct.OrderSlotPrefab, _ordersSlotsFillablePanel);
            WorkRoomOrderSlotBehaviour slotBehaviour = slotUI.GetComponentInChildren<WorkRoomOrderSlotBehaviour>();
            _orderSlotsBehaviours.Add(slotBehaviour);
            slotBehaviour.Initialize(slotIndex, OrderStorageType.None, false);
            slotBehaviour.OnClickRemoveOrderButtonHandler += OnClick_RemoveOrderFromSlotButton;
            slotBehaviour.OnClickOpenRecipeBookButtonHandler += OnClick_OrderSlot;
            _selectedRoom.OrdersSlots.OnPutElementToSlotHandler += OnAddOrderInSlot;
            _selectedRoom.OrdersSlots.OnTakeElementFromSlotHandler += OnRemoveOrderFromSlot;
        }

        private void InitializeCharacterListItemUI(int slotIndex, CharacterModel character)
        {
            GameObject characterUI = InstantiateUIObject(_data.WorkRoomDataStruct.CharacterListItemPrefab, _charactersFillablePanel);
            WorkRoomCharacterListItemBehaviour uiBehaviour = characterUI.GetComponent<WorkRoomCharacterListItemBehaviour>();
            uiBehaviour.Initialize(slotIndex, character);
            uiBehaviour.OnBeginDragItemHandler += OnBeginDragCharacterFromSlot;
            uiBehaviour.OnEndDragItemHandler += OnEndDragCharacter;
            uiBehaviour.OnDroppedItemHandler += OnDropCharacterToSlot;
            uiBehaviour.IsPointerEnterOn += IsPointerEnterCharacterListItemOn;
        }

        private void RemoveCharacterListItemUI(int slotIndex)
        {
            DestroyImmediate(GetCharacterListItemBehaviour(slotIndex).gameObject);
        }

        private GameObject InstantiateUIObject(GameObject prefab, GameObject parent)
        {
            GameObject objectUI = GameObject.Instantiate(prefab);
            objectUI.transform.SetParent(parent.transform, false);
            objectUI.transform.localScale = new Vector3(1, 1, 1);
            return objectUI;
        }

        #endregion

        #region OnClickMethods

        private void OnClick_RoomButton(WorkRoomModel model)
        {
            _selectedRoom = model;
            FillRoomPanel(model);
            _roomPanel.SetActive(true);
        }

         private void OnClick_CloseRoomButton()
        {
            _roomPanel.SetActive(false);
            ClearRoomPanel();
            _selectedRoom = null;
        }

        private void OnClick_RemoveOrderFromSlotButton(int slotIndex)
        {
            _selectedRoom.OrdersSlots.RemoveElement(slotIndex);
        }

        private void OnClick_CreateOrderButton()
        {
            if (_selectedRoom.OrdersSlots.HasFreeSlots())
            {
                //todo: open craft window
                //temporary for debug:
                ItemOrderModel order = new ItemOrderModel(_recipeForDebug, _selectedRoom.OrderTimeReducePercent);
                _selectedRoom.OrdersSlots.PutElementToFirstEmptySlot(order);
            }
        }

        private void OnClick_OrderSlot(int slotIndex)
        {
            Debug.Log($"OnClick_OrderSlot({slotIndex})");
            //todo: open recipe book etc
        }

        private void OnClick_TakeMakedItemsButton()
        {
            //todo by design doc (open add window and craft fail chance)
            //temporary:
            for (int i = 0; i < _selectedRoom.OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = _selectedRoom.OrdersSlots.GetElementBySlot(i);
                if (order != null && order.IsCompleted)
                {
                    _context.Player.Inventory.PutElementToFirstEmptySlot(order.MakedItem);
                    HubUIServices.SharedInstance.GameMessages.Notice($"{order.MakedItem.Name} moved to player inventory");
                    _selectedRoom.OrdersSlots.RemoveElement(i);
                }
            }
            
        }

        private void OnClick_UpgradeRoomButton()
        {
            //todo: open add window
        }

        #endregion

        #region DragAndDropMethods

        private void OnBeginDragCharacterFromSlot(int slotIndex, CharacterStorageType storageType)
        {
            _draggedCharacterInfo.slotIndex = slotIndex;
            _draggedCharacterInfo.storageType = storageType;
        }

        private void OnEndDragCharacter(int slotIndex, CharacterStorageType storageType)
        {
            if (storageType != CharacterStorageType.AvailableCharacters)
            {
                FillCharacterSlotUI(storageType, slotIndex, GetCharacterStorageByType(storageType).GetElementBySlot(slotIndex));
            }
            _draggedCharacterInfo.slotIndex = null;
        }

        private void OnDropCharacterToSlot(int dropSlotIndex, CharacterStorageType dropStorageType)
        {
            if (_draggedCharacterInfo.slotIndex.HasValue)
            {
                if (dropStorageType == CharacterStorageType.AvailableCharacters)
                {
                    BaseCharacterStorage storageOut = GetCharacterStorageByType(_draggedCharacterInfo.storageType);
                    CharacterModel character = storageOut.GetElementBySlot(_draggedCharacterInfo.slotIndex.Value);

                    if(!(_draggedCharacterInfo.storageType == CharacterStorageType.AvailableCharacters && _draggedCharacterInfo.slotIndex < dropSlotIndex))
                    {
                        dropSlotIndex++;
                    }

                    if (storageOut.RemoveElement(_draggedCharacterInfo.slotIndex.Value))
                    {
                        GetCharacterStorageByType(dropStorageType).PutElement(dropSlotIndex, character);
                    }
                    else
                    {
                        Debug.LogError($"The remove element from {_draggedCharacterInfo.storageType} is not succeful");
                    }
                }
                else
                {
                    GetCharacterStorageByType(dropStorageType).SwapElementsWithOtherStorage(dropSlotIndex,
                    GetCharacterStorageByType(_draggedCharacterInfo.storageType), _draggedCharacterInfo.slotIndex.Value);
                }
                _draggedCharacterInfo.slotIndex = null;
            }
        }

        private void OnDropToCharacterPanel()
        {
            if (_draggedCharacterInfo.slotIndex.HasValue)
            {
                GetCharacterStorageByType(CharacterStorageType.AvailableCharacters).
                    PutElementToFirstEmptySlotFromOtherStorage(GetCharacterStorageByType(_draggedCharacterInfo.storageType), _draggedCharacterInfo.slotIndex.Value);
                _draggedCharacterInfo.slotIndex = null;
            }
        }

        private bool IsPointerEnterCharacterListItemOn(int slotIndex)
        {
            if (_draggedCharacterInfo.slotIndex.HasValue)
            {
                if(_draggedCharacterInfo.storageType != CharacterStorageType.AvailableCharacters)
                {
                    return true;
                }
                else
                {
                    return _draggedCharacterInfo.slotIndex.Value != slotIndex;
                }
            }
            return false;
        }

        #endregion

        #region FillAndClearMethods

        private void FillRoomPanel(WorkRoomModel room)
        {
            _roomNameText.text = room.Name;

            FillCharacterSlotUI(CharacterStorageType.ChiefWorkplace, 0, room.ChiefWorkplace.GetElementBySlot(0));
            room.ChiefWorkplace.OnPutElementToSlotHandler += FillCharacterSlotUI;
            room.ChiefWorkplace.OnTakeElementFromSlotHandler += FillCharacterSlotUI;

            for (int i = 0; i < room.AssistantWorkplaces.GetSlotsCount(); i++)
            {
                InitializeAssistantSlotUI(i);
                FillCharacterSlotUI(CharacterStorageType.AssistantWorkplaces, i, room.AssistantWorkplaces.GetElementBySlot(i));
            }

            for (int i = 0; i < room.OrdersSlots.GetSlotsCount(); i++)
            {
                InitializeOrderSlotUI(i);
                ItemOrderModel order = room.OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    OnAddOrderInSlot(OrderStorageType.None, i, order);
                }
            }
        }

        private void ClearRoomPanel()
        {
            _selectedRoom.ChiefWorkplace.OnPutElementToSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.ChiefWorkplace.OnTakeElementFromSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.AssistantWorkplaces.OnPutElementToSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.AssistantWorkplaces.OnTakeElementFromSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.OrdersSlots.OnTakeElementFromSlotHandler -= OnRemoveOrderFromSlot;
            _selectedRoom.OrdersSlots.OnPutElementToSlotHandler -= OnAddOrderInSlot;

            for (int i = 0; i < _selectedRoom.OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = _selectedRoom.OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    order.OnChangeHoursNumberToCompleteHandler -= _orderSlotsBehaviours[i].UpdateCraftTimeText;
                }
            }

            for (int i = _assistantsSlotsBehaviours.Count - 1; i >= 0; i--)
            {
                Destroy(_assistantsSlotsBehaviours[i].gameObject);
            }

            for (int i = _orderSlotsBehaviours.Count - 1; i >= 0; i--)
            {
                Destroy(_orderSlotsBehaviours[i].gameObject);
            }

            _assistantsSlotsBehaviours.Clear();
            _orderSlotsBehaviours.Clear();
        }

        private void FillCharacterSlotUI(CharacterStorageType storageType, int slotIndex, CharacterModel character)
        {
            BaseCharacterStorage storage = GetCharacterStorageByType(storageType);
            Sprite sprite = storage.GetElementSpriteBySlot(slotIndex);

            switch (storageType)
            {
                case CharacterStorageType.ChiefWorkplace:

                    _chiefSlotBehaviour.FillSlot(sprite);
                    if (character != null)
                    {
                        _chiefSkillLevelImage.fillAmount = (float)character.Skills[_selectedRoom.UsedSkill] / 100;
                        _chiefSkillLevelText.text = character.Skills[_selectedRoom.UsedSkill].ToString() + "%";
                    }
                    else
                    {
                        _chiefSkillLevelImage.fillAmount = 0;
                        _chiefSkillLevelText.text = "0%";
                    }

                    break;

                case CharacterStorageType.AssistantWorkplaces:

                    _assistantsSlotsBehaviours[slotIndex].FillSlot(sprite);
                    _assistantsSkillLevelImage.fillAmount = (float)_selectedRoom.AssistansGeneralSkillLevel / 100;
                    _assistantsSkillLevelText.text = _selectedRoom.AssistansGeneralSkillLevel.ToString() + "%";

                    break;

                default:

                    Debug.LogError(this + ": incorrect StorageSlotType");

                    break;
            }
        }

        private void OnAddOrderInSlot(OrderStorageType storageType, int slotIndex, ItemOrderModel order)
        {
            _orderSlotsBehaviours[slotIndex].FillSlot(order.Recipe.Item.Icon, order.IsCompleted);
            _orderSlotsBehaviours[slotIndex].UpdateCraftTimeText(order.HoursNumberToComplete);
            order.OnChangeHoursNumberToCompleteHandler += _orderSlotsBehaviours[slotIndex].UpdateCraftTimeText;
        }

        private void OnRemoveOrderFromSlot(OrderStorageType storageType, int slotIndex, ItemOrderModel order)
        {
            _orderSlotsBehaviours[slotIndex].FillSlot(null, null);
            order.OnChangeHoursNumberToCompleteHandler -= _orderSlotsBehaviours[slotIndex].UpdateCraftTimeText;
        }

        private void UpdateCharacterListItemUI(int slotIndex, CharacterModel character)
        {
            GetCharacterListItemBehaviour(slotIndex).UpdateInfo(character);
        }

        #endregion

        #region OtherMethods

        private WorkRoomCharacterListItemBehaviour GetCharacterListItemBehaviour(int index)
        {
            return _charactersFillablePanel.transform.GetChild(index).GetComponent<WorkRoomCharacterListItemBehaviour>();
        }

        private BaseCharacterStorage GetCharacterStorageByType(CharacterStorageType storageType)
        {
            BaseCharacterStorage storage = null;
            switch (storageType)
            {
                case CharacterStorageType.ChiefWorkplace:
                    storage = _selectedRoom.ChiefWorkplace;
                    break;

                case CharacterStorageType.AssistantWorkplaces:
                    storage = _selectedRoom.AssistantWorkplaces;
                    break;

                case CharacterStorageType.AvailableCharacters:
                    storage = _context.Player.AvailableCharacters;
                    break;

                default:
                    Debug.LogError(this + ": incorrect StorageSlotType");
                    break;
            }
            return storage;
        }

        #endregion

        #endregion
    }
}
