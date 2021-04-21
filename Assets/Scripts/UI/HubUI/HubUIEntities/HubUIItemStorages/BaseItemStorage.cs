using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseItemStorage
    {
        #region Fields

        protected List<BaseItemModel> _items;

        #endregion


        #region Properties

        public virtual Action<ItemStorageType, int, BaseItemModel> OnPutItemToSlotHandler { get; set; }
        public virtual Action<ItemStorageType, int, BaseItemModel> OnTakeItemFromSlotHandler { get; set; }

        public abstract ItemStorageType StorageType { get; protected set; }

        #endregion


        #region Methods

        public abstract bool PutItem(int slotIndex, BaseItemModel item);
        public abstract bool PutItemToFirstEmptySlot(BaseItemModel item);

        public virtual bool RemoveItem(int slotIndex)
        {
            if (_items[slotIndex] != null)
            {
                BaseItemModel takenItem = _items[slotIndex];
                _items[slotIndex] = null;
                OnTakeItemFromSlot(slotIndex, takenItem);
            }
            return true;
        }

        public virtual BaseItemModel GetItemBySlot(int slotIndex)
        {
            return _items[slotIndex];
        }

        public virtual Sprite GetItemIconBySlot(int slotIndex)
        {
            if (_items[slotIndex] != null)
            {
                return _items[slotIndex].Icon;
            }
            else
            {
                return null;
            }
        }

        public virtual int GetSlotsCount()
        {
            return _items.Count;
        }

        public virtual void Clear()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null)
                {
                    RemoveItem(i);
                }
            }
        }

        protected virtual void OnPutItemToSlot(int slotIndex, BaseItemModel newItem)
        {
            OnPutItemToSlotHandler?.Invoke(StorageType, slotIndex, newItem);
        }

        protected virtual void OnTakeItemFromSlot(int slotIndex, BaseItemModel takedItem)
        {
            OnTakeItemFromSlotHandler?.Invoke(StorageType, slotIndex, takedItem);
        }

        #endregion
    }
}
