using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class HubMapUISlotBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _slotButton;

        #endregion


        #region Properties

        public Action <int>OnClick_SlotButtonHandler { get; set; }

        #endregion


        #region Methods

        public void FillSlotInfo(int slotIndex)
        {
            _slotButton.onClick.AddListener(() => OnClick_SlotButton(slotIndex));
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

        private void OnClick_SlotButton(int slotIndex)
        {
            OnClick_SlotButtonHandler?.Invoke(slotIndex);
        }

        #endregion
    }
}
