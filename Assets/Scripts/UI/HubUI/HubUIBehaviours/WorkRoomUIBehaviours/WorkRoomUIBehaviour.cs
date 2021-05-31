﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomUIBehaviour : MonoBehaviour, IStart, IDestroy
    {
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
        [SerializeField] private GameObject _makedItemsSlotsFillablePanel;
        [SerializeField] private GameObject _charactersFillablePanel;
        [SerializeField] private Button _createOrderButton;
        [SerializeField] private Button _takeMakedItemsButton;

        //TEMPORARY! Remove after debug!
        [SerializeField] private ItemRecipeData _recipeForDebug;


        private HubUIContext _context;
        private HubUIData _data;
        private (int? slotIndex, CharacterStorageType storageType) _draggedCharacterInfo;
        WorkRoomModel _selectedRoom;
        List<WorkRoomWorkerSlotBehaviour> _assistantsSlotsBehaviours;
        List<WorkRoomOrderSlotBehaviour> _orderSlotsBehaviours;
        List<WorkRoomMakedItemSlotBehaviour> _makedItemSlotsBehaviours;


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


        public void Starting(HubUIContext context)
        {
            _context = context;
            _data = BeastHunter.Data.HubUIData;

            _assistantsSlotsBehaviours = new List<WorkRoomWorkerSlotBehaviour>();
            _orderSlotsBehaviours = new List<WorkRoomOrderSlotBehaviour>();
            _makedItemSlotsBehaviours = new List<WorkRoomMakedItemSlotBehaviour>();

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
            _context.Player.AvailableCharacters.OnReplaceCharacterHandler += ReplaceCharacterListItemUI;

            _roomButtonsFillablePanel.SetActive(true);
            _roomPanel.SetActive(false);
        }

        public void Destroying()
        {
            _context.Player.AvailableCharacters.OnAddCharacterHandler -= InitializeCharacterListItemUI;
            _context.Player.AvailableCharacters.OnRemoveCharacterHandler -= RemoveCharacterListItemUI;
            _context.Player.AvailableCharacters.OnReplaceCharacterHandler -= ReplaceCharacterListItemUI;
        }


        public void ShowRoomButtonsPanel()
        {
            _roomButtonsPanel.SetActive(true);
        }

        public void HideRoomButtonsPanel()
        {
            _roomButtonsPanel.SetActive(false);
        }

        private void OnClick_CreateOrderButton()
        {
            if (_selectedRoom.OrdersSlots.HasFreeSlots())
            {
                //todo: open craft window
                //temporary for debug:
                ItemOrderModel order = new ItemOrderModel(_recipeForDebug, _selectedRoom.OrderTimeReducePercent);
                if(_selectedRoom.OrdersSlots.PutElementToFirstEmptySlot(order, out int? slotIndex))
                {
                    _makedItemSlotsBehaviours[slotIndex.Value].FillSlot(order.Recipe.Item.Icon);
                    _makedItemSlotsBehaviours[slotIndex.Value].UpdateTimeText(order.HoursNumberToComplete);
                    order.OnChangeHoursNumberToCompleteHandler += _makedItemSlotsBehaviours[slotIndex.Value].UpdateTimeText;
                }
            }
        }

        private void OnClick_TakeMakedItemsButton()
        {
            //todo
        }

        private void OnClick_UpgradeRoomButton()
        {
            //todo: open add window
        }

        //WIP
        private void InitializeCharacterListItemUI(int slotIndex, CharacterModel character)
        {
            GameObject characterUI = InstantiateUIObject(_data.WorkRoomDataStruct.CharacterListItemPrefab, _charactersFillablePanel);
            WorkRoomCharacterListItemBehaviour uiBehaviour = characterUI.GetComponent<WorkRoomCharacterListItemBehaviour>();
            uiBehaviour.Initialize(slotIndex, character);
            uiBehaviour.OnBeginDragItemHandler += OnBeginDragCharacterFromSlot;
            uiBehaviour.OnEndDragItemHandler += OnEndDragCharacter;
            uiBehaviour.OnDroppedItemHandler += OnDropCharacterToSlot;
            uiBehaviour.IsPointerEnterOn += IsDraggedCharacter;
        }

        private bool IsDraggedCharacter(int slotIndex)
        {
            return _draggedCharacterInfo.slotIndex.HasValue && _draggedCharacterInfo.slotIndex.Value != slotIndex;
        }

        private void RemoveCharacterListItemUI(int slotIndex)
        {
            Destroy(GetCharacterListItemBehaviour(slotIndex).gameObject);
        }

        private void ReplaceCharacterListItemUI(int slotIndex, CharacterModel character)
        {
            GetCharacterListItemBehaviour(slotIndex).UpdateInfo(character);
        }

        private WorkRoomCharacterListItemBehaviour GetCharacterListItemBehaviour(int index)
        {
            return _charactersFillablePanel.transform.GetChild(index).GetComponent<WorkRoomCharacterListItemBehaviour>();
        }

        private void InitializeWorkRoomButton(WorkRoomModel model)
        {
            GameObject buttonUI = InstantiateUIObject(_data.WorkRoomDataStruct.WorkRoomButtonPrefab, _roomButtonsFillablePanel);
            WorkRoomButtonBehaviour behaviour = buttonUI.GetComponentInChildren<WorkRoomButtonBehaviour>();
            behaviour.Initialize(model);
            behaviour.OnClickButtonHandler += OnClick_RoomButton;
            model.OnChangeMinOrderCompleteTimeHandler += behaviour.UpdateOrderTime;
        }

        private void OnClick_RoomButton(WorkRoomModel model)
        {
            _selectedRoom = model;
            FillRoomPanel(model);
            _roomPanel.SetActive(true);
        }

        private void OnClick_CloseRoomButton()
        {
            _roomPanel.SetActive(false);

            _selectedRoom.ChiefWorkplace.OnPutElementToSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.ChiefWorkplace.OnTakeElementFromSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.AssistantWorkplaces.OnPutElementToSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.AssistantWorkplaces.OnTakeElementFromSlotHandler -= FillCharacterSlotUI;
            _selectedRoom.OrdersSlots.OnTakeElementFromSlotHandler -= FillOrderSlotUI;
            _selectedRoom.OrdersSlots.OnPutElementToSlotHandler -= FillOrderSlotUI;
            _selectedRoom.MakedItemsSlots.OnPutElementToSlotHandler -= FillMakedItemSlotUI;
            _selectedRoom.MakedItemsSlots.OnTakeElementFromSlotHandler -= FillMakedItemSlotUI;

            for (int i = 0; i < _selectedRoom.OrdersSlots.GetSlotsCount(); i++)
            {
                ItemOrderModel order = _selectedRoom.OrdersSlots.GetElementBySlot(i);
                if (order != null)
                {
                    order.OnChangeHoursNumberToCompleteHandler -= _makedItemSlotsBehaviours[i].UpdateTimeText;
                }
            }

            for (int i = _assistantsSlotsBehaviours.Count-1; i >= 0; i--)
            {
                Destroy(_assistantsSlotsBehaviours[i].gameObject);
            }

            for (int i = _orderSlotsBehaviours.Count-1; i >= 0; i--)
            {
                Destroy(_orderSlotsBehaviours[i].gameObject);
            }

            for (int i = _makedItemSlotsBehaviours.Count-1; i >= 0; i--)
            {
                Destroy(_makedItemSlotsBehaviours[i].gameObject);
            }

            _assistantsSlotsBehaviours.Clear();
            _orderSlotsBehaviours.Clear();
            _makedItemSlotsBehaviours.Clear();
            _selectedRoom = null;
        }

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
                ItemOrderModel order = room.OrdersSlots.GetElementBySlot(i);
                InitializeOrderSlotUI(i);
                InitializeMakedItemSlotUI(i);
                FillOrderSlotUI(OrderStorageType.None, i, order);
                FillMakedItemSlotUI(ItemStorageType.WorkRoomFinishedItems, i, room.MakedItemsSlots.GetElementBySlot(i));
                if (order != null)
                {
                    _makedItemSlotsBehaviours[i].FillSlot(order.Recipe.Item.Icon);
                    _makedItemSlotsBehaviours[i].UpdateTimeText(order.HoursNumberToComplete);
                    order.OnChangeHoursNumberToCompleteHandler += _makedItemSlotsBehaviours[i].UpdateTimeText;
                }
            }
        }

        private void InitializeOrderSlotUI(int slotIndex)
        {
            GameObject slotUI = InstantiateUIObject(_data.WorkRoomDataStruct.OrderSlotPrefab, _ordersSlotsFillablePanel);
            WorkRoomOrderSlotBehaviour slotBehaviour = slotUI.GetComponentInChildren<WorkRoomOrderSlotBehaviour>();
            _orderSlotsBehaviours.Add(slotBehaviour);
            slotBehaviour.Initialize(slotIndex, OrderStorageType.None, false);
            slotBehaviour.OnClickRemoveOrderButtonHandler += OnClick_RemoveOrderFromSlotButton;
            _selectedRoom.OrdersSlots.OnPutElementToSlotHandler += FillOrderSlotUI;
            _selectedRoom.OrdersSlots.OnTakeElementFromSlotHandler += FillOrderSlotUI;
        }

        private void OnClick_RemoveOrderFromSlotButton(int slotIndex)
        {
            _makedItemSlotsBehaviours[slotIndex].FillSlot(null);
            _selectedRoom.OrdersSlots.GetElementBySlot(slotIndex).OnChangeHoursNumberToCompleteHandler -= _makedItemSlotsBehaviours[slotIndex].UpdateTimeText;
            _selectedRoom.OrdersSlots.RemoveElement(slotIndex);
        }

        private void InitializeMakedItemSlotUI(int slotIndex)
        {
            GameObject slotUI = InstantiateUIObject(_data.WorkRoomDataStruct.MakedItemSlotPrefab, _makedItemsSlotsFillablePanel);
            WorkRoomMakedItemSlotBehaviour slotBehaviour = slotUI.GetComponentInChildren<WorkRoomMakedItemSlotBehaviour>();
            _makedItemSlotsBehaviours.Add(slotBehaviour);
            slotBehaviour.Initialize(slotIndex, ItemStorageType.WorkRoomMakedItems, false);
            _selectedRoom.MakedItemsSlots.OnPutElementToSlotHandler += FillMakedItemSlotUI;
            _selectedRoom.MakedItemsSlots.OnTakeElementFromSlotHandler += FillMakedItemSlotUI;
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
                    //GetCharacterStorageByType(dropStorageType).PutElementToFirstEmptySlotFromOtherStorage
                    //    (GetCharacterStorageByType(_draggedCharacterInfo.storageType), _draggedCharacterInfo.slotIndex.Value);
                    BaseCharacterStorage storageOut = GetCharacterStorageByType(_draggedCharacterInfo.storageType);
                    CharacterModel character = storageOut.GetElementBySlot(_draggedCharacterInfo.slotIndex.Value);

                    if (storageOut.RemoveElement(_draggedCharacterInfo.slotIndex.Value))
                    {
                        GetCharacterStorageByType(dropStorageType).PutElement(dropSlotIndex + 1, character);
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

        private void FillOrderSlotUI(OrderStorageType storageType, int slotIndex, ItemOrderModel order)
        {
            Sprite sprite = _selectedRoom.OrdersSlots.GetElementSpriteBySlot(slotIndex);
            _orderSlotsBehaviours[slotIndex].FillSlot(sprite);
        }

        private void FillMakedItemSlotUI(ItemStorageType storageType, int slotIndex, BaseItemModel item)
        {
            Sprite sprite = _selectedRoom.MakedItemsSlots.GetElementSpriteBySlot(slotIndex);
            _makedItemSlotsBehaviours[slotIndex].FillSlot(sprite);
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

        private GameObject InstantiateUIObject(GameObject prefab, GameObject parent)
        {
            GameObject objectUI = GameObject.Instantiate(prefab);
            objectUI.transform.SetParent(parent.transform, false);
            objectUI.transform.localScale = new Vector3(1, 1, 1);
            return objectUI;
        }
    }
}
