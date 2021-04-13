﻿using UnityEngine;


namespace BeastHunter
{
    public class HubMapUIClothesEquipmentStorage : HubMapUIBaseItemStorage
    {
        #region Fields

        public override HubMapUIItemStorageType StorageType { get; protected set; }
        private HubMapUIClothesType[] _slotTypes;

        #endregion

  
        #region ClassLifeCycle

        public HubMapUIClothesEquipmentStorage(HubMapUIClothesType[] clothTypes, HubMapUIItemStorageType storageType)
        {
            StorageType = storageType;
            _items = new HubMapUIBaseItemModel[clothTypes.Length];
            _slotTypes = clothTypes;
        }

        #endregion


        #region Methods

        public override bool PutItem(int slotNumber, HubMapUIBaseItemModel item)
        {
            bool isSucceful = false;

            if (item != null)
            {
                if (item.ItemType == HubMapUIItemType.Cloth)
                {
                    if ((item as HubMapUIClothesItemModel).Type == _slotTypes[slotNumber])
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
            }
            else
            {
                isSucceful = true;
            }

            if (isSucceful)
            {
                OnPutItemToSlot(slotNumber, item);
            }

            return isSucceful;
        }

        public override bool PutItemToFirstEmptySlot(HubMapUIBaseItemModel item)
        {
            if (item != null)
            {
                if (item.ItemType == HubMapUIItemType.Cloth)
                {
                    HubMapUIClothesItemModel clothItem = item as HubMapUIClothesItemModel;
                    for (int i = 0; i < _items.Length; i++)
                    {
                        if (_slotTypes[i] == clothItem.Type)
                        {
                            if (_items[i] == null)
                            {
                                return PutItem(i, item);
                            }
                        }
                    }
                }
            }
            else
            {
                return true;
            }

            Debug.Log("No free slot of suitable cloth type found");
            return false;
        }

        #endregion
    }
}
