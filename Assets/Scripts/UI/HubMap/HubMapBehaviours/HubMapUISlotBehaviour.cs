using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BeastHunter
{
    class HubMapUISlotBehaviour : MonoBehaviour, IPointerClickHandler
    {
        private const float DOUBLECLICK_TIME = 0.75f;

        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _slotButton;
        [SerializeField] private GameObject _selectSlotFrame;

        private float lastClickTime;

        #endregion


        #region Properties

        public Action<int> OnClick_SlotButtonHandler { get; set; }
        public Action<int> OnDoubleClickButtonHandler { get; set; }
        public int SlotIndex { get; private set; }

        #endregion


        #region Methods

        public void SelectFrameSwitcher(bool flag)
        {
            _selectSlotFrame.SetActive(flag);
        }

        public void RemoveAllListeners()
        {
            OnClick_SlotButtonHandler = null;
            OnDoubleClickButtonHandler = null;

        }

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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_slotButton.IsInteractable())
            {
                if (Time.time < lastClickTime + DOUBLECLICK_TIME)
                {
                    OnDoubleClickButtonHandler?.Invoke(SlotIndex);
                }
                lastClickTime = Time.time;
            }
        }

        #endregion
    }
}
