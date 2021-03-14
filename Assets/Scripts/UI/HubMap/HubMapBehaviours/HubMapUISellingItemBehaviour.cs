using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class HubMapUISellingItemBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _itemNotSellingImage;

        public void FillSellingItemInfo(BaseItem item, bool isAvailableForSale)
        {
            _itemImage.sprite = item.ItemStruct.Icon;
            SetAvailability(isAvailableForSale);
        }

        public void SetAvailability(bool isAvailableForSale)
        {
            _itemNotSellingImage.SetActive(!isAvailableForSale);
        }
    }
}
