using System;
using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class AvailableCharacterListItemBehaviour : BaseCharacterSlotBehaviour
    {
        #region Fields

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _rankText;

        private RectTransform _rectTransform;
        private Vector2 _size;

        #endregion


        #region Properties

        public Func<int, bool> IsPointerEnterOnFunc { get; set; }

        protected override int _storageSlotIndex
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


        #region BaseCharacterSlotBehaviour

        public override void Initialize(CharacterStorageType storageType, int storageSlotIndex)
        {
            base.Initialize(CharacterStorageType.AvailableCharacters, storageSlotIndex);
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _size = _rectTransform.sizeDelta;
        }

        protected override void FillSlot(CharacterModel character)
        {
            base.FillSlot(character);
            _nameText.text = character.Name;
            _rankText.text = character.Rank.ToString();
        }

        protected override void ClearSlot()
        {
            base.ClearSlot();
            _nameText.text = "";
            _rankText.text = "";
        }

        protected override bool IsPointerEnterOn()
        {
            return base.IsPointerEnterOn()
                && IsPointerEnterOnFunc.Invoke(_storageSlotIndex);
        }

        protected override bool IsPointerExitOn()
        {
            return base.IsPointerExitOn()
                && IsPointerEnterOnFunc.Invoke(_storageSlotIndex);
        }

        protected override void OnBeginDragLogic()
        {
            base.OnBeginDragLogic();
            _objectForDrag.SetActive(false);
            Vector2 newSize = _size;
            newSize.y = 0;
            _rectTransform.sizeDelta = newSize;
        }

        protected override void OnEndDragLogic()
        {
            base.OnEndDragLogic();
            _rectTransform.sizeDelta = _size;
            _objectForDrag.SetActive(true);
        }

        protected override void OnDropLogic()
        {
            base.OnDropLogic();
            _rectTransform.sizeDelta = _size;
        }

        protected override void OnPointerEnterLogic()
        {
            base.OnPointerEnterLogic();
            Vector2 newSize = _size;
            newSize.y = newSize.y * 1.5f;
            _rectTransform.sizeDelta = newSize;
        }

        protected override void OnPointerExitLogic()
        {
            base.OnPointerExitLogic();
            _rectTransform.sizeDelta = _size;
        }

        #endregion
    }
}
