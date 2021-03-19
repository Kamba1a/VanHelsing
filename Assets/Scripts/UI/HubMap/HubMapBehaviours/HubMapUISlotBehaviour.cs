using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BeastHunter
{
    class HubMapUISlotBehaviour : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _slotButton;
        [SerializeField] private GameObject _selectSlotFrame;

        #endregion


        #region Properties

        public Action<int> OnClick_SlotButtonHandler { get; set; }
        public Action<int> OnSelectHandler { get; set; }
        public int SlotIndex { get; private set; }

        #endregion


        #region Methods

        public void FillSlotInfo(int slotIndex)
        {
            SlotIndex = slotIndex;
            _slotButton.onClick.AddListener(() => OnClick_SlotButton());
        }

        public void SetInteractable(bool flag)
        {
            _slotButton.interactable = flag;
        }

        public void FillSlot(Sprite sprite)
        {
            SetIcon(sprite);
        }

        private void SetIcon(Sprite sprite)
        {
            if (sprite != null)
            {
                Color color = _itemImage.color;
                color.a = 255f;
                _itemImage.color = color;
            }
            else
            {
                Color color = _itemImage.color;
                color.a = 0f;
                _itemImage.color = color;
            }
            _itemImage.sprite = sprite;
        }

        private void OnClick_SlotButton()
        {
            OnClick_SlotButtonHandler?.Invoke(SlotIndex);
        }

        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log("OnSelect");

            OnSelectHandler?.Invoke(SlotIndex);
            if (_selectSlotFrame != null)
            {
                _selectSlotFrame.SetActive(true);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            Debug.Log("OnDeSelect");

            if (_selectSlotFrame != null)
            {
                _selectSlotFrame.SetActive(false);
            }
        }

        #endregion
    }
}
