using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIClothItemData", menuName = "CreateData/HubMapUIData/Items/Cloth", order = 0)]
    class HubMapUIClothItemData : HubMapUIBaseItemData
    {
        [SerializeField] private HubMapUIClothType _clothType;
        [SerializeField] private string[] _equipmentPartsNames;
        [SerializeField] private string[] _equipmentPartsNamesFemale;
        [SerializeField] private HubMapUICharacterHeadParts[] _disabledHeadParts;

        public HubMapUIClothType ClothType => _clothType;
        public string[] EquipmentPartsNames => (string[])_equipmentPartsNames.Clone();
        public HubMapUICharacterHeadParts[] DisabledHeadParts => (HubMapUICharacterHeadParts[])_disabledHeadParts.Clone();
        public override HubMapUIItemType ItemType { get; protected set; }


        private void OnEnable()
        {
            ItemType = HubMapUIItemType.Cloth;
        }
    }
}
