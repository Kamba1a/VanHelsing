using UnityEngine;
using UnityEngine.UI;


namespace BeastHunterHubUI
{
    class WorkRoomCharacterListItemBehaviour : BaseSlotBehaviour<CharacterModel, CharacterStorageType>
    {
        #region Fields

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _rankText;

        private RectTransform _rectTransform;
        private Vector2 _size;

        #endregion


        #region Properties

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


        #region Methods

        public override void Initialize(CharacterStorageType storageType, int storageSlotIndex)
        {
            base.Initialize(CharacterStorageType.AvailableCharacters, storageSlotIndex);
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _size = _rectTransform.sizeDelta;
        }

        #endregion


        #region BaseSlotBehaviour

        public override void UpdateInfo(CharacterModel character)
        {
            _changeableImage.sprite = character.Portrait;
            _nameText.text = character.Name;
            _rankText.text = character.Rank.ToString();
        }

        protected override void OnBeginDragHeirLogic()
        {
            _objectForDrag.SetActive(false);
            Vector2 newSize = _size;
            newSize.y = 0;
            _rectTransform.sizeDelta = newSize;
        }

        protected override void OnEndDragHeirLogic()
        {
            _rectTransform.sizeDelta = _size;
            _objectForDrag.SetActive(true);
        }

        protected override void OnDropHeirLogic()
        {
            _rectTransform.sizeDelta = _size;
        }

        protected override void OnPointerEnterHeirLogic()
        {
            Vector2 newSize = _size;
            newSize.y = newSize.y * 1.5f;
            _rectTransform.sizeDelta = newSize;
        }

        protected override void OnPointerExitHeirLogic()
        {
            _rectTransform.sizeDelta = _size;
        }

        #endregion
    }
}
