namespace BeastHunter
{
    public class HubMapUIClothesItemModel : HubMapUIBaseItemModel
    {
        #region Properties

        public HubMapUIClothesType ClothesType { get; private set; }
        public string[] ClothesPartsNamesAllGender { get; private set; }
        public string[] ClothesPartsNamesMale { get; private set; }
        public string[] ClothesPartsNamesFemale { get; private set; }
        public HubMapUICharacterHeadParts[] DisabledHeadParts { get; private set; }

        #endregion


        public HubMapUIClothesItemModel(HubMapUIBaseItemData data) : base(data)
        {
            HubMapUIClothesItemData clothesData = data as HubMapUIClothesItemData;
            ClothesType = clothesData.ClothesType;
            ClothesPartsNamesAllGender = clothesData.ClothesPartsNamesAllGender;
            ClothesPartsNamesMale = clothesData.ClothesPartsNamesMale;
            ClothesPartsNamesFemale = clothesData.ClothesPartsNamesFemale;
            DisabledHeadParts = clothesData.DisabledHeadParts;
        }
    }
}
