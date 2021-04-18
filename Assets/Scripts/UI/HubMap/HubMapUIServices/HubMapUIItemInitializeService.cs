namespace BeastHunter
{
    public class HubMapUIItemInitializeService
    {
        public HubMapUIBaseItemModel InitializeItemModel(HubMapUIBaseItemData data)
        {
            switch (data.ItemType)
            {
                case HubMapUIItemType.None:
                    return new HubMapUIBaseItemModel(data);

                case HubMapUIItemType.Cloth:
                    return new HubMapUIClothesItemModel(data as HubMapUIClothesItemData);

                case HubMapUIItemType.PocketItem:
                    return new HubMapUIPocketItemModel(data as HubMapUIPocketItemData);

                case HubMapUIItemType.Weapon:
                    return new HubMapUIWeaponItemModel(data as HubMapUIWeaponItemData);

                default:
                    CustomDebug.LogError(this + ": incorrect HubMapUIItemType");
                    return null;
            }
        }

    }
}
