using UnityEngine;


namespace BeastHunter
{
    class HubMapUIClothItemData : HubMapUIBaseItemData
    {
        [SerializeField] private HubMapUIClothType _clothType;

        public HubMapUIClothType ClothType => _clothType;
        public override HubMapUIItemType ItemType { get; protected set; }


        private void OnEnable()
        {
            ItemType = HubMapUIItemType.Cloth;
        }
    }
}
