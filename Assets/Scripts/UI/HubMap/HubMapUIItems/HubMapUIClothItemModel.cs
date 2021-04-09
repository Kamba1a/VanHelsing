namespace BeastHunter
{
    public class HubMapUIClothItemModel : HubMapUIBaseItemModel
    {
        #region Properties

        public HubMapUIClothType ClothType { get; private set; }

        #endregion


        public HubMapUIClothItemModel(HubMapUIBaseItemData data) : base(data)
        {
            HubMapUIClothItemData clothData = data as HubMapUIClothItemData;
            ClothType = clothData.ClothType;
        }
    }
}
