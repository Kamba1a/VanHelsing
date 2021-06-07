using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace BeastHunterHubUI
{
    public abstract class SlotBehaviour<StorageType> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler where StorageType : Enum
    {
        #region Constants

        private const float DOUBLECLICK_TIME = 0.5f;

        #endregion


        #region Fields

        [SerializeField] protected Image _itemImage;

        private GameObject _draggedObject;
        private float _lastClickTime;
        protected int _slotIndex;
        protected bool _isInteractable;
        protected bool _isDragAndDropOn;
        protected StorageType _storageType;

        #endregion


        #region Properties

        public Action<int, StorageType> OnBeginDragItemHandler { get; set; }
        public Action<int, StorageType> OnEndDragItemHandler { get; set; }
        public Action<int, StorageType> OnDroppedItemHandler { get; set; }
        public Action<int, StorageType> OnPointerEnterHandler { get; set; }
        public Action<int, StorageType> OnPointerExitHandler { get; set; }
        public Action<int, StorageType> OnDoubleClickButtonHandler { get; set; }

        #endregion


        #region Methods

        public virtual void Initialize(int slotIndex, StorageType storageType, bool isDragAndDropOn)
        {
            _storageType = storageType;
            _isInteractable = true;
            _itemImage.enabled = false;
            _slotIndex = slotIndex;
            _isDragAndDropOn = isDragAndDropOn;
        }

        public virtual void FillSlot(Sprite sprite)
        {
            if (sprite != null)
            {
                _itemImage.enabled = true;
            }
            else
            {
                _itemImage.enabled = false;
            }
            _itemImage.sprite = sprite;
        }

        public virtual void SetInteractable(bool flag)
        {
            _isInteractable = flag;
        }

        public virtual void RemoveAllListeners()
        {
            OnBeginDragItemHandler = null;
            OnDroppedItemHandler = null;
            OnEndDragItemHandler = null;
            OnPointerEnterHandler = null;
            OnPointerExitHandler = null;
            OnDoubleClickButtonHandler = null;
        }

        #endregion


        #region IPointerClickHandler

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isInteractable)
            {
                if (Time.time < _lastClickTime + DOUBLECLICK_TIME)
                {
                        OnDoubleClickButtonHandler?.Invoke(_slotIndex, _storageType);
                }
                _lastClickTime = Time.time;
             }
        }

        #endregion


        #region IBeginDragHandler

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (_isInteractable && _isDragAndDropOn)
            {
                if (_itemImage.sprite != null)
                {
                    _draggedObject = Instantiate(_itemImage.gameObject, gameObject.transform.root.Find("Canvas"));

                    RectTransform draggedObjectRectTransform = _draggedObject.GetComponent<RectTransform>();
                    draggedObjectRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    draggedObjectRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    draggedObjectRectTransform.pivot = new Vector2(0.5f, 0.5f);

                    Rect itemImageRect = _itemImage.gameObject.GetComponent<RectTransform>().rect;
                    draggedObjectRectTransform.sizeDelta = new Vector2(itemImageRect.width, itemImageRect.height);

                    FillSlot(null);

                    OnBeginDragItemHandler?.Invoke(_slotIndex, _storageType);
                }
            }
        }

        #endregion


        #region IDragHandler

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (_draggedObject != null)
            {
                _draggedObject.transform.position = Mouse.current.position.ReadValue();
            }
        }

        #endregion


        #region IEndDragHandler

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            //System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
            //EventSystem.current.RaycastAll(eventData, results);
            //Debug.Log("Drop on " + results[0].gameObject.name);

            if (_draggedObject != null)
            {
                Destroy(_draggedObject);
                OnEndDragItemHandler?.Invoke(_slotIndex, _storageType);
            }
        }

        #endregion


        #region IDropHandler

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (_isInteractable && _isDragAndDropOn)
            {
                OnDroppedItemHandler?.Invoke(_slotIndex, _storageType);
            }
        }

        #endregion


        #region IPointerEnterHandler

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_itemImage.sprite != null && _isInteractable)
            {
                OnPointerEnterHandler?.Invoke(_slotIndex, _storageType);
            }
        }

        #endregion


        #region IPointerExitHandler

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (_itemImage.sprite != null)
            {
                OnPointerExitHandler?.Invoke(_slotIndex, _storageType);
            }
        }

        #endregion
    }
}
