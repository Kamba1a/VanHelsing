namespace BeastHunter
{
    public class HubMapUIClothItemModel : HubMapUIBaseItemModel
    {
        #region Properties

        public HubMapUIClothType ClothType { get; private set; }
        public string[] EquipmentPartsNames { get; private set; }
        public HubMapUICharacterHeadParts[] DisabledHeadParts { get; private set; }

        #endregion


        public HubMapUIClothItemModel(HubMapUIBaseItemData data) : base(data)
        {
            HubMapUIClothItemData clothData = data as HubMapUIClothItemData;
            ClothType = clothData.ClothType;
            EquipmentPartsNames = clothData.EquipmentPartsNames;
            DisabledHeadParts = clothData.DisabledHeadParts;
        }
    }
}
