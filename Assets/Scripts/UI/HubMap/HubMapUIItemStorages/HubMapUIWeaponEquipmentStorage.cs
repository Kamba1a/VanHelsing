using System;
using UnityEngine;
using System.Collections.Generic;


namespace BeastHunter
{
    public class HubMapUIWeaponEquipmentStorage : HubMapUIBaseItemStorage
    {
        private const int SLOTS_AMOUNT_IN_WEAPON_SET = 2;


        public Action<int, Sprite> OnTwoHandedWeaponHandler;

        public override HubMapUIItemStorageType StorageType { get; protected set; }


        public HubMapUIWeaponEquipmentStorage(int weaponSetsAmount)
        {
            StorageType = HubMapUIItemStorageType.WeaponEquipment;

            _items = new List<HubMapUIBaseItemModel>();
            int slotsAmount = weaponSetsAmount * SLOTS_AMOUNT_IN_WEAPON_SET;
            for (int i = 0; i < slotsAmount; i++)
            {
                _items.Add(null);
            }
        }


        public override bool RemoveItem(int slotIndex)
        {
            if (_items[slotIndex] != null && (_items[slotIndex] as HubMapUIWeaponItemModel).IsTwoHanded)
            {
                OnTwoHandedWeapon(AdjacentSlotIndex(slotIndex), null);
            }
            return base.RemoveItem(slotIndex);
        }

        public override bool PutItem(int slotIndex, HubMapUIBaseItemModel item)
        {
            bool isSucceful = false;
            int putSlotIndex = slotIndex;
            int? adjacendSlotIndex = null;

            if (item != null)
            {
                if (item.ItemType == HubMapUIItemType.Weapon)
                {
                    HubMapUIWeaponItemModel weapon = item as HubMapUIWeaponItemModel;

                    if (_items[slotIndex] == null && AdjacentWeapon(slotIndex) == null)
                    {
                        if (IsEvenSlotIndex(slotIndex) || !weapon.IsTwoHanded)
                        {
                            if (weapon.IsTwoHanded)
                            {
                                adjacendSlotIndex = AdjacentSlotIndex(slotIndex);
                            }
                            _items[slotIndex] = item;
                            isSucceful = true;
                        }
                        else
                        {
                            _items[AdjacentSlotIndex(slotIndex)] = item;
                            putSlotIndex = AdjacentSlotIndex(slotIndex);
                            adjacendSlotIndex = slotIndex;
                            isSucceful = true;
                        }
                    }
                    else if (_items[slotIndex] == null && !AdjacentWeapon(slotIndex).IsTwoHanded && !weapon.IsTwoHanded)
                    {
                        _items[slotIndex] = item;
                        isSucceful = true;
                    }
                    else
                    {
                        HubMapUIServices.SharedInstance.GameMessages.Notice("There is already an weapon in the set");
                        isSucceful = false;
                    }
                }
                else
                {
                    HubMapUIServices.SharedInstance.GameMessages.Notice("Putting item is not weapon");
                    isSucceful = false;
                }
            }
            else
            {
                if (_items[slotIndex] == null)
                {
                    return true;
                }
                else
                {
                    _items[slotIndex] = null;
                    isSucceful = true;
                }
            }

            if (isSucceful)
            {
                OnPutItemToSlot(putSlotIndex, _items[putSlotIndex]);
            }

            if (adjacendSlotIndex.HasValue)
            {
                OnTwoHandedWeapon(adjacendSlotIndex.Value, GetItemIconBySlot(putSlotIndex));
            }

            return isSucceful;
        }

        public HubMapUIWeaponItemModel AdjacentWeapon(int slotIndex)
        {
            return GetItemBySlot(AdjacentSlotIndex(slotIndex)) as HubMapUIWeaponItemModel;
        }

        private bool IsEvenSlotIndex(int slotIndex)
        {
            return (float)slotIndex % 2 == 0;
        }

        private int AdjacentSlotIndex(int slotIndex)
        {
            if (IsEvenSlotIndex(slotIndex))
            {
                return slotIndex + 1;
            }
            else
            {
                return slotIndex - 1;
            }
        }

        private void OnTwoHandedWeapon(int adjacentSlotIndex, Sprite sprite)
        {
            OnTwoHandedWeaponHandler?.Invoke(adjacentSlotIndex, sprite);
        }
    }
}
