using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class EquipmentItemHubMapUIBehaviour: MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _itemButton;

        #endregion


        #region Methods

        public void SetImage(Sprite image)
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

        public void SetInteractable(bool flag)
        {
            _itemButton.interactable = flag;
        }

        #endregion
    }
}
