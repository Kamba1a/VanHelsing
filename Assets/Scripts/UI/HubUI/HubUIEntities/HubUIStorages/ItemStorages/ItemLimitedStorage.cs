﻿namespace BeastHunterHubUI
{
    public class ItemLimitedStorage : BaseItemLimitedStorage
    {
        #region ClassLifeCycle

        public ItemLimitedStorage(int slotsAmount, ItemStorageType storageType) : base(slotsAmount, storageType) { }

        #endregion


        #region Methods

        public override bool PutElement(int slotIndex, BaseItemModel item)
        {
            bool isSucceful = false;

            if (_elementSlots[slotIndex] == null)
            {
                _elementSlots[slotIndex] = item;
                isSucceful = true;
            }
            else
            {
                isSucceful = PutElementToFirstEmptySlot(item);
            }

            if (isSucceful)
            {
                OnPutElementToSlot(slotIndex, item);
            }
            else
            {
                HubUIServices.SharedInstance.GameMessages.Notice(StorageType + " is full");
            }

            return isSucceful;
        }

        public override bool PutElementToFirstEmptySlot(BaseItemModel item)
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    return PutElement(i, item);
                }
            }
            return false;
        }

        public bool IsFull()
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsContainItem(BaseItemData item)
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i].DataInstanceID == item.GetInstanceID())
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveFirstItem(BaseItemData item)
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i].DataInstanceID == item.GetInstanceID())
                {
                    RemoveElement(i);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
