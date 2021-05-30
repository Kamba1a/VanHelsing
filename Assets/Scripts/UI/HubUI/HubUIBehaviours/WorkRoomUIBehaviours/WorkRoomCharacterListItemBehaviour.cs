using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomCharacterListItemBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _objectForDrag;
        [SerializeField] private Image _characterPortraitImage;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _rankText;

        private GameObject _currentDraggedObject;
        private CharacterStorageType _storageType;
        private RectTransform _rectTransform;
        private Vector2 _size;


        #region Properties

        public Action<int, CharacterStorageType> OnBeginDragItemHandler { get; set; }
        public Action<int, CharacterStorageType> OnEndDragItemHandler { get; set; }
        public Action<int, CharacterStorageType> OnDroppedItemHandler { get; set; }
        public Action<int, CharacterStorageType> OnPointerEnterHandler { get; set; }
        public Action<int, CharacterStorageType> OnPointerExitHandler { get; set; }
        public Func<bool> IsPointerEnterOn { get; set; }

        public int SlotIndex
        {
            get
            {
                return gameObject.transform.GetSiblingIndex();
            }
            set
            {
                gameObject.transform.SetSiblingIndex(value);
            }
        }

        #endregion


        private void OnDestroy()
        {
            if(_currentDraggedObject != null)
            {
                Destroy(_currentDraggedObject);
            }
        }


        #region Methods

        public void Initialize(int storageSlotIndex,  CharacterModel character)
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _size = _rectTransform.sizeDelta;
            SlotIndex = storageSlotIndex;
            _storageType = CharacterStorageType.AvailableCharacters;
            UpdateInfo(character);
        }

        public void UpdateInfo(CharacterModel character)
        {
            _characterPortraitImage.sprite = character.Portrait;
            _nameText.text = character.Name;
            _rankText.text = character.Rank.ToString();
        }

        public void RemoveAllListeners()
        {
            OnBeginDragItemHandler = null;
            OnDroppedItemHandler = null;
            OnEndDragItemHandler = null;
            OnPointerEnterHandler = null;
            OnPointerExitHandler = null;
        }

        private void SetHide(bool flag)
        {
            if (flag)
            {
                _objectForDrag.SetActive(false);
                Vector2 newSize = _size;
                newSize.y = 0;
                _rectTransform.sizeDelta = newSize;
            }
            else
            {
                _rectTransform.sizeDelta = _size;
                _objectForDrag.SetActive(true);
            }
        }

        #endregion


        #region IBeginDragHandler

        public void OnBeginDrag(PointerEventData eventData)
        {
            _currentDraggedObject = Instantiate(_objectForDrag, gameObject.transform.root.Find("Canvas"));

            RectTransform draggedObjectRectTransform = _currentDraggedObject.GetComponent<RectTransform>();
            draggedObjectRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            draggedObjectRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            draggedObjectRectTransform.pivot = new Vector2(0.5f, 0.5f);

            Rect objectForDragRect = _objectForDrag.GetComponent<RectTransform>().rect;
            draggedObjectRectTransform.sizeDelta = new Vector2(objectForDragRect.width, objectForDragRect.height);

            SetHide(true);
            OnBeginDragItemHandler?.Invoke(SlotIndex, _storageType);
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


        #region IEndDragHandler

        public void OnEndDrag(PointerEventData eventData)
        {
            //System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
            //EventSystem.current.RaycastAll(eventData, results);
            //Debug.Log("Drop on " + results[0].gameObject.name);

            if (_currentDraggedObject != null)
            {
                Destroy(_currentDraggedObject);
                SetHide(false);
                OnEndDragItemHandler?.Invoke(SlotIndex, _storageType);
            }
        }

        #endregion


        #region IDropHandler

        public void OnDrop(PointerEventData eventData)
        {
            OnDroppedItemHandler?.Invoke(SlotIndex, _storageType);
        }

        #endregion


        #region IPointerEnterHandler

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsPointerEnterOn.Invoke())
            {
                OnPointerEnterHandler?.Invoke(SlotIndex, _storageType);
            }
        }

        #endregion


        #region IPointerExitHandler

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsPointerEnterOn.Invoke())
            {
                OnPointerExitHandler?.Invoke(SlotIndex, _storageType);
            }
        }

        #endregion
    }
}
