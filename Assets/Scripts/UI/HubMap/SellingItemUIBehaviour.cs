using UnityEngine;
using UnityEngine.UI;

namespace BeastHunter
{
    class SellingItemUIBehaviour: MonoBehaviour
    {
        [SerializeField] private GameObject _itemImage;
        [SerializeField] private GameObject _itemNotSellingMask;

        public void Initialize(ISellingItemInfo item)
        {
            _itemImage.GetComponent<Image>().sprite = item.Image;
            _itemNotSellingMask.SetActive(!item.IsEnoughReputation);
        }
    }
}
