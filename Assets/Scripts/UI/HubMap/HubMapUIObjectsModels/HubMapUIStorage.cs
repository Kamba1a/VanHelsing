using System;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIStorage
    {
        #region Fields

        private HubMapUIBaseItemModel[] _items;

        #endregion


        #region Properties

        public Action<int, HubMapUIBaseItemModel> OnChangeItemHandler { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIStorage(int slotsAmount)
        {
            _items = new HubMapUIBaseItemModel[slotsAmount];
        }

        #endregion


        #region Methods

        public HubMapUIBaseItemModel TakeItem(int slotNumber)
        {
            HubMapUIBaseItemModel item = _items[slotNumber];
            _items[slotNumber] = null;
            OnChangeItem(slotNumber, null);
            return item;
        }

        public bool PutItem(int slotNumber, HubMapUIBaseItemModel item)
        {
            bool isSucceful = false;

            if (_items[slotNumber] == null)
            {
                _items[slotNumber] = item;
                isSucceful = true;
            }
            else
            {
                isSucceful = PutItemToFirstEmptySlot(item);
            }

            if (isSucceful)
            {
                OnChangeItem(slotNumber, item);
            }

            return isSucceful;
        }

        public bool PutItemToFirstEmptySlot(HubMapUIBaseItemModel item)
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

        public HubMapUIBaseItemModel GetItemBySlot(int slotNumber)
        {
            return _items[slotNumber];
        }

        public Sprite GetItemIconBySlot(int slotNumber)
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

        public HubMapUIBaseItemModel[] GetAll()
        {
            return (HubMapUIBaseItemModel[])_items.Clone();
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

        public bool IsFull()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsContainItem(HubMapUIBaseItemData item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].DataInstanceID == item.GetInstanceID())
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsContainItem(HubMapUIBaseItemModel item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == item)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveFirstItem(HubMapUIBaseItemData item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].DataInstanceID == item.GetInstanceID())
                {
                    TakeItem(i);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveFirstItem(HubMapUIBaseItemModel item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == item)
                {
                    TakeItem(i);
                    return true;
                }
            }
            return false;
        }

        private void OnChangeItem(int slotNumber, HubMapUIBaseItemModel item)
        {
            OnChangeItemHandler?.Invoke(slotNumber, item);
        }

        #endregion
    }
}
