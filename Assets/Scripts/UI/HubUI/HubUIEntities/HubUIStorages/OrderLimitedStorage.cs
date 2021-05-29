using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class OrderLimitedStorage : BaseLimitedStorage<ItemOrderModel, OrderStorageType>
    {
        public Func<int, bool> CheckPossibilityFunc { get; set; }


        public OrderLimitedStorage(int slotsAmount) : base(slotsAmount, OrderStorageType.None) { }


        public bool HasFreeSlots()
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PutElement(int slotIndex, ItemOrderModel order)
        {
            bool isSucceful = false;

            if (_elementSlots[slotIndex] == null)
            {
                _elementSlots[slotIndex] = order;
                isSucceful = true;
            }
            //else
            //{
            //    isSucceful = PutElementToFirstEmptySlot(order);
            //}

            if (isSucceful)
            {
                OnPutElementToSlot(slotIndex, order);
            }
            //else
            //{
            //    HubUIServices.SharedInstance.GameMessages.Notice(StorageType + " is full");
            //}

            return isSucceful;
        }

        public override bool PutElementToFirstEmptySlot(ItemOrderModel order)
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    if (CheckPossibilityFunc.Invoke(i))
                    {
                        return PutElement(i, order);
                    }
                    else
                    {
                        Debug.Log($"Impossible to put an element in slot {i}");
                    }
                }
            }
            return false;
        }

        public bool PutElementToFirstEmptySlot(ItemOrderModel order, out int? slotIndex)
        {
            slotIndex = null;
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] == null)
                {
                    if (CheckPossibilityFunc.Invoke(i))
                    {
                        if (PutElement(i, order))
                        {
                            slotIndex = i;
                            return true;
                        }
                    }
                    else
                    {
                        Debug.Log($"Impossible to put an element in slot {i}");
                    }
                }
            }
            return false;
        }

        public override Sprite GetElementSpriteBySlot(int slotIndex)
        {
            if (_elementSlots[slotIndex] != null)
            {
                return _elementSlots[slotIndex].Recipe.Item.Icon;
            }
            else
            {
                return null;
            }
        }
    }
}
