using System.Collections.Generic;
using UnityEngine;


namespace BeastHunterHubUI
{
    public class CharacterUnlimitedStorage : BaseStorage<CharacterModel, CharacterStorageType>
    {
        public override CharacterStorageType StorageType { get; protected set; }


        public CharacterUnlimitedStorage(CharacterStorageType storageType)
        {
            StorageType = storageType;
            _elementSlots = new List<CharacterModel>();
        }


        public override void ClearSlots()
        {
            _elementSlots.Clear();
        }

        public override bool PutElement(int slotIndex, CharacterModel character)
        {
            if(character != null)
            {
                _elementSlots.Insert(slotIndex, character);
            }
            return true;
        }

        public override bool PutElementToFirstEmptySlot(CharacterModel character)
        {
            if(character != null)
            {
                _elementSlots.Add(character);
            }
            return true;
        }

        public override bool RemoveElement(int slotIndex)
        {
            if(slotIndex < _elementSlots.Count)
            {
                _elementSlots.RemoveAt(slotIndex);
            }
            else
            {
                Debug.LogError(this + "Incorrect input parameter: slot index exceeds the number of slots in list");
            }
            return true;
        }
    }
}
