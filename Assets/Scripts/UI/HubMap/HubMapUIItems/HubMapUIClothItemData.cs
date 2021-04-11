using UnityEngine;


namespace BeastHunter
{
    [CreateAssetMenu(fileName = "HubMapUIClothItemData", menuName = "CreateData/HubMapUIData/Items/Cloth", order = 0)]
    class HubMapUIClothesItemData : HubMapUIBaseItemData
    {
        [SerializeField] private HubMapUIClothesType _clothType;
        [SerializeField] private string[] _clothesPartsNamesAllGender;
        [SerializeField] private string[] _clothesPartsNamesMale;
        [SerializeField] private string[] _clothesPartsNamesFemale;
        [SerializeField] private HubMapUICharacterHeadParts[] _disabledHeadParts;
        //todo: clothes material

        public HubMapUIClothesType ClothesType => _clothType;
        public string[] ClothesPartsNamesAllGender => (string[])_clothesPartsNamesAllGender.Clone();
        public string[] ClothesPartsNamesMale => (string[])_clothesPartsNamesMale.Clone();
        public string[] ClothesPartsNamesFemale => (string[])_clothesPartsNamesFemale.Clone();
        public HubMapUICharacterHeadParts[] DisabledHeadParts => (HubMapUICharacterHeadParts[])_disabledHeadParts.Clone();
        public override HubMapUIItemType ItemType { get; protected set; }


        private void OnEnable()
        {
            ItemType = HubMapUIItemType.Cloth;
        }
    }
}
