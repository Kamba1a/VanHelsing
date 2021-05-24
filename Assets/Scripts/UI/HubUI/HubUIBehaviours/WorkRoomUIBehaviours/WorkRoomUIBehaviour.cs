using System;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomUIBehaviour : MonoBehaviour, IStart
    {
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


        private HubUIContext _context;
        private HubUIData _data;
        private (int? slotIndex, CharacterStorageType storageType) _draggedCharacterInfo;


        private void OnEnable()
        {
            _roomCloseButton.onClick.AddListener(OnClick_CloseRoomButton);
            _roomUpgradeButton.onClick.AddListener(OnClick_UpgradeRoomButton);
        }

        private void OnDisable()
        {
            _roomCloseButton.onClick.RemoveAllListeners();
            _roomUpgradeButton.onClick.RemoveAllListeners();
        }


        public void Starting(HubUIContext context)
        {
            _context = context;
            _data = BeastHunter.Data.HubUIData;


            for (int i = 0; i < _context.WorkRooms.Count; i++)
            {
                InitializeWorkRoomButton(_context.WorkRooms[i]);
            }

            //todo: _chiefSlotBehaviour initialize
            //todo: _charactersFillablePanel initialize

            _roomButtonsFillablePanel.SetActive(true);
            _roomPanel.SetActive(false);
        }


        private void InitializeWorkRoomButton(WorkRoomModel model)
        {
            GameObject buttonUI = InstantiateUIObject(_data.WorkRoomDataStruct.WorkRoomButtonPrefab, _roomButtonsFillablePanel);
            WorkRoomButtonBehaviour behaviour = buttonUI.GetComponentInChildren<WorkRoomButtonBehaviour>();
            behaviour.FillUIInfo(model);
            behaviour.OnClickButtonHandler += OnClick_RoomButton;
        }

        private void OnClick_RoomButton(WorkRoomModel model)
        {
            FillRoomPanel(model);
            _roomPanel.SetActive(true);
        }

        private void OnClick_CloseRoomButton()
        {
            //todo: close and clear
        }

        private void OnClick_UpgradeRoomButton()
        {
            //todo: open add window
        }

        private void FillRoomPanel(WorkRoomModel room)
        {
            _roomNameText.text = room.Name;
            //todo: _chiefSlotBehaviour fill slot logic
            _chiefSkillLevelImage.fillAmount = room.ChiefWorkplace.GetElementBySlot(0).TemporarySkillLevelForDebug/100;
            _chiefSkillLevelText.text = room.ChiefWorkplace.GetElementBySlot(0).TemporarySkillLevelForDebug.ToString() + "%";

            for (int i = 0; i < room.AssistantWorkplaces.GetSlotsCount(); i++)
            {
                InitializeAssistantSlotUI(i);
            }

            _assistantsSkillLevelImage.fillAmount = room.AssistansGeneralSkillLevel / 100;
            _assistantsSkillLevelText.text = room.AssistansGeneralSkillLevel.ToString();

            for (int i = 0; i < room.OrdersSlots.GetSlotsCount(); i++)
            {
                InitializeOrderSlotUI(i);
                InitializeMakedItemSlotUI(i);
            }
        }

        private void InitializeMakedItemSlotUI(int slotIndex)
        {
            //todo
        }

        private void InitializeOrderSlotUI(int slotIndex)
        {
            //todo
        }

        private void InitializeAssistantSlotUI(int slotIndex)
        {
            //todo
        }

        private void OnBeginDragCharacterFromSlot(int slotIndex, CharacterStorageType storageType)
        {
            _draggedCharacterInfo.slotIndex = slotIndex;
            _draggedCharacterInfo.storageType = storageType;
        }

        private void OnEndDragCharacter(int slotIndex, CharacterStorageType storageType)
        {
            FillCharacterSlotUI(storageType, slotIndex, GetCharacterStorageByType(storageType).GetElementBySlot(slotIndex));
            _draggedCharacterInfo.slotIndex = null;
        }

        private void OnDropCharacterToSlot(int dropSlotIndex, CharacterStorageType dropStorageType)
        {
            if (_draggedCharacterInfo.slotIndex.HasValue)
            {
                GetCharacterStorageByType(dropStorageType).SwapElementsWithOtherStorage(dropSlotIndex,
                    GetCharacterStorageByType(_draggedCharacterInfo.storageType), _draggedCharacterInfo.slotIndex.Value);
            }
        }

        private void FillCharacterSlotUI(CharacterStorageType storageType, int slotIndex, CharacterModel character)
        {
            BaseStorage<CharacterModel,Enum> storage = GetCharacterStorageByType(storageType);
            Sprite sprite = storage.GetElementSpriteBySlot(slotIndex);

            switch (storageType)
            {
                case CharacterStorageType.ChiefWorkplace:

                    //_characterBackpackSlotsUIBehaviours[slotIndex].FillSlot(sprite);

                    break;

                default:

                    Debug.LogError(this + ": incorrect StorageSlotType");

                    break;
            }
        }

        private BaseStorage<CharacterModel, Enum> GetCharacterStorageByType(CharacterStorageType storageType)
        {
            BaseStorage<CharacterModel, Enum> storage = null;
            switch (storageType)
            {
                case CharacterStorageType.ChiefWorkplace:
                    //storage = _selected.Character.Backpack;
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
