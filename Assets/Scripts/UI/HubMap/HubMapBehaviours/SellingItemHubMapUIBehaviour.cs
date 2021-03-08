using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class SellingItemHubMapUIBehaviour: MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _itemNotSellingImage;

        public void Initialize(IHubMapItem item, bool isAvailableForSale)
        {
            _itemImage.sprite = item.Image;
            SetAvailability(isAvailableForSale);
        }

        public void Initialize(TempItemData item, bool isAvailableForSale)
        {
            _itemImage.sprite = item.Image;
            SetAvailability(isAvailableForSale);
        }

        public void SetAvailability(bool isAvailableForSale)
        {
            _itemNotSellingImage.SetActive(!isAvailableForSale);
        }
    }
}
