using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Extensions;
using System.Collections.Generic;

namespace BeastHunter
{
    class HubMapUISlotBehaviour : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        private const float DOUBLECLICK_TIME = 0.75f;

        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _slotButton;
        [SerializeField] private GameObject _selectSlotFrame;

        private float _lastClickTime;
        private Vector3 _position;
        private GameObject _draggedObject;
        Sprite _itemSprite;

        #endregion


        #region Properties

        public Action<int> OnClick_SlotButtonHandler { get; set; }
        public Action<int> OnDoubleClickButtonHandler { get; set; }
        public Action<int> OnDraggedItemHandler { get; set; }
        public Action<int> OnEndDragItemHandler { get; set; }
        public Action<int> OnDroppedItemHandler { get; set; }

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
            OnDraggedItemHandler = null;
            OnDroppedItemHandler = null;
            OnEndDragItemHandler = null;

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
                if (Time.time < _lastClickTime + DOUBLECLICK_TIME)
                {
                    OnDoubleClickButtonHandler?.Invoke(SlotIndex);
                }
                _lastClickTime = Time.time;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_itemImage.sprite != null)
            {
                _itemSprite = _itemImage.sprite;
                _position = _itemImage.gameObject.transform.position;

                _draggedObject = Instantiate(_itemImage.gameObject, gameObject.transform.GetMainParent().Find("Canvas"));

                RectTransform draggedObjectRectTransform = _draggedObject.GetComponent<RectTransform>();
                draggedObjectRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                draggedObjectRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                draggedObjectRectTransform.pivot = new Vector2(0.5f, 0.5f);

                Rect itemImageRect = _itemImage.gameObject.GetComponent<RectTransform>().rect;
                draggedObjectRectTransform.sizeDelta = new Vector2(itemImageRect.width, itemImageRect.height);

                FillSlot(null);

                OnDraggedItemHandler?.Invoke(SlotIndex);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_draggedObject != null)
            {
                _draggedObject.transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_draggedObject != null)
            {
                Destroy(_draggedObject);
                OnEndDragItemHandler?.Invoke(SlotIndex);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnDroppedItemHandler?.Invoke(SlotIndex);
        }

        #endregion
    }
}
