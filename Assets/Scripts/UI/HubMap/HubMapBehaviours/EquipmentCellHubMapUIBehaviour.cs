using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class EquipmentCellHubMapUIBehaviour: MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _itemButton;

        #endregion


        #region Properties

        public IHubMapItem CurrentItem { get; private set; }

        #endregion


        #region Methods

        public void SetInteractable(bool flag)
        {
            _itemButton.interactable = flag;
        }

        public void PutItemInCell(IHubMapItem item)
        {
            CurrentItem = item;
            SetImage(item.Image);
        }

        public void ClearCell()
        {
            CurrentItem = null;
            SetImage(null);
        }

        private void SetImage(Sprite image)
        {
            if (image != null)
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
            _itemImage.sprite = image;
        }

        #endregion
    }
}
