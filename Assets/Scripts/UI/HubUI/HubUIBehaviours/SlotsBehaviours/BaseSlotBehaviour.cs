using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    public abstract class BaseSlotBehaviour<EntityType, StorageType> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler where StorageType : Enum
    {
        #region Fields

        [SerializeField] protected Image _changeableImage;
        [SerializeField] protected GameObject _objectForDrag;

        protected GameObject _currentDraggedObject;
        protected StorageType _storageType;
        protected int _storageSlotIndex;

        #endregion


        #region Properties

        public Action<int, StorageType> OnBeginDragHandler { get; set; }
        public Action<int, StorageType> OnEndDragHandler { get; set; }
        public Action<int, StorageType> OnDropHandler { get; set; }
        public Action<int, StorageType> OnPointerEnterHandler { get; set; }
        public Action<int, StorageType> OnPointerExitHandler { get; set; }
        public Func<int, bool> IsPointerEnterOn { get; set; }

        public virtual bool IsDragAndDropOn { get; set; }
        public virtual bool IsInteractable { get; set; }

        #endregion


        #region UnityMethods


        private void OnDestroy()
        {
            if (_currentDraggedObject != null)
            {
                Destroy(_currentDraggedObject);
            }
        }

        #endregion


        #region Methods

        public abstract void UpdateInfo(EntityType entityModel);
        protected abstract void OnBeginDragHeirLogic();
        protected abstract void OnEndDragHeirLogic();
        protected abstract void OnDropHeirLogic();
        protected abstract void OnPointerEnterHeirLogic();
        protected abstract void OnPointerExitHeirLogic();

        public virtual void Initialize(StorageType storageType, int storageSlotIndex)
        {
            _storageType = storageType;
            _storageSlotIndex = storageSlotIndex;
            IsDragAndDropOn = true;
            IsInteractable = true;
            IsPointerEnterOn = (_slotIndex) => true;
        }

        public virtual void RemoveAllListeners()
        {
            OnBeginDragHandler = null;
            OnDropHandler = null;
            OnEndDragHandler = null;
            OnPointerEnterHandler = null;
            OnPointerExitHandler = null;
        }

        #endregion


        #region IBeginDragHandler

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsInteractable && IsDragAndDropOn)
            {
                if (_changeableImage.sprite != null)
                {
                    _currentDraggedObject = Instantiate(_objectForDrag, gameObject.transform.root.Find("Canvas"));

                    RectTransform draggedObjectRectTransform = _currentDraggedObject.GetComponent<RectTransform>();
                    draggedObjectRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    draggedObjectRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    draggedObjectRectTransform.pivot = new Vector2(0.5f, 0.5f);

                    Rect objectForDragRect = _objectForDrag.GetComponent<RectTransform>().rect;
                    draggedObjectRectTransform.sizeDelta = new Vector2(objectForDragRect.width, objectForDragRect.height);

                    OnBeginDragHeirLogic();

                    OnBeginDragHandler?.Invoke(_storageSlotIndex, _storageType);
                }
            }
        }

        #endregion


        #region IDragHandler

        public void OnDrag(PointerEventData eventData)
        {
            if (_currentDraggedObject != null)
            {
                _currentDraggedObject.transform.position = Mouse.current.position.ReadValue();
            }
        }

        #endregion


        #region IDrophandler

        public void OnDrop(PointerEventData eventData)
        {
            if (IsInteractable && IsDragAndDropOn)
            {
                OnDropHeirLogic();
                OnDropHandler?.Invoke(_storageSlotIndex, _storageType);
            }
        }

        #endregion


        #region IEndDragHandler

        public void OnEndDrag(PointerEventData eventData)
        {
            //for debug:
            //System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
            //EventSystem.current.RaycastAll(eventData, results);
            //Debug.Log("Drop on " + results[0].gameObject.name);

            if (_currentDraggedObject != null)
            {
                Destroy(_currentDraggedObject);
                OnEndDragHeirLogic();
                OnEndDragHandler?.Invoke(_storageSlotIndex, _storageType);
            }
        }

        #endregion


        #region IPointerEnterHandler

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsPointerEnterOn.Invoke(_storageSlotIndex))
            {
                OnPointerEnterHeirLogic();
                OnPointerEnterHandler?.Invoke(_storageSlotIndex, _storageType);
            }
        }

        #endregion


        #region IPointerExitHandler

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsPointerEnterOn.Invoke(_storageSlotIndex))
            {
                OnPointerExitHeirLogic();
                OnPointerExitHandler?.Invoke(_storageSlotIndex, _storageType);
            }
        }

        #endregion
    }
}
