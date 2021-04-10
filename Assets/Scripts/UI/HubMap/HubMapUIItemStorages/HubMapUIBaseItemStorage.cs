using System;
using UnityEngine;


namespace BeastHunter
{
    public abstract class HubMapUIBaseItemStorage
    {
        #region Fields

        protected HubMapUIBaseItemModel[] _items;

        #endregion


        #region Properties

        public virtual Action<int, HubMapUIBaseItemModel> OnChangeItemHandler { get; set; }

        #endregion


        #region Methods

        public abstract bool PutItem(int slotNumber, HubMapUIBaseItemModel item);
        public abstract bool PutItemToFirstEmptySlot(HubMapUIBaseItemModel item);

        public virtual HubMapUIBaseItemModel TakeItem(int slotNumber)
        {
            HubMapUIBaseItemModel item = _items[slotNumber];
            _items[slotNumber] = null;
            OnChangeItem(slotNumber, null);
            return item;
        }

        public virtual HubMapUIBaseItemModel GetItemBySlot(int slotNumber)
        {
            return _items[slotNumber];
        }

        public virtual Sprite GetItemIconBySlot(int slotNumber)
        {
            if (_items[slotNumber] != null)
            {
                return _items[slotNumber].Icon;
            }
            else
            {
                return null;
            }
        }

        public virtual HubMapUIBaseItemModel[] GetAll()
        {
            return (HubMapUIBaseItemModel[])_items.Clone();
        }

        public virtual int GetSlotsCount()
        {
            return _items.Length;
        }

        public virtual void Clear()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    TakeItem(i);
                }
            }
        }

        protected virtual void OnChangeItem(int slotNumber, HubMapUIBaseItemModel item)
        {
            OnChangeItemHandler?.Invoke(slotNumber, item);
        }

        #endregion
    }
}
