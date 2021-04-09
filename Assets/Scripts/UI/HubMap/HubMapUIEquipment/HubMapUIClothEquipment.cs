using System;
using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIClothEquipment
    {
        #region Fields

        private HubMapUIBaseItemModel[] _items;
        private HubMapUIClothType[] _slotTypes;

        #endregion


        #region Properties

        public Action<int, HubMapUIBaseItemModel> OnChangeItemHandler { get; set; }

        #endregion


        #region ClassLifeCycle

        public HubMapUIClothEquipment(HubMapUIClothType[] clothTypes)
        {
            _items = new HubMapUIBaseItemModel[clothTypes.Length];
            _slotTypes = clothTypes;
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

            if (item.ItemType == HubMapUIItemType.Cloth)
            {
                if ((item as HubMapUIClothItemModel).ClothType == _slotTypes[slotNumber])
                {
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
                }
                else
                {
                    Debug.Log("The clothing is not the right type");
                }
            }
            else
            {
                Debug.Log("The item is not clothing");
            }

            return isSucceful;
        }

        public bool PutItemToFirstEmptySlot(HubMapUIBaseItemModel item)
        {
            if (item.ItemType == HubMapUIItemType.Cloth)
            {
                HubMapUIClothItemModel clothItem = item as HubMapUIClothItemModel;
                for (int i = 0; i < _items.Length; i++)
                {
                    if (_slotTypes[i] == clothItem.ClothType)
                    {
                        if (_items[i] == null)
                        {
                            PutItem(i, item);
                            return true;
                        }
                    }
                }
            }
            Debug.Log("No free slot of suitable cloth type found");
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

        private void OnChangeItem(int slotNumber, HubMapUIBaseItemModel item)
        {
            OnChangeItemHandler?.Invoke(slotNumber, item);
        }

        #endregion
    }
}
