using System;


namespace BeastHunterHubUI
{
    public class OrderLimitedStorage : BaseLimitedStorage<ItemOrderModel, OrderStorageType>
    {
        public OrderLimitedStorage(int slotsAmount) : base(slotsAmount, OrderStorageType.None) { }


        public override bool PutElement(int slotIndex, ItemOrderModel order)
        {
            bool isSucceful = false;

            if (_elementSlots[slotIndex] == null)
            {
                _elementSlots[slotIndex] = order;
                isSucceful = true;
            }
            else
            {
                isSucceful = PutElementToFirstEmptySlot(order);
            }

            if (isSucceful)
            {
                OnPutElementToSlot(slotIndex, order);
            }
            else
            {
                HubUIServices.SharedInstance.GameMessages.Notice(StorageType + " is full");
            }

            return isSucceful;
        }

        public override bool PutElementToFirstEmptySlot(ItemOrderModel order)
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    return PutElement(i, order);
                }
            }
            return false;
        }
    }
}
