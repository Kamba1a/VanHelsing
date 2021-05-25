using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseLimitedStorage<ElementType, EnumStorageType> : BaseStorage<ElementType, EnumStorageType> where EnumStorageType : Enum 
    {
        #region Properties

        public Action<EnumStorageType, int, ElementType> OnPutElementToSlotHandler { get; set; }
        public Action<EnumStorageType, int, ElementType> OnTakeElementFromSlotHandler { get; set; }
        public Action<EnumStorageType> OnStorageResizeHandler { get; set; }


        #endregion


        #region ClassLifeCycle

        public BaseLimitedStorage(int slotsAmount, EnumStorageType storageType) : base(storageType)
        {
            _elementSlots = new List<ElementType>();

            for (int i = 0; i < slotsAmount; i++)
            {
                _elementSlots.Add(default);
            }
        }

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

        public virtual void AddSlots(int slotAmount)
        {
            if (slotAmount > 0)
            {
                for (int i = 0; i < slotAmount; i++)
                {
                    _elementSlots.Add(default);
                }
                OnStorageResize();
            }
            else
            {
                Debug.LogError($"Incorrect input parameter: parameter less or equal zero (input value: {slotAmount})");
            }
        }

        protected void OnPutElementToSlot(int slotIndex, ElementType newElement)
        {
            OnPutElementToSlotHandler?.Invoke(StorageType, slotIndex, newElement);
        }

        protected void OnTakeElementFromSlot(int slotIndex, ElementType takedElement)
        {
            OnTakeElementFromSlotHandler?.Invoke(StorageType, slotIndex, takedElement);
        }

        protected void OnStorageResize()
        {
            OnStorageResizeHandler?.Invoke(StorageType);
        }

        #endregion
    }
}
