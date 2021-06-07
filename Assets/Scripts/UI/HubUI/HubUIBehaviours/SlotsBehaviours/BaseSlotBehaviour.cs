using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    public abstract class BaseSlotBehaviour<EntityType, StorageType> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler where StorageType : Enum
    {
        [SerializeField] protected Image _changeableImage;
        [SerializeField] protected GameObject _objectForDrag;

        protected GameObject _currentDraggedObject;
        protected StorageType _storageType;
        protected int _slotIndex;


        public Action<int, StorageType> OnBeginDragHandler { get; set; }
        public Action<int, StorageType> OnEndDragHandler { get; set; }
        public Action<int, StorageType> OnDropHandler { get; set; }
        public Action<int, StorageType> OnPointerEnterHandler { get; set; }
        public Action<int, StorageType> OnPointerExitHandler { get; set; }
        public Func<int, bool> IsPointerEnterOn { get; set; }

        public bool IsDragAndDropOn { get; set; }
        public bool IsInteractable { get; set; }


        public abstract void UpdateInfo(EntityType entityModel);
        protected abstract void OnBeginDragHeirLogic();
        protected abstract void OnEndDragHeirLogic();
        protected abstract void OnDropHeirLogic();
        protected abstract void OnPointerEnterHeirLogic();
        protected abstract void OnPointerExitHeirLogic();

        public virtual void Initialize(StorageType storageType, int slotIndex)
        {
            _storageType = storageType;
            _slotIndex = slotIndex;
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

                    OnBeginDragHandler?.Invoke(_slotIndex, _storageType);
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_currentDraggedObject != null)
            {
                _currentDraggedObject.transform.position = Mouse.current.position.ReadValue();
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (IsInteractable && IsDragAndDropOn)
            {
                OnDropHeirLogic();
                OnDropHandler?.Invoke(_slotIndex, _storageType);
            }
        }

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
                OnEndDragHandler?.Invoke(_slotIndex, _storageType);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsPointerEnterOn.Invoke(_slotIndex))
            {
                OnPointerEnterHeirLogic();
                OnPointerEnterHandler?.Invoke(_slotIndex, _storageType);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsPointerEnterOn.Invoke(_slotIndex))
            {
                OnPointerExitHeirLogic();
                OnPointerExitHandler?.Invoke(_slotIndex, _storageType);
            }
        }
    }
}
