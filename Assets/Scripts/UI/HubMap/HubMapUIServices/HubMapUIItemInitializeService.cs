﻿namespace BeastHunter
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

                //todo other cases

                default:
                    CustomDebug.LogError(this + ": incorrect HubMapUIItemType");
                    return null;
            }
        }

    }
}
