using System;
using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseLimitedStorage<ElementType, EnumStorageType> : BaseStorage<ElementType, EnumStorageType> where EnumStorageType : Enum 
    {
        #region Properties

        public virtual Action<EnumStorageType, int, ElementType> OnPutElementToSlotHandler { get; set; }
        public virtual Action<EnumStorageType, int, ElementType> OnTakeElementFromSlotHandler { get; set; }


        #endregion


        #region Methods

        public override bool RemoveElement(int slotIndex)
        {
            if (_elementSlots[slotIndex] != null)
            {
                ElementType takenElement = _elementSlots[slotIndex];
                _elementSlots[slotIndex] = default;
                OnTakeElementFromSlot(slotIndex, takenElement);
            }
            return true;
        }

        public override void ClearSlots()
        {
            for (int i = 0; i < _elementSlots.Count; i++)
            {
                if (_elementSlots[i] != null)
                {
                    RemoveElement(i);
                }
            }
        }

        protected virtual void OnPutElementToSlot(int slotIndex, ElementType newElement)
        {
            OnPutElementToSlotHandler?.Invoke(StorageType, slotIndex, newElement);
        }

        protected virtual void OnTakeElementFromSlot(int slotIndex, ElementType takedElement)
        {
            OnTakeElementFromSlotHandler?.Invoke(StorageType, slotIndex, takedElement);
        }

        #endregion
    }
}
