using System;
using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public abstract class BaseStorage<ElementType, EnumStorageType> where EnumStorageType : Enum
    {
        #region Fields

        protected List<ElementType> _elementSlots;

        #endregion


        #region Properties

        public virtual Action<EnumStorageType, int, ElementType> OnPutElementToSlotHandler { get; set; }
        public virtual Action<EnumStorageType, int, ElementType> OnTakeElementFromSlotHandler { get; set; }

        public abstract EnumStorageType StorageType { get; protected set; }

        #endregion


        #region Methods

        public abstract bool PutElement(int slotIndex, ElementType element);
        public abstract bool PutElementToFirstEmptySlot(ElementType element);


        public virtual bool PutElementToFirstEmptySlotFromOtherStorage(BaseStorage<ElementType, EnumStorageType> otherStorage, int otherStorageSlotIndex)
        {
            ElementType element = otherStorage.GetElementBySlot(otherStorageSlotIndex);
            if (element != null)
            {
                if (this.PutElementToFirstEmptySlot(element))
                {
                    otherStorage.RemoveElement(otherStorageSlotIndex);
                    return true;
                }
                else
                {
                    Debug.Log($"The storage {StorageType} is full");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public virtual void SwapElementsWithOtherStorage(int currentStorageSlotIndex, BaseStorage<ElementType, EnumStorageType> otherStorage, int otherStorageSlotIndex)
        {
            ElementType currentStorageElement = this.GetElementBySlot(currentStorageSlotIndex);
            ElementType otherStorageElement = otherStorage.GetElementBySlot(otherStorageSlotIndex);
            bool isSuccefulTakeElements = false;
            bool isSuccefullPutElements = false;

            if (this.RemoveElement(currentStorageSlotIndex))
            {
                if (otherStorage.RemoveElement(otherStorageSlotIndex))
                {
                    isSuccefulTakeElements = true;
                }
                else
                {
                    this.PutElement(currentStorageSlotIndex, currentStorageElement);
                }
            }

            if (isSuccefulTakeElements)
            {
                isSuccefullPutElements =
                    this.PutElement(currentStorageSlotIndex, otherStorageElement) &&
                    otherStorage.PutElement(otherStorageSlotIndex, currentStorageElement);

                if (!isSuccefullPutElements)
                {
                    this.RemoveElement(currentStorageSlotIndex);
                    this.PutElement(currentStorageSlotIndex, currentStorageElement);

                    otherStorage.RemoveElement(otherStorageSlotIndex);
                    otherStorage.PutElement(otherStorageSlotIndex, otherStorageElement);
                }
            }

            if (!(isSuccefulTakeElements && isSuccefullPutElements))
            {
                Debug.LogWarning("Drag and drop swap elements operation was not successful");
            }
        }

        public virtual bool RemoveElement(int slotIndex)
        {
            if (_elementSlots[slotIndex] != null)
            {
                ElementType takenElement = _elementSlots[slotIndex];
                _elementSlots[slotIndex] = default;
                OnTakeElementFromSlot(slotIndex, takenElement);
            }
            return true;
        }

        public virtual ElementType GetElementBySlot(int slotIndex)
        {
            return _elementSlots[slotIndex];
        }

        public virtual int GetSlotsCount()
        {
            return _elementSlots.Count;
        }

        public virtual void Clear()
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
