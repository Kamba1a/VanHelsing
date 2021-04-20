using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace BeastHunter
{
    public class HubMapUIStorageSlotBehaviour : HubMapUIBaseSlotBehaviour, IPointerDownHandler
    {
        #region Fields

        [SerializeField] private Button _slotButton;
        [SerializeField] private Image _selectSlotFrame;

        #endregion


        #region Properties

        public Action<int> OnClick_SlotButtonHandler { get; set; }
        public Action<int> OnPointerDownHandler { get; set; }

        #endregion


        #region Methods

        public override void FillSlotInfo(int slotIndex, bool isDragAndDropOn)
        {
            base.FillSlotInfo(slotIndex, isDragAndDropOn);
            _slotButton.onClick.AddListener(() => OnClick_SlotButton());
        }

        public override void RemoveAllListeners()
        {
            base.RemoveAllListeners();
            OnClick_SlotButtonHandler = null;
            OnPointerDownHandler = null;
            OnDoubleClickButtonHandler = null;
        }

        public void SelectFrameSwitcher(bool flag)
        {
            _selectSlotFrame.enabled = flag;
        }

        public override void SetInteractable(bool flag)
        {
            base.SetInteractable(flag);
            _slotButton.interactable = flag;

            if (!flag)
            {
                FillSlot(null);
            }
        }

        private void OnClick_SlotButton()
        {
            if (_slotButton.interactable)
            {
                OnClick_SlotButtonHandler?.Invoke(_slotIndex);
            }
        }

        #endregion


        #region IPointerDownHandler

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_slotButton.interactable)
            {
                OnPointerDownHandler?.Invoke(_slotIndex);
            }
        }

        #endregion
    }
}
