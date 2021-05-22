using System;


namespace BeastHunterHubUI
{
    public class OrderLimitedStorage : BaseLimitedStorage<ItemOrderModel, OrderStorageType>
    {
        public OrderLimitedStorage(int slotsAmount) : base(slotsAmount, OrderStorageType.None) { }


        public override bool PutElement(int slotIndex, ItemOrderModel element)
        {
            throw new NotImplementedException();
        }

        public override bool PutElementToFirstEmptySlot(ItemOrderModel element)
        {
            throw new NotImplementedException();
        }
    }
}
