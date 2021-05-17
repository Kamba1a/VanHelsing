using System;
using System.Collections.Generic;


namespace BeastHunterHubUI
{
    public class ItemStorage : BaseItemStorage
    {
        #region Properties

        public override ItemStorageType StorageType { get; protected set; }

        #endregion


        #region ClassLifeCycle

        public ItemStorage(int slotsAmount, ItemStorageType storageType)
        {
            StorageType = storageType;

            _elementSlots = new List<BaseItemModel>();
            for (int i = 0; i < slotsAmount; i++)
            {
                _elementSlots.Add(null);
            }
        }

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
