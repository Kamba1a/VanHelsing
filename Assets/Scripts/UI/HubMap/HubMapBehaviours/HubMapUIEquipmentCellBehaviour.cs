﻿using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class HubMapUIEquipmentCellBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _itemButton;

        #endregion


        #region Methods

        public void SetInteractable(bool flag)
        {
            _itemButton.interactable = flag;
        }

        public void PutItemInCell(IHubMapUIItem item)
        {
            SetImage(item.Image);
        }

        public void ClearCell()
        {
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
