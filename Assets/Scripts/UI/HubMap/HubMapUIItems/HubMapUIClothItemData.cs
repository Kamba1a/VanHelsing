using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIClothItemData", menuName = "CreateData/HubMapUIData/Items/Cloth", order = 0)]
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
