using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class SellingItemUIBehaviour: MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _itemNotSellingImage;

        public void Initialize(ISellingItemInfo item)
        {
            _itemImage.sprite = item.Image;
            _itemNotSellingImage.SetActive(!item.IsEnoughReputation);
        }
    }
}
