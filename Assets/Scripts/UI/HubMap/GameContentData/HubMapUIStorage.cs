using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeastHunter
{
    public class HubMapUIStorage
    {
        private BaseItem[] _items;

        public Action<int, Sprite> OnChangeItemHandler;

        public HubMapUIStorage(int slotsAmount)
        {
            _items = new BaseItem[slotsAmount];
        }

        public void PutItem(int slotNumber, BaseItem item)
        {
            if (_items[slotNumber] == null)
            {
                _items[slotNumber] = item;
            }
            OnChangeItem(slotNumber, item);
        }

        public BaseItem TakeItem(int slotNumber)
        {
            BaseItem item = _items[slotNumber];
            _items[slotNumber] = null;
            OnChangeItem(slotNumber, null);
            return item;
        }

        public BaseItem GetItemBySlot(int slotNumber)
        {
            return _items[slotNumber];
        }

        public BaseItem[] GetAll()
        {
            return (BaseItem[])_items.Clone();
        }

        public int GetSlotsCount()
        {
            return _items.Length;
        }

        public List<BaseItem> GetItemsOnly()
        {
            List<BaseItem> items = new List<BaseItem>();
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    items.Add(_items[i]);
                }
            }
            return items;
        }

        private void OnChangeItem(int slotNumber, BaseItem item)
        {
            OnChangeItemHandler?.Invoke(slotNumber, item?.ItemStruct.Icon);
        }
    }
}
