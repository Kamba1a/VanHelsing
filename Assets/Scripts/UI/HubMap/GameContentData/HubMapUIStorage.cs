using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIStorage
    {
        #region Fields

        private BaseItem[] _items;

        #endregion


        #region Properties

        public Action<int, Sprite> OnChangeItemHandler { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIStorage(int slotsAmount)
        {
            _items = new BaseItem[slotsAmount];
        }

        #endregion


        #region Methods

        public BaseItem TakeItem(int slotNumber)
        {
            BaseItem item = _items[slotNumber];
            _items[slotNumber] = null;
            OnChangeItem(slotNumber, null);
            return item;
        }

        public bool PutItem(int slotNumber, BaseItem item)
        {
            bool isSucceful = false;

            if (_items[slotNumber] == null)
            {
                _items[slotNumber] = item;
                isSucceful = true;
            }
            else
            {
                isSucceful = MovingItemToFirstEmptySlot(item);
            }

            if (isSucceful)
            {
                OnChangeItem(slotNumber, item);
            }

            return isSucceful;
        }

        public bool MovingItemToFirstEmptySlot(BaseItem item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    PutItem(i, item);
                    return true;
                }
            }
            return false;
        }

        public BaseItem GetItemBySlot(int slotNumber)
        {
            return _items[slotNumber];
        }

        public Sprite GetItemIconBySlot(int slotNumber)
        {
            if (_items[slotNumber] != null)
            {
                return _items[slotNumber].ItemStruct.Icon;
            }
            else
            {
                return null;
            }
        }

        public BaseItem[] GetAll()
        {
            return (BaseItem[])_items.Clone();
        }

        public int GetSlotsCount()
        {
            return _items.Length;
        }

        public void Clear()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    TakeItem(i);
                }
            }
        }

        private void OnChangeItem(int slotNumber, BaseItem item)
        {
            OnChangeItemHandler?.Invoke(slotNumber, item?.ItemStruct.Icon);
        }

        #endregion
    }
}
